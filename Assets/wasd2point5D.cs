using UnityEngine;
using System.Collections;

public class wasd2point5D : MonoBehaviour
{

    [Header("movement settings")]
    public static wasd2point5D _instance;
    Rigidbody myRB;
    public float speed = 50f;
    public bool jumped = false;
    public bool grounded = false;
    public float jumpForce = 50f;
    public KeyCode jumpKey;


    [Header("Animation settings")]
    public Animator myAnimator;
    public SpriteRenderer mySprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
         if (wasd3D._instance == null)
        {
            _instance = this;
        }
        else { Destroy(this); }
    }
    
    void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();
        mySprite = GetComponentInChildren<SpriteRenderer>();
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Direction());
        //Debug.Log(lookDir());

        if (Input.GetKeyUp(jumpKey) && grounded)
        {
            jumped = true;
        }

        //function("nameOfParameter", value);
        myAnimator.SetBool("isGrounded", grounded);
        Debug.Log(grounded);
        myAnimator.SetFloat("speed", Mathf.Abs(myRB.linearVelocity.x));

        Debug.Log("Velocity: " + myRB.linearVelocity);

        if(myRB.linearVelocity.x < 0)
        {
            mySprite.flipX = true;
        }
        else { mySprite.flipX = false;}
    }

    void FixedUpdate()
    {
        Vector3 force = Direction();
        Debug.DrawRay(transform.position, force * 2f, Color.white);
        myRB.AddForce(force * speed * Time.fixedDeltaTime);

        if(jumped)
        {
            Debug.Log("Jumping");
            myAnimator.SetBool("jump", true);
            Jump(Time.fixedDeltaTime);
            jumped = false;
        }
    }

    Vector3 Direction()
    {
        Vector3 dir = Vector3.zero;
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        dir = new Vector3(x, 0, z);
        return dir;
    }

    void Jump(float deltaTime)
    {
        Debug.Log("Jumped");
        myRB.AddForce(Vector3.up * jumpForce * deltaTime, ForceMode.Impulse);
        StartCoroutine(jumpCooldown(.3f));
    }

    void OnCollisionStay(Collision collision)
    {
        grounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    //this is a simple coroutine 
    public IEnumerator jumpCooldown(float time)
    {
        //when you first call the coroutine method, anything up here runs immediately
        //any code above the yield statement runs right away
        Debug.Log("jump cooldown started");
        yield return new WaitForSeconds(time); //this lines says, "pause for X or (time) amount of seconds
        //any code written below the yield will run when X or (time) seconds have passed
        myAnimator.SetBool("jump", false);
        jumped = false;
    }


}

