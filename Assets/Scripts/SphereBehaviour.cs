using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereBehaviour : MonoBehaviour
{

    public Rigidbody rb;
    //public Slider masa;
    //public Slider velocidad;
    private float velocity;
    private float mass;
    // Start is called before the first frame update
    void Start()
    {
        //velocidad = transform.Find("Canvas").Find("Vinicial").GetComponent<Slider>();
        //velocity = velocidad.value;
        //masa = transform.Find("Canvas").Find("Masa").GetComponent<Slider>();
        //mass = masa.value;
        rb = transform.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.mass = mass;
            rb.AddForce(velocity,0,0,ForceMode.Impulse);
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        //velocity = velocidad.value;
        //mass = masa.value;
        transform.Find("Canvas").Find("Velocidad").GetComponent<Text>().text = rb.velocity.ToString();
        //transform.Find("Canvas").Find("Vinicial").Find("printvi").GetComponent<Text>().text = velocity.ToString();
        //transform.Find("Canvas").Find("Masa").Find("printmasa").GetComponent<Text>().text = mass.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Choco broco!");

        HingeJoint join = gameObject.AddComponent<HingeJoint>();
        join.connectedBody = collision.rigidbody;
    }

}

