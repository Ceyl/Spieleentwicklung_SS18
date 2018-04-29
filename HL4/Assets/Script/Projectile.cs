using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float Damage;
    public float Range;

    private float NormalizedDamage;
    private Vector3 StartPosition;

	// Use this for initialization
	void Start () {
        StartPosition = transform.position;
        NormalizedDamage = Damage / Range;
	}
	
	// Update is called once per frame
	void Update () {

        if ((transform.position - StartPosition).magnitude >= Range * 0.9f)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else if ((transform.position - StartPosition).magnitude >= 2 * Range)
            Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Building>() != null && (transform.position - StartPosition).magnitude < Range)
            other.GetComponent<Building>().HitPoints -= NormalizedDamage * (Range - (transform.position - StartPosition).magnitude);
        Destroy(gameObject);
    }
}
