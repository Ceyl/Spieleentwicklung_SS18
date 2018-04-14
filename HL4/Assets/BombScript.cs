using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour {

    public float lifeTime;
    public float explosionForce;
    public float explosionRadius;
    public float upModifier;
    public LayerMask bombMask;
    public GameObject explosionEffect;
    private GameObject explosionEffectClone;

    // Use this for initialization
    void Start () {
        Invoke("explode", lifeTime);
	}
	
	// Update is called once per frame
	void explode () {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, bombMask);
        foreach(Collider collider in hitColliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            if (rigidbody != null)
                rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, upModifier);
        }
        explosionEffectClone = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        gameObject.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<Renderer>().enabled = false;
        Invoke("destroy", 2);
	}

    void destroy()
    {
        Destroy(explosionEffectClone);
        Destroy(gameObject);
    }
}
