using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    [SerializeField]
    private float movSpeed = 5f;
    [SerializeField]
    private float runSpeed = 20f;
    [SerializeField]
    private float jumpingPower = 7.5f;
    [SerializeField]
    private float jumpingRunning = 10f;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float runDelay = 1f;

    private float walkSpd;
    private float delayTimer = 0f;
    private bool _isWalkingR = false;
    private bool _isWalkingL = false;
    private bool _isRunning = false;
    private bool _isJumping = false;

    private float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        walkSpd = movSpeed;
        delayTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
        {
            float temp = jumpingPower;
            if(_isRunning) temp = jumpingRunning;
            rb.velocity = new Vector3(rb.velocity.x, temp, 0);
            _isJumping = true;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(_isWalkingL == true)
            {
                //Debug.Log("Apertou dnv");
                walkSpd = runSpeed;
                _isWalkingL = false;
                _isRunning = true;
            }
            else
            {
                _isWalkingL = true;
                delayTimer = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_isWalkingR == true)
            {
                //Debug.Log("Apertou dnv");
                walkSpd = runSpeed;
                _isWalkingR = false;
                _isRunning = true;
            }
            else
            {
                _isWalkingR = true;
                delayTimer = Time.time;
            }
        }

        if (((Time.time - delayTimer) > runDelay) && (_isWalkingL || _isWalkingR))
        {
            _isWalkingL = false;
            _isWalkingR = false;
            walkSpd = movSpeed;
            //Debug.Log("Reset Walk");
        }
    }

    private void FixedUpdate()
    {
        if(!_isJumping) rb.velocity = new Vector3(horizontal * walkSpd, rb.velocity.y, 0);

        if (rb.velocity.x == 0)
        {
            _isRunning = false;
            walkSpd = movSpeed;
        }
        if(rb.velocity.y == 0)
        {
            _isJumping = false;
        }
    }
}
