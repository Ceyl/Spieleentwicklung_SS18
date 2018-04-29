using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : Hole {

    void OnTriggerEnter (Collider other)
    {
        if (holeMask == (holeMask | 1<<other.gameObject.layer))
        {
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.mass += other.gameObject.GetComponent<Rigidbody>().mass / 10;
                SphereCollider sphereCollider = gameObject.GetComponent<SphereCollider>();
                sphereCollider.radius += rigidbody.mass / 2;
                transform.localScale += new Vector3(1, 1, 1);
                Destroy(other.gameObject);
            }
        }
    }

}
