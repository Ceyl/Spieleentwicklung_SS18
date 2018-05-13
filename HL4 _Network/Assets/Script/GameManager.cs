using Invector.CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.PunBehaviour {

    public GameObject CrossHair;
    public GameObject GameOver;
	// Use this for initialization
	void Start () {
        SpawnPlayer();
    }
	
    public void SpawnPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Transform spawnPoint = spawnPoint = GameObject.Find("SpawnPoint_Gordon").transform;
        GameObject gordon = PhotonNetwork.Instantiate("Gordon", spawnPoint.position, spawnPoint.rotation, 0);
        gordon.GetComponentInChildren<vThirdPersonCamera>().enabled = true;
        gordon.GetComponentInChildren<Camera>().enabled = true;
        gordon.GetComponent<vThirdPersonController>().enabled = true;
        gordon.GetComponent<vThirdPersonInput>().enabled = true;
        gordon.GetComponent<BuildControl>().enabled = true;
        gordon.GetComponent<PlayerControls>().enabled = true;
        CrossHair.SetActive(true);
        GameOver.SetActive(false);
    }


    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }
}
