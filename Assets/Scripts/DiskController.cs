using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskController : MonoBehaviour
{
    public Rigidbody rbd;
    public Rigidbody rbesf1;
    public Rigidbody rbesf2;
    private float speed = 150;
    public static bool iniciar;
    private void Start()
    {
        rbd = transform.GetComponent<Rigidbody>();
        rbesf1 = transform.GetComponent<Rigidbody>();
        rbesf2 = transform.GetComponent<Rigidbody>();
    }
    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        HingeJoint join = gameObject.AddComponent<HingeJoint>();
        join.connectedBody = collision.rigidbody;
    }
}
