using Invector.CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPoint : Photon.PunBehaviour {

    public float HitPoints;

    void Start()
    {
        if (HitPoints <= 0)
            HitPoints = 1800;
    }

    // Update is called once per frame
    void Update () {
        if (HitPoints <= 0)
        {
            if (gameObject.layer == 8 && photonView.isMine)
            {
                GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
                gameManager.CrossHair.SetActive(false);
                gameManager.GameOver.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            Destroy(gameObject);
        }
	}
}
