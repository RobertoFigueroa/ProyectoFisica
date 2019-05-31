using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barra : MonoBehaviour
{
    //Cambio a script de las esferas de la escena del disco
   public Rigidbody rbe;
    public Rigidbody rbd;
    /*public Vector3 com;*/
    // Start is called before the first frame update
    void Start()
    {
       // rb = GetComponent<Rigidbody>();
       // rb.centerOfMass = com;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("El com es : " + rb.centerOfMass);
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*HingeJoint join = gameObject.AddComponent<HingeJoint>();
        join.connectedBody = collision.rigidbody;
        rb.useGravity = false;*/
        rbe.transform.SetParent(rbd.transform);
        rbe.transform.position = new Vector3(gameObject.transform.position.x, 3, gameObject.transform.position.z);
        rbe.useGravity = false;
    }


}
