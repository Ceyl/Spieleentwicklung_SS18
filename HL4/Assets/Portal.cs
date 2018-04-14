using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    private static GameObject[] Portals;
    private static int PortalCounter;
    private bool PortalState;
    private int PortalNumber;

    public void Awake()
    {
        Portals = new GameObject[2];
        PortalCounter = 0;
    }
	// Use this for initialization
	void Start () {
        PortalState = false;
		if(Portals[PortalCounter] != null )
        {
            Destroy(Portals[PortalCounter]);
            Portals[PortalCounter] = gameObject;
            PortalNumber = PortalCounter;
        }
        else
        {
            Portals[PortalCounter] = gameObject;
            PortalNumber = PortalCounter;
        }
        PortalCounter = PortalCounter % 2;
        Invoke("activatePortal", 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if(PortalState)
        {
            if(Portals[0] != null && Portals[1] != null)
            {
                int otherPortalNumber = (PortalNumber + 1) % 2;
                other.gameObject.transform.position = Portals[otherPortalNumber].transform.position;
            }
        }
        else
        {
            activatePortal();
        }
    }

    private void activatePortal()
    {
        if (!PortalState)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            PortalState = true;
        }
    }
}
