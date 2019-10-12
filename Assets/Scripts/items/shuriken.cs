using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuriken : MonoBehaviour
{
    // public float rotationSpeed;
    public float velocity;
    public float ttl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        //  transform.Rotate(Vector3.up * rotationSpeed );
         Destroy(gameObject, ttl);
    }
}
