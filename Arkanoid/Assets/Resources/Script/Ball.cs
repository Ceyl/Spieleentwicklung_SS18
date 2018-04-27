using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

    public GameObject paddle;
    public float zSpeed;
    public float xMax;
    public Text score;
    public Text gameOver;
    public Text congratulations;
    protected Vector3 velocity;
    private bool started;

    // Use this for initialization
    void Start()
    {
        velocity = new Vector3(0, 0, 0);
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                velocity.Set(0, 0, zSpeed);
                started = true;
            }
            else
            {
                transform.position = new Vector3(paddle.transform.position.x, transform.position.y, paddle.transform.position.z + 1.01f);
            }
        }
        else
        {
            transform.position += velocity * Time.deltaTime;
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Cube");
            if (gameObjects.Length == 0)
                congratulations.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Paddle":
                SetVelocityPaddle(other);
                break;
            case "Wall":
                velocity.Set(-velocity.x, velocity.y, velocity.z);
                break;
            case "WallBack":
                velocity.Set(velocity.x, velocity.y, -velocity.z);
                break;
            case "Cube":
                IncreaseScore();
                SetVelocityCube(other);
                break;
            case "Ball":
                SetVelocityPaddle(other);
                break;
            case "Outside":
                gameObject.GetComponent<Renderer>().enabled = false;
                CheckForRestart();
                break;
            default:
                return;
        }
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void SetVelocityPaddle (Collider other)
    {
        float maxDist = transform.localScale.x * 0.5f + other.transform.localScale.x * 0.5f;
        float actualDist = transform.position.x - other.transform.position.x;
        float normalizedDistX = actualDist / maxDist;
        maxDist = transform.localScale.z * 0.5f + other.transform.localScale.z * 0.5f;
        actualDist = transform.position.z - other.transform.position.z;
        float normalizedDistZ = actualDist / maxDist;
        velocity.Set(normalizedDistX * xMax, velocity.y, normalizedDistZ * zSpeed);
    }

    private void SetVelocityCube(Collider other)
    {
        float localScaleCubeX = other.transform.localScale.x * 0.5f;
        float localScaleCubeZ = other.transform.localScale.z * 0.5f;

        if((gameObject.transform.position.z <= other.transform.position.z + localScaleCubeZ) && (gameObject.transform.position.z >= other.transform.position.z - localScaleCubeZ))
            velocity.Set(-velocity.x, velocity.y, velocity.z);
        else if ((gameObject.transform.position.x <= other.transform.position.x + localScaleCubeX) && (gameObject.transform.position.x >= other.transform.position.x - localScaleCubeX))
            velocity.Set(velocity.x, velocity.y, -velocity.z);
        else
            velocity.Set(-velocity.x, velocity.y, -velocity.z);
    }

    private void CheckForRestart ()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Ball");
        if (gameObjects.Length <= 1)
        {
            gameOver.enabled = true;
            Invoke("GameOver", 5);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void GameOver ()
    {
        SceneManager.LoadScene("main");
    }

    private void IncreaseScore ()
    {
        int scoreValue = 0;
        int.TryParse(score.text, out scoreValue);
        scoreValue++;
        score.text = scoreValue.ToString();
    }
}
