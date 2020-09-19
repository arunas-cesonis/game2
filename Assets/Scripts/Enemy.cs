using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        //target = new Vector3(Random.Range(-10.0f, 10.0f), 0.5f, Random.Range(-10.0f, 10.0f));
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 d = target.GetComponent<Rigidbody>().position - body.position;
        d.Normalize();
        if (d != Vector3.zero)
        {
            //transform.forward = d; 
        }
        //print(d);
        //body.MovePosition(body.position + d * (Time.fixedDeltaTime * 0.1f));
    }

}   
