using UnityEngine;
using System.Collections;
using System;

public class dataPrint : MonoBehaviour
{

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

        print("SECOND: " + s);
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