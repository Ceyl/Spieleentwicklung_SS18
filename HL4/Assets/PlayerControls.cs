using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon {Bomb, BlackHole, DuplicatingHole, BouncingBall, PullingHole, Portal}

public class PlayerControls : MonoBehaviour {

    public Camera cam;
    public LayerMask interactionLayer;
    public float maxRange;
    private Rigidbody objInHand;
    public Transform handPosition;
    public GameObject bombPreFab;
    public GameObject blackHolePreFab;
    public GameObject duplicatingHolePreFab;
    public GameObject bouncingBallPreFab;
    public GameObject pullingHolePreFab;
    public GameObject portalPrefab;
    public float throwForce;
    public float slowMoFactor;
    private Weapon weapon;
    private bool timeStop;

	// Use this for initialization
	void Start () {
        weapon = Weapon.Bomb;
        timeStop = false;
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            switch (weapon)
            {
                case Weapon.Bomb:
                    weapon = Weapon.BlackHole;
                    break;
                case Weapon.BlackHole:
                    weapon = Weapon.DuplicatingHole;
                    break;
                case Weapon.DuplicatingHole:
                    weapon = Weapon.BouncingBall;
                    break;
                case Weapon.BouncingBall:
                    weapon = Weapon.PullingHole;
                    break;
                case Weapon.PullingHole:
                    weapon = Weapon.Portal;
                    break;
                case Weapon.Portal:
                    weapon = Weapon.Bomb;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            weapon = Weapon.Bomb;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            weapon = Weapon.BlackHole;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            weapon = Weapon.DuplicatingHole;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            weapon = Weapon.BouncingBall;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            weapon = Weapon.PullingHole;
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            weapon = Weapon.Portal;

        if (Input.GetKeyDown(KeyCode.T))
        {
                if (Time.timeScale == 1)
                    Time.timeScale = slowMoFactor;
                else
                    Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (timeStop)
                foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Interaction"))
                {
                    gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    timeStop = false;
                }
            else
                foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Interaction"))
                {
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    timeStop = true;
                }
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

        if(Input.GetKeyDown(KeyCode.Mouse0))
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
                    case Weapon.BlackHole:
                        GameObject blackHole = Instantiate(blackHolePreFab, handPosition.position, Quaternion.identity);
                        blackHole.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
                        break;
                    case Weapon.DuplicatingHole:
                        GameObject duplicatingHole = Instantiate(duplicatingHolePreFab, handPosition.position, Quaternion.identity);
                        duplicatingHole.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
                        break;
                    case Weapon.BouncingBall:
                        GameObject bouncingBall = Instantiate(bouncingBallPreFab, handPosition.position, Quaternion.identity);
                        bouncingBall.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
                        break;
                    case Weapon.PullingHole:
                        GameObject pullingHole = Instantiate(pullingHolePreFab, handPosition.position, Quaternion.identity);
                        pullingHole.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
                        break;
                    case Weapon.Portal:
                        GameObject portal = Instantiate(portalPrefab, handPosition.position, Quaternion.identity);
                        portal.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);
                        break;
                    default:
                        break;
                }
            }
        }
	}
}
