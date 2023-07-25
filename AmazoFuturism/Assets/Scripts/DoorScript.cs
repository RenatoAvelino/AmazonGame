using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private GameObject endPoint;
    private GameObject player;
    private Vector3 nuovoPos;
    // Start is called before the first frame update
    void Start()
    {
        if(endPoint == null) endPoint = this.gameObject.transform.GetChild(0).gameObject;

        nuovoPos = endPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(player.GetComponent<PlayerMov>()._actived)
            {
                player.transform.position = new Vector3(nuovoPos.x, nuovoPos.y, player.transform.position.z);
            }
        }
    }
}
