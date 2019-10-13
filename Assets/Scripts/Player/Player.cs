using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("properties")]
    public Transform cameraTransform;
    private float Life;
    public float startLife;
    public float damageCooldown;
    public bool isTakingDMG;
    public float distance;
    public float offset;

    [Header("Collectables")]
    public int shurikenNumber;
    public int trophies;
    public AudioSource pizzaTime;

    [Header("Shuriken Area")]
    public float fireRate;
    private float nextFire;
    public Transform spawnShurikens;
    public GameObject ShurikenPrefab;
    public AudioSource ShurikenSound;
    public TextMeshProUGUI nShuriken;
    public Animator anim;



    [Header("GameObjects")]
    public GameObject player;

    [Header("GUI")]
    public Image LifeBar;

    // Start is called before the first frame update
    void Start()
    {
        Life = startLife;
        Cursor.visible = false;
        updateItemsGUI();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && Time.time > nextFire && shurikenNumber > 0)
        {
            shurikenNumber--;
            nextFire = Time.time + fireRate;
            updateItemsGUI();
            anim.SetBool("idle", false);
            anim.SetBool("walk", false);
            anim.SetBool("shuriken", true);
            ShurikenSound.Play();

            Instantiate(ShurikenPrefab, spawnShurikens.position, transform.rotation);
            // StartCoroutine(resetaAnim(1f));

        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("shuriken") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f){
           anim.SetBool("shuriken", false); 
        }
        if (Life <= 0)
        {
            Life = startLife;
            GetComponent<Respawn>().Respawning();
            updateItemsGUI();
        }






    }
    private void LateUpdate()
    {
        Vector3 targetPos = cameraTransform.position;
        targetPos.y = transform.position.y;
        Quaternion newRotation = Quaternion.LookRotation(transform.position - targetPos);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 0.5f);
    }

    public void updateItemsGUI()
    {
        nShuriken.text = shurikenNumber.ToString();
        LifeBar.fillAmount = Life / 100;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("collectable"))
        {
            if (other.GetComponent<Collectable>().type == 1)
            {
                shurikenNumber += other.GetComponent<Collectable>().value;
                Destroy(other);
                updateItemsGUI();
            }
            else if (other.GetComponent<Collectable>().type == 2)
            {
                pizzaTime.Play();
                Life += other.GetComponent<Collectable>().value;
                Destroy(other);
                updateItemsGUI();
            }
        }

        if (other.CompareTag("deathzone"))
        {
            this.GetComponent<Respawn>().Respawning();
        }
    }

    public void takeDmg(float dmg)
    {
        Life -= dmg;
        updateItemsGUI();

    }
}
