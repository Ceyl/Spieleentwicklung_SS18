using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public Vector3 speed;
    public Transform floor;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float dir = Input.GetAxis("Horizontal");
        float newPos = transform.position.x + dir * speed.x * Time.deltaTime;
        float paddleScale = transform.localScale.x;
        float floorScale = floor.localScale.x;
        float maxPos = floorScale * 10 * 0.5f - paddleScale * 1 * 0.5f;
        float pos = Mathf.Clamp(newPos, -maxPos, maxPos);
        transform.position = new Vector3(pos, transform.position.y, transform.position.z);

    }
}
