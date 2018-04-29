using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicatingHole : Hole {

    void OnTriggerEnter(Collider other)
    {
        if (holeMask == (holeMask | 1 << other.gameObject.layer))
        
            Instantiate(other.gameObject);
    }
}
