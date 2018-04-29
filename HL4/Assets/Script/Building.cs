using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public float HitPoints;
    public LayerMask BuildingMask;

    void Start()
    {
        if (HitPoints <= 0)
            HitPoints = 1800;
    }

    // Update is called once per frame
    void Update () {
        if (HitPoints <= 0)
            Destroy(gameObject);
	}
}
