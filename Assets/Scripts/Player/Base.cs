using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [Header("Properties")]
    private float Life;
    public float startLife;
    public float damageCooldown;
    public bool isTakingDMG;
    


    // Start is called before the first frame update
    void Start()
    {
        Life = startLife;
        isTakingDMG = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Life<=0){
            Death();
           Life = startLife; 
        }
        
    }

    public void Death(){

    }

    public void TakeDamage(float dmg){
        if(isTakingDMG.Equals(false)){
            Life -= dmg;
            isTakingDMG = true;
        }

        StartCoroutine(dmgCooldown());
    }

    IEnumerator dmgCooldown(){
       yield return new WaitForSeconds(damageCooldown);
       isTakingDMG = false;
    }
}
