using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
   public int score = 0;
    public float turnSpeed = 20;
    public float moveSpeed = 1f;
    public float jumpForce = 10f;
    public float gravityModifier = 1f;
    public float outOfBounds = -10f;
    public bool isOnGround = true;
    public bool isAtCheckpoint = false;
    private Vector3 _movement;
    private Rigidbody _rigidbody;
    private Quaternion _rotation = Quaternion.identity;
    private Vector3 _defaultGravity = new Vector3(0f, -9.81f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Physics.gravity = _defaultGravity;
        //Debug.Log(Physics.gravity);
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _movement.Set(horizontal, 0f, vertical);
        _movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        //_animator.SetBool ("IsWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, _movement, turnSpeed * Time.deltaTime, 0f);
        _rotation = Quaternion.LookRotation (desiredForward);

        _rigidbody.MovePosition (_rigidbody.position + _movement * moveSpeed * Time.deltaTime);
        _rigidbody.MoveRotation (_rotation);
    
    }

    //void OnAnimatorMove ()
    //{
    //    _rigidbody.MovePosition (_rigidbody.position + _movement * m_Animator.deltaPosition.magnitude);
    //    _rigidbody.MoveRotation (_rotation);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        
    }
   
    public bool IsPlayerOnGround()
    {
        return isOnGround;
    }
}
