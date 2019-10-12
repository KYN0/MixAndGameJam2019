using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
     [Header("Properties")]
    public GameObject player;
    public bool enableCloudSteping;
    public float cloudSteppingDelay;
    // Start is called before the first frame update
    void Start()
    {
       enableCloudSteping = false; 
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("ground") && enableCloudSteping.Equals(false)){
            enableCloudSteping = true;
            player.GetComponent<PlayerController>().cloudStepping = true;
            print("enable cloudstep");
            //player.GetComponent<PlayerController>().jump();

            //StartCoroutine(resetCloudStepping());
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("ground")){
            enableCloudSteping = false;
        }
    }

    IEnumerator resetCloudStepping(){

        yield return new WaitForSeconds(cloudSteppingDelay);
        print("reset cloudstep");
        enableCloudSteping = false;
    }
}
