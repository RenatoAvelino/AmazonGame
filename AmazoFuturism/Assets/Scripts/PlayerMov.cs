using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    [SerializeField]
    private float movSpeed = 5f;
    [SerializeField]
    private float jumpingPower = 10f;
    [SerializeField]
    private Rigidbody rb;

    private float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpingPower, 0);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontal * movSpeed, rb.velocity.y, 0);
    }
}
