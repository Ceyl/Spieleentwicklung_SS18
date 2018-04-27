using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	public byte numberOfHits;

    public void OnTriggerEnter(Collider other)
    {
        if (numberOfHits > 1)
            numberOfHits--;
        else
            Destroy(gameObject);

        Renderer renderer = gameObject.GetComponent<Renderer>();
        switch (numberOfHits)
        {
            case 1:
                renderer.material = Resources.Load("Material/Material4", typeof(Material)) as Material;
                break;
            case 2:
                renderer.material = Resources.Load("Material/Material3", typeof(Material)) as Material;
                break;
            case 3:
                renderer.material = Resources.Load("Material/Material2", typeof(Material)) as Material;
                break;
            case 4:
                renderer.material = Resources.Load("Material/Material1", typeof(Material)) as Material;
                break;
        }
    }
}
