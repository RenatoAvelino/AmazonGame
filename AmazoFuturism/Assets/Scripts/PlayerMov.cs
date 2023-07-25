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
    private float sneakSpeed = 1.5f;
    [SerializeField]
    private float jumpingPower = 7.5f;
    [SerializeField]
    private float jumpingRunning = 10f;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float runDelay = 1f;
    private float noiseLevel = 5f;
    private float noiseBase;
    public bool _actived = false;

    private float walkSpd;
    private float delayTimer = 0f;
    private bool _isWalkingR = false;
    private bool _isWalkingL = false;
    private bool _isRunning = false;
    private bool _isJumping = false;
    private bool _isSneaking = false;

    private float horizontal;


    public float NoiseLv
    {
        get
        {
            return noiseLevel;
        }
        set
        {
            noiseLevel = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        walkSpd = movSpeed;
        delayTimer = 0f;
        noiseBase = noiseLevel;
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            walkSpd = sneakSpeed;
            _isSneaking = true;
            Debug.Log("Stealth Enter");
            Debug.Log("Speed: " + walkSpd);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isSneaking = false;
            walkSpd = movSpeed;
            Debug.Log("Stealth Exit");
            Debug.Log("Speed: " + walkSpd);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _actived = true;
            //Debug.Log("Ativo");
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            _actived = false;
            //Debug.Log("Inativo");
        }

        if (((Time.time - delayTimer) > runDelay) && (_isWalkingL || _isWalkingR))
        {
            _isWalkingL = false;
            _isWalkingR = false;
            if(!_isSneaking)
            {
                walkSpd = movSpeed;
            }
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

    private void MakeNoise()
    {
        //TODO: Noiselate
    }
}
