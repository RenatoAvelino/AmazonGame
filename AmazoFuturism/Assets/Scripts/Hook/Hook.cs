using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField]
    private float hookForce = 25f;

    public float Force
    {
        get { return hookForce; }
        set { hookForce = value; }
    }

    private Grapple grappler;
    private Rigidbody rigid;
    private LineRenderer lineRenderer;

    public void Initialize(Grapple grapple, Transform shootTransform)
    {
        transform.forward = shootTransform.forward;
        grappler = grapple;
        rigid = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        rigid.AddForce(transform.forward * hookForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] positions = new Vector3[]
        {
            transform.position,
            grappler.transform.position
        };
        lineRenderer.SetPositions(positions);
    }

    private void OnTriggerEnter(Collider other)
    {
        if((LayerMask.GetMask("Grapple") & 1 << other.gameObject.layer ) > 0)
        {
            rigid.useGravity = false;
            rigid.isKinematic = true;

            grappler.StartPull();
        }
    }
}
