using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField]
    private float pullSpeed = 4f;
    [SerializeField]
    private float stopDistance = .5f;
    [SerializeField]
    private GameObject hookPrefab;
    [SerializeField]
    private Transform shootTransform;
    [SerializeField]
    private float hookVelocity = 5f;
    [SerializeField] private float hookLifeTime = 8f;

    private Hook hook;
    private bool pulling;
    private Rigidbody rigid;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        pulling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hook == null && Input.GetMouseButtonDown(0))
        {
            //StopAllCoroutines();
            //Vector3 worldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector3 worldPos2D = new Vector3(worldPos3D.x, worldPos3D.y, this.gameObject.transform.position.z);
            //shootTransform.LookAt(worldPos2D);
            pulling = false;
            hook = Instantiate(hookPrefab, shootTransform.position, Quaternion.identity).GetComponent<Hook>();
            hook.Force = hookVelocity;
            hook.Initialize(this, shootTransform);
            StartCoroutine(DestroyHookAfterLifetime());
        }
        else if (hook != null && Input.GetMouseButtonDown(1))
        {
            DestroyHook();
        }

        if (!pulling || hook == null) return;

        if(Vector3.Distance(transform.position, hook.transform.position) <= stopDistance)
        {
            DestroyHook();
        }
        else
        {
            rigid.AddForce((hook.transform.position - transform.position).normalized * (pullSpeed / 100),
                ForceMode.VelocityChange);
        }
    }

    public void StartPull()
    {
        pulling = true;
    }

    private void DestroyHook()
    {
        if (hook == null) return;

        pulling = false;
        Destroy(hook.gameObject);
        hook = null;
    }

    private IEnumerator DestroyHookAfterLifetime()
    {
        yield return new WaitForSeconds(hookLifeTime);

        DestroyHook();
    }
}
