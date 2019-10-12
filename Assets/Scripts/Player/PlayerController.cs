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
    public bool EnablecloudStepping;
    public bool cloudStepping;
    public float distToGround;
    public float cloudStepCooldown;

    private Vector3 targetPos;
    private Vector3 velocity = Vector3.zero;
    public float lowJumpMultiplierFloat;
    Collider m_ObjectCollider;


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
    public GameObject sword;

    [Header("animation")]
    public Animator anim;
    private bool isJumping;
    private bool isGrounded;
    public float gravity;

    public float JumpForce;

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }


    // Start is called before the first frame update
    void Start()
    {
        sword.SetActive(false);
        distToGround = GetComponent<Collider>().bounds.extents.y;
        cloudStepping = false;
        EnablecloudStepping = true;


        targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (EnablecloudStepping.Equals(false))
        {
            sword.SetActive(true);
        }
        else
        {
            sword.SetActive(false);
        }

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

           // qTo = Quaternion.LookRotation(moveDir);



        }
        // transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime * smooth);
        // transform.eulerAngles = new Vector3(fixedRotation.x, transform.eulerAngles.y, transform.eulerAngles.z);


        velocity = rb.velocity;
        Vector3 velocityChange = (moveDir - velocity);

        velocityChange.y = 0;


        rb.AddForce(velocityChange, ForceMode.VelocityChange);


        // if (isJumping == false && Input.GetButtonDown("Jump") && isGrounded == true)
        if (Input.GetButtonDown("Jump"))
        {

            if (IsGrounded())
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

            if (!IsGrounded() && cloudStepping.Equals(true))
            {
                rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), 0f);
                moveDir.y = JumpForce;
                isGrounded = false;
                isJumping = true;
                cloudStepping = false;
            }
        }

        if (Input.GetButton("Fire1") && EnablecloudStepping.Equals(true))
            {

                EnablecloudStepping = false;
                anim.SetBool("attack", true);
                anim.SetBool("idle", false);

            }

        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                EnablecloudStepping = true;
                anim.SetBool("attack", false);
                anim.SetBool("idle", true);
            }


        /*if (isJumping == false && Input.GetButtonDown("Jump") && IsGrounded())
        {




            if (rb.velocity.y > 0 && !Input.GetButton("Jump") && !cloudStepping)
            {
                print("apply gravity fall");
                rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplierFloat - 1f) * Time.deltaTime;
            }

            if (Input.GetButton("Fire1") && EnablecloudStepping.Equals(true))
            {

                EnablecloudStepping = false;
                anim.SetBool("attack", true);
                anim.SetBool("idle", false);




            }
            if (isJumping == true && Input.GetButtonDown("Jump") && cloudStepping == true)
            {

                print("cloudstep ");
                rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), 0f);
                moveDir.y = JumpForce;
                isGrounded = false;
                isJumping = true;
                cloudStepping = false;
                //jump();

            }

            if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                EnablecloudStepping = true;
                anim.SetBool("attack", false);
                anim.SetBool("idle", true);
            }

            //moveDir.y -= gravity * Time.deltaTime;

        }*/
    }

    IEnumerator CloudStepReset(){

        yield return new WaitForSeconds(cloudStepCooldown);
        
    }

        public void jump()
        {
            // if (isJumping == true)
            // {
            print("ta entrando em cloudStep pelo menos");
            rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), 0f);
            moveDir.y = JumpForce;
            isGrounded = false;
            isJumping = true;
            cloudStepping = false;
            // }

        }

        float CalculateJumpVerticalSpeed()
        {
            return Mathf.Sqrt(2 * JumpForce * gravity);
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.collider.CompareTag("baseGround"))
            {
                distToGround = GetComponent<Collider>().bounds.extents.y;
                print("ready for jump");
                isJumping = false;
            }
        }
        /* private void OnCollisionStay(Collision collision)
         {
             isGrounded = true;
             isJumping = false;
         }
         */

    }
