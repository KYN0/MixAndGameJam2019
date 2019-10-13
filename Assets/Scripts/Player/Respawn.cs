using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject[] respawns;
    public int respawnsIndex;
    public bool enableTochangeRespawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        respawnsIndex = 0;
        enableTochangeRespawnPoint=true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawning(){
    transform.position= respawns[respawnsIndex].transform.position; 
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("respawnChanger") && enableTochangeRespawnPoint){
            enableTochangeRespawnPoint= false;
            respawnsIndex++;
            Destroy(other.gameObject);
        }
    }

    IEnumerator cooldownRespawnChanger(){

        yield return new WaitForSeconds(2f);
        enableTochangeRespawnPoint=true;
    }
}
