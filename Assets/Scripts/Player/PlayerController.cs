using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("properties")]
    public float speed;
    public float dashDistance;
    public float maxHeight;
    public float minHeight;
    public float smoothTime;
    public float boost;
    public bool cloudStepping;

    private Vector3 targetPos;
    private Vector3 velocity = Vector3.zero;
    public float lowJumpMultiplierFloat;


    [Header("Game Objects")]
    public GameObject player;
    public Rigidbody rb;
    public GameObject GUI;
    private float movx;
    private float movy;
    private Vector3 moveDir;
    public int maxVelocityChange;
    private Quaternion qTo;
    private Vector3 fixedRotation;
    public float smooth = 10f;
    public float gravityScale = 1.0f;
    public float globalGravity = -9.81f;

    [Header("animation")]
    public Animator anim;
    private bool isJumping;
    private bool isGrounded;
    public float gravity;

    public float JumpForce;


    // Start is called before the first frame update
    void Start()
    {
        cloudStepping = false;



        targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {

        movx = Input.GetAxisRaw("Horizontal");
        movy = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(movx, 0f, movy);
        moveDir = Camera.main.transform.TransformDirection(moveDir);
        // moveDir *= speed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveDir *= boost;
        }
        else
        {
            moveDir *= speed;
        }

        if (movx != 0 || movy != 0)
        {

            // anim.SetBool("walk", true);
            // anim.SetBool("idle", false);
            // anim.SetBool("jump", false);

        }
        else
        {

            // anim.SetBool("walk", false);
            // anim.SetBool("idle", true);
            // anim.SetBool("jump", false);


        }
        if (movx == 0 && movy == 0)
        {
            // anim.SetBool("walk", false);
            // anim.SetBool("idle", true);
            // anim.SetBool("jump", false);
        }

        if (moveDir != Vector3.zero)
        {

            qTo = Quaternion.LookRotation(moveDir);



        }
        transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime * smooth);
        transform.eulerAngles = new Vector3(fixedRotation.x, transform.eulerAngles.y, transform.eulerAngles.z);


        velocity = rb.velocity;
        Vector3 velocityChange = (moveDir - velocity);

        velocityChange.y = 0;


        rb.AddForce(velocityChange, ForceMode.VelocityChange);


        if (isJumping == false && Input.GetButtonDown("Jump") && isGrounded == true)
        {


            // anim.SetBool("jump", true);
            // anim.SetBool("idle", false);
            // anim.SetBool("walk", false);

            //Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            //rb.AddForce(gravity, ForceMode.Acceleration);
            rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), 0f);
            moveDir.y = JumpForce;
            isGrounded = false;
            isJumping = true;
        }
        if (rb.velocity.y > 0 && !Input.GetButton("Jump") || Input.GetButton("Fire1"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplierFloat - 1f) * Time.deltaTime;
        }

        if (Input.GetButton("Fire1"))
        {

            anim.SetBool("attack", true);
            anim.SetBool("idle", false);
            
            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("attack")){
                anim.SetBool("attack", false);
            anim.SetBool("idle", true);
            }


        }
        
        //moveDir.y -= gravity * Time.deltaTime;

    }

    public void jump(){
        if (isJumping == true)
            {
                print("ta entrando em jump pelo menos");
                rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), 0f);
                moveDir.y = JumpForce;
                isGrounded = false;
                isJumping = true;
                cloudStepping = false;
            }

    }

    float CalculateJumpVerticalSpeed()
    {
        return Mathf.Sqrt(2 * JumpForce * gravity);
    }
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
        isJumping = false;
    }


}
