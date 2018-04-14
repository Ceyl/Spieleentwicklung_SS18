using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicatingHole : Hole {

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
            Object.Instantiate(other.gameObject);
    }
}
