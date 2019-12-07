using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class p2colourChange : MonoBehaviour
{

    public float r;
    public float g;
    public float b;
    public float a;

    
    private double avg;
    private int sum;

    public float changingSpeed = 0;


    public UnitySerialPort sp;

    private string str = "";
    public int[] values; // 

    // Use this for initialization
    void Start()
    {
        values = new int[16];  // numbers is a 16-element array
        sp = UnitySerialPort.Instance;
        sp.OpenSerialPort();

    }

    // Update is called once per frame
    void Update()
    {
        sum = 0;

        if (!sp.SerialPort.IsOpen)
            sp.OpenSerialPort();

        str = sp.RawData;
        values = splitString(str);

        //uncomment for debug
        string s = "";

        foreach (int item in values)
        {
            sum += item;
            s += item.ToString();
            s += ", ";
        }

        //average of all 16 values
        avg = sum / 16;
        //avg = 22;

        print("avg: " + avg + " - original: " + values[1]);


        if (values[1] > avg)
        {
            r -= Time.deltaTime * changingSpeed;
            r = Mathf.Clamp(r, 0f, 1f);

            g += Time.deltaTime * changingSpeed;
            g = Mathf.Clamp(g, 0f, 1f);

            b -= Time.deltaTime * changingSpeed;
            b = Mathf.Clamp(b, 0f, 1f);

            a -= Time.deltaTime * changingSpeed;
            a = Mathf.Clamp(a, 0.3f, 1f);

        }
        else if (values[1] <= avg)
        {
            r += Time.deltaTime * changingSpeed;
            r = Mathf.Clamp(r, 0f, 1f);

            g -= Time.deltaTime * changingSpeed;
            g = Mathf.Clamp(g, 0f, 1f);

            b += Time.deltaTime * changingSpeed;
            b = Mathf.Clamp(b, 0f, 1f);

            a += Time.deltaTime * changingSpeed;
            a = Mathf.Clamp(a, 0.3f, 1f);

        }



        this.GetComponent<Renderer>().material.color = new Color(r, g, b, a);
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
}

