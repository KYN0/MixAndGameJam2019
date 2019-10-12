using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [Header("Shuriken Area")]
    public float fireRate;
    private float nextFire;
    public Transform spawnShurikens;
    public GameObject ShurikenPrefab;
    public AudioSource ShurikenSound;
    public TextMeshProUGUI nShuriken;



    [Header("GameObjects")]
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
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
            ShurikenSound.Play();
            
            Instantiate(ShurikenPrefab, spawnShurikens.position, transform.rotation);
            // StartCoroutine(resetaAnim(1f));

        }

        




    }
    private void LateUpdate() {
         Vector3 targetPos = cameraTransform.position;
        targetPos.y = transform.position.y;
        Quaternion newRotation = Quaternion.LookRotation(transform.position - targetPos);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 0.5f);
    }

    public void updateItemsGUI()
    {
        nShuriken.text = shurikenNumber.ToString();
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
                Life += other.GetComponent<Collectable>().value;
                Destroy(other);
                updateItemsGUI();
            }
        }
    }
}
