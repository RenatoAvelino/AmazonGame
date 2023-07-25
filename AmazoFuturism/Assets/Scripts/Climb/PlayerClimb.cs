using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    private GameObject parent;
    [SerializeField] private Vector2 position = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            //Debug.Log("Teste");
            parent.transform.position = new Vector3(parent.transform.position.x + position.x,
                parent.transform.position.y + position.y, parent.transform.position.z);
        }
    }
}
