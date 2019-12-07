using UnityEngine;
using System.Collections;
using System;

public class REFERENCE: MonoBehaviour
{

    public int scaleMinValue = 31; //change the  minimum heat level to trigger scale
    public float maxSize = 20;
    private float growFactor = 1f;
    public float waitTime = 15f;

    private bool isScaling = false;
    private Vector3 originalScale;

    public UnitySerialPort sp;

    private string str = "";
    public int[] values; // declare numbers as an int array of any size


    void Start()
    {
        values = new int[16];  // numbers is a 16-element array
        sp = UnitySerialPort.Instance;
        sp.OpenSerialPort();
    }


    void Update()
    {

        if (!sp.SerialPort.IsOpen)
            sp.OpenSerialPort();

        str = sp.RawData;
        values = splitString(str);

        //uncomment for debug
        string s = "";

        foreach (int item in values)
        {
            s += item.ToString();
            s += ", ";
        }

        print("FIRST: " + s);


        originalScale = transform.localScale;

        if (values[0] >= scaleMinValue && originalScale.x <= 1)
            StartCoroutine(Scale());

    }

    private int[] splitString(string str)
    {
        int i = 0;
        int[] readings = new int[16];
        int num = 0;

        foreach (string s in str.Split(','))
        {
            if (Int32.TryParse(s, out num))
            {
                if (i <= 15)
                    readings[i++] = num;
            }
        }

        return readings;
    }

    IEnumerator Scale()
    {

        if (isScaling)
            yield break;

        isScaling = true;

        float timer = 0;

        // we scale all axis, so they will have the same value, 
        // so we can work with a float instead of comparing vectors
        while (maxSize > transform.localScale.x)
        {

            timer += Time.deltaTime;
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
            yield return null;
        }
        // reset the timer

        yield return new WaitForSeconds(waitTime);

        timer = 0;
        while (0 < transform.localScale.x)
        {
            timer += Time.deltaTime;
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
            yield return null;
        }

        transform.localScale = new Vector3(0, 0, 0);

        isScaling = false;

    }

}
