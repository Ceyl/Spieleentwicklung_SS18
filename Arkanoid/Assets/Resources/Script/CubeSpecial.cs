using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpecial : Cube {

    public void OnTriggerEnter(Collider other)
    {
        GameObject newBall = Instantiate(other.gameObject);
        //newBall.transform.position.Set(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
        base.OnTriggerEnter(other);
    }
}
