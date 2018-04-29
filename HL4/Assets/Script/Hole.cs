using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {

    public float lifeTime;
    public float pullForce;
    public float pullRadius;
    public LayerMask holeMask;
    protected bool startGravity;

    // Use this for initialization
    void Start()
    {
        startGravity = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        Invoke("fixPoint", 3.0f);
        Invoke("destroy", lifeTime);
    }

    void Update()
    {
        if (startGravity)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, pullRadius, holeMask);
            foreach (Collider collider in hitColliders)
            {
                Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    rigidbody.isKinematic = false;
                    rigidbody.useGravity = true;
                    Vector3 forceDirection = transform.position - collider.transform.position;
                    // apply force on target towards me
                    rigidbody.AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
                }
            }
        }
    }

    void fixPoint()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        startGravity = true;
    }

    void destroy()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        Destroy(gameObject);
    }
}
