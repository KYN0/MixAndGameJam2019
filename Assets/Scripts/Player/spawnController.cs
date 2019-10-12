using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnController : MonoBehaviour
{
    public Transform cameraTransform;
    public float distanceFromCamera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /* Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
          transform.position = new Vector3 (resultingPosition.x, resultingPosition.y, transform.position.z);*/
        Vector3 targetPos = cameraTransform.position;
        targetPos.y = transform.position.y;
        Quaternion newRotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 0.5f);

    }
}
