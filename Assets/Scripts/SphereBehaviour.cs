using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereBehaviour : MonoBehaviour
{

    public Rigidbody rb;

    public float velocity = -10;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.AddForce(velocity,0,0,ForceMode.Impulse);
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("Canvas").Find("Velocidad").GetComponent<Text>().text = rb.velocity.ToString();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Choco broco!");

        HingeJoint join = gameObject.AddComponent<HingeJoint>();
        join.connectedBody = collision.rigidbody;
        
    }

}

