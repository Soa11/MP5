using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCube1 : MonoBehaviour {




    void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(Vector3.up * Time.deltaTime*10);

        // ...also rotate around the World's Y axis
        //transform.Rotate(Vector3.up * Time.deltaTime*10 , Space.World);
    }
}