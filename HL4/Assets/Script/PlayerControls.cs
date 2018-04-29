using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Weapon {Off, Bomb, Projectile, BouncingBall, BlackHole, DuplicatingHole, PullingHole}

public class PlayerControls : MonoBehaviour {

    #region public variables

    public Camera cam;
    public LayerMask interactionLayer;
    public float maxRange;
    public Transform handPosition;
    public GameObject bombPreFab;
    public GameObject projectilePreFab;
    public GameObject bouncingBallPreFab;
    public GameObject blackHolePreFab;
    public GameObject duplicatingHolePreFab;
    public GameObject pullingHolePreFab;
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
                    weapon = Weapon.BouncingBall;
                    break;
                case Weapon.BouncingBall:
                    weapon = Weapon.BlackHole;
                    break;
                case Weapon.BlackHole:
                    weapon = Weapon.DuplicatingHole;
                    break;
                case Weapon.DuplicatingHole:
                    weapon = Weapon.PullingHole;
                    break;
                case Weapon.PullingHole:
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
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                weapon = Weapon.BouncingBall;
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                weapon = Weapon.BlackHole;
            else if (Input.GetKeyDown(KeyCode.Alpha5))
                weapon = Weapon.DuplicatingHole;
            else if (Input.GetKeyDown(KeyCode.Alpha6))
                weapon = Weapon.PullingHole;
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
                        GameObject bomb = Instantiate(bombPreFab, handPosition.position, Quaternion.identity);
                        bomb.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
                        break;
                    case Weapon.Projectile:
                        GameObject projectile = Instantiate(projectilePreFab, handPosition.position, Quaternion.identity);
                        projectile.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
                        break;
                    case Weapon.BouncingBall:
                        GameObject bouncingBall = Instantiate(bouncingBallPreFab, handPosition.position, Quaternion.identity);
                        bouncingBall.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
                        break;
                    case Weapon.BlackHole:
                        GameObject blackHole = Instantiate(blackHolePreFab, handPosition.position, Quaternion.identity);
                        blackHole.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
                        break;
                    case Weapon.DuplicatingHole:
                        GameObject duplicatingHole = Instantiate(duplicatingHolePreFab, handPosition.position, Quaternion.identity);
                        duplicatingHole.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
                        break;                  
                    case Weapon.PullingHole:
                        GameObject pullingHole = Instantiate(pullingHolePreFab, handPosition.position, Quaternion.identity);
                        pullingHole.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
                        break;                    
                    default:
                        break;
                }
            }
        }
	}

    #endregion
}
