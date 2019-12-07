using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class colourChange : MonoBehaviour
{

    public KeyCode rKeyp, gKeyp, bKeyp, rKeym, gKeym, bKeym;

    public float r ;
    public float g ;
    public float b ;
    public float a ;

    public GameObject prevObject;
    public Material prevMaterial;

    public float changingSpeed = 0;
    public float alphaChangingSpeed = 0f;

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
        //Debug.Log(mode + " : " + this.gameObject.name + " : " + this.GetComponent<Renderer>().material.color.a);

        if (values[0] > 25)
            
        {
            r += Time.deltaTime * changingSpeed;
            r = Mathf.Clamp(r, 0f, 1f);
            Debug.Log("r;" + r);
        }

        if (values[0] <= 25)
        {
            r -= Time.deltaTime * changingSpeed;
            r = Mathf.Clamp(r, 0f, 1f);
            Debug.Log("r;" + r);
        }

        if (values[0] > 27)
        {
            g += Time.deltaTime * changingSpeed;
            g = Mathf.Clamp(g, 0f, 1f);
            Debug.Log("g;" + g);
        }

        if (values[0] <= 27)
        {
            g -= Time.deltaTime * changingSpeed;
            g = Mathf.Clamp(g, 0f, 1f);
            Debug.Log("g;" + g);
        }

        if (values[0] > 29)
        
        {
            b += Time.deltaTime * changingSpeed;
            b = Mathf.Clamp(b, 0f, 1f);
            Debug.Log("b;" + b);
        }

        if (values[0] <= 29)
        {
            b -= Time.deltaTime * changingSpeed;
            b = Mathf.Clamp(b, 0f, 1f);
            Debug.Log("b;" + b);
        }

        //if (Input.GetKey(KeyCode.J))
        //if (Input.GetKey(KeyCode.K))

        // 앞에 오브젝트가 있어서 FadeMode가 Stop인 상태일 때,
        // 앞의 오브젝트의 알파가 0.5 이하인지 확인
     

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

