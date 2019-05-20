using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barra : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 com;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("El com es : " + rb.centerOfMass);
    }

    
}
