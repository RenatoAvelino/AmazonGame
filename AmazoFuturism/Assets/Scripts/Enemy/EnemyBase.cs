using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private SphereCollider hearRange;
    [SerializeField] private float hearRadius = 3f;
    [SerializeField] private float noiseSensor = 2f;
    // Start is called before the first frame update
    void Start()
    {
        if (hearRange == null)
        {
            hearRange = GetComponent<SphereCollider>();
        }
        hearRange.isTrigger = true;
        hearRange.radius = hearRadius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float distance = Vector3.Distance(this.gameObject.transform.position, other.gameObject.transform.position);
            float realNoise = other.GetComponent<PlayerMov>().NoiseLv;


            if (other.GetComponent<PlayerMov>().NoiseLv >= noiseSensor)
            {
                //TODO: Oq ele faz ao ouvir o player
                Debug.Log("Distancia: " + distance);
            }
        }
    }
}
