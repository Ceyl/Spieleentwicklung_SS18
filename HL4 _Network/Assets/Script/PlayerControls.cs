using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Weapon {Off, Bomb, Projectile}

public class PlayerControls : Photon.PunBehaviour {

    #region public variables

    public Camera cam;
    public LayerMask interactionLayer;
    public float maxRange;
    public Transform handPosition;
    public GameObject bombPreFab;
    public GameObject projectilePreFab;
    public float throwForce;
    public float slowMoFactor;

    #endregion

    #region private variables

    private Rigidbody objInHand;
    private Weapon weapon;
    private Weapon lastUsedWeapon;
    private bool timeStop;

    #endregion

    #region MonoBehaviour CallBacks
    // Use this for initialization
    void Start () {
        weapon = Weapon.Bomb;
        timeStop = false;
	}
	
	// Update is called once per frame
	void Update () {


        if(weapon != Weapon.Off && Input.GetKeyDown(KeyCode.Tab))
        {
            switch (weapon)
            {
                case Weapon.Bomb:
                    weapon = Weapon.Projectile;
                    break;
                case Weapon.Projectile:
                    weapon = Weapon.Bomb;
                    break;
            }
        }

        if (weapon != Weapon.Off)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                weapon = Weapon.Bomb;
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                weapon = Weapon.Projectile;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (weapon != Weapon.Off)
            {
                lastUsedWeapon = weapon;
                weapon = Weapon.Off;
            }
            else
                weapon = lastUsedWeapon;
        }

            if (Input.GetKeyDown(KeyCode.T))
        {
                if (Time.timeScale == 1)
                    Time.timeScale = slowMoFactor;
                else
                    Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (objInHand == null)
            {
                RaycastHit hit;
                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                Debug.DrawLine(ray.origin, ray.GetPoint(maxRange));
                if (Physics.Raycast(ray, out hit, maxRange, interactionLayer))
                {
                    objInHand = hit.transform.GetComponent<Rigidbody>();
                    objInHand.isKinematic = true;
                    objInHand.transform.position = handPosition.position;
                    objInHand.transform.parent = handPosition;
                }
            }
        }

        if(weapon != Weapon.Off && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (objInHand != null)
            {
                objInHand.isKinematic = false;
                objInHand.transform.parent = null;
                objInHand.AddForce(cam.transform.forward * throwForce);
                objInHand = null;
            }
            else
            {
                switch (weapon)
                {
                    case Weapon.Bomb:
                        photonView.RPC("RPCInstantiateBomb", PhotonTargets.AllViaServer, handPosition.position, cam.transform.forward);
                        break;
                    case Weapon.Projectile:
                        photonView.RPC("RPCInstantiateProjectile", PhotonTargets.AllViaServer, handPosition.position, cam.transform.forward);
                        break;                   
                    default:
                        break;
                }
            }
        }
	}

    #endregion

    #region PunRPC

    [PunRPC]
    public void RPCInstantiateBomb(Vector3 pHandPosition, Vector3 pDirection)
    {
        GameObject weapon = Instantiate(bombPreFab, handPosition.position, Quaternion.identity);
        weapon.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
    }

    [PunRPC]
    public void RPCInstantiateProjectile(Vector3 pHandPosition, Vector3 pDirection)
    {
        GameObject weapon = Instantiate(projectilePreFab, handPosition.position, Quaternion.identity);
        weapon.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
    }

    #endregion
}
