using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BuildType { Nothing, Floor, Wall, Ramp}
public class BuildControl : Photon.PunBehaviour {

    #region Public Variables

    // Building Prefabs
    public GameObject Square;
    public Material Material;
    public Material MaterialTransparent;

    public LayerMask BuildingLayer;
    public float GridSize;

    #endregion

    #region private variables

    private vThirdPersonCamera Camera;
    private BuildType BuildType;
    private bool BuildModeActive;

    #endregion


    #region MonoBehaviour CallBacks
    // Use this for initialization
    void Start () {
        Camera = FindObjectOfType<vThirdPersonCamera>();
        BuildType = BuildType.Nothing;
        Square = Instantiate(Square);
        Square.GetComponent<BoxCollider>().enabled = false;
        Square.GetComponent<Renderer>().sharedMaterial = MaterialTransparent;
        Square.transform.localScale = new Vector3(GridSize, 0.05f, GridSize);
        Square.SetActive(false);
        BuildModeActive = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            BuildModeActive = !BuildModeActive;
            Square.SetActive(false);
        }

        if (BuildModeActive)
        { 
            if (Input.GetKeyUp(KeyCode.Alpha1))
                ChangeBuildType(BuildType.Floor);
            else if (Input.GetKeyUp(KeyCode.Alpha2))
                ChangeBuildType(BuildType.Wall);
            else if (Input.GetKeyUp(KeyCode.Alpha3))
                ChangeBuildType(BuildType.Ramp);
            if (BuildType != BuildType.Nothing)
            {
                if (BuildingCheck() && Input.GetKeyUp(KeyCode.Mouse0))
                {
                    photonView.RPC("RPCBuild", PhotonTargets.AllBuffered, Square.transform.position, Square.transform.rotation, Square.transform.localScale);
                }
            }
        }

    }

    #endregion

    #region Helpers

    private void ChangeBuildType(BuildType pBuildType)
    {
        if (pBuildType == BuildType)
        {
            BuildType = BuildType.Nothing;
            Square.SetActive(false);
        }
        else
        {
            BuildType = pBuildType;
            PythagorasRamp();
            Square.SetActive(true);
        }
    }

    private void PythagorasRamp()
    {
        if (BuildType == BuildType.Ramp)
            Square.transform.localScale = new Vector3(Mathf.Sqrt(GridSize * GridSize + GridSize * GridSize), 0.05f, GridSize);
        else
            Square.transform.localScale = new Vector3(GridSize, 0.05f, GridSize);
    }

    private Vector3 SnapToGrid(Vector3 pHitPoint)
    {
        float snapPointX = GetSnapPoint(pHitPoint.x);
        float snapPointY = GetSnapPoint(pHitPoint.y);
        float snapPointZ = GetSnapPoint(pHitPoint.z);

        //Get the nearest 90° rotation of y-Achsis to rotate the component into this position
        var rotation = Camera.transform.rotation.eulerAngles;
        rotation.x = 0;
        rotation.y = Mathf.Round(rotation.y / 90) * 90 - 90;
        rotation.z = 0;

        if (BuildType == BuildType.Wall)
        {
            if (Mathf.Abs(rotation.y) == 180 || Mathf.Abs(rotation.y) == 0)
                snapPointX += GridSize / 2;
            else
                snapPointZ += GridSize / 2;
            rotation.x = 90;
            rotation.z = 90;
            snapPointY += GridSize / 2;
        }

        if (BuildType == BuildType.Ramp)
        {
            rotation.z = 45;
            snapPointY += GridSize / 2;
        }
        Square.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        Vector3 gridPosition = new Vector3(snapPointX, snapPointY, snapPointZ);

        return gridPosition;
    }

    private float GetSnapPoint(float pHitPointCoord)
    {
        float snapPoint = pHitPointCoord + ((GridSize - pHitPointCoord) % GridSize);

        if (!(snapPoint + GridSize / 2 >= pHitPointCoord)) snapPoint += GridSize;
        else if (!(snapPoint - GridSize / 2 < pHitPointCoord)) snapPoint -= GridSize;

        return snapPoint;
    }

    private bool BuildingCheck()
    {
        Vector3 position = Camera.transform.position + Camera.transform.forward * (GridSize + 2);
        position = SnapToGrid(position);
        Square.transform.position = position;

        Collider[] boxColliders = Physics.OverlapBox(position, Square.transform.localScale / 2, Square.transform.rotation, BuildingLayer);
        if (boxColliders.Length != 0)
        {
            foreach (Collider collider in boxColliders)
            {
                if (collider.transform.position != position)
                {
                    Square.GetComponent<Renderer>().sharedMaterial.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    return true;
                }
            }
        }
        Square.GetComponent<Renderer>().sharedMaterial.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        return false;
    }

    #endregion

    #region PunRPC

    [PunRPC]
    public void RPCBuild(Vector3 pPosition, Quaternion pRotation, Vector3 pLocalScale)
    {
        GameObject newSquare = Instantiate(Square);
        newSquare.transform.position = pPosition;
        newSquare.transform.rotation = pRotation;
        newSquare.transform.localScale = pLocalScale;
        newSquare.GetComponent<Renderer>().sharedMaterial = Material;
        newSquare.GetComponent<BoxCollider>().enabled = true;
        newSquare.GetComponent<HitPoint>().enabled = true;
    }

    #endregion
}
