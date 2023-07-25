using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody myRigidBody;
    private GameObject parent;
    
    // Start is called before the first frame update
    void Start()
    {
        parent = this.gameObject.transform.parent.gameObject;
        myRigidBody = parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            //move right
            myRigidBody.velocity = new Vector3(moveSpeed, 0);
        }
        else
        {
            //move left
            myRigidBody.velocity = new Vector3(-moveSpeed, 0);
        }
    }

    private bool IsFacingRight()
    {
        return (parent.transform.localScale.x > Mathf.Epsilon);
    }
    private void OnTriggerExit(Collider other)
    {
        //Turn
        parent.transform.localScale = new Vector3(-(Mathf.Sign(myRigidBody.velocity.x)), parent.transform.localScale.y, parent.transform.localScale.z);
    }
}
