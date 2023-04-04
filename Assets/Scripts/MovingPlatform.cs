using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 0.5f;
    public float leftMax = -6f;
    public float rightMax = 6f;
    float originalPos;

    Vector3 left;
    Vector3 right;

    public GameObject players;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = gameObject.transform.position.x;
        left = new Vector3(originalPos + leftMax,gameObject.transform.position.y,gameObject.transform.position.z);
        right = new Vector3(originalPos + rightMax, gameObject.transform.position.y, gameObject.transform.position.z);

        //players = GameObject.Find("Player").GetComponent<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(left, right, time);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            players.transform.parent = gameObject.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            players.transform.parent = null;
        }
        
    }
}
