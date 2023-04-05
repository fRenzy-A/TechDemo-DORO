using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Platform Speed")]
    public float speed = 0.5f;
    [Header("Platform Distance:Horizontal")]
    public bool isPlatformMovingHorizontally = false;
    public float leftMax;
    public float rightMax;
    [Header("Platform Distance:Vertical")]
    public bool isPlatformMovingVertically = false;
    public float topMax;
    public float bottomMax;

    float originalPos;

    Vector3 left;
    Vector3 right;
    Vector3 up;
    Vector3 down;
    public GameObject players;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = gameObject.transform.position.x;
        left = new Vector3(originalPos + leftMax,gameObject.transform.position.y,gameObject.transform.position.z);
        right = new Vector3(originalPos + rightMax, gameObject.transform.position.y, gameObject.transform.position.z);
        up = new Vector3(originalPos + gameObject.transform.position.x, topMax, gameObject.transform.position.z);
        down = new Vector3(originalPos + gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        //players = GameObject.Find("Player").GetComponent<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        if (isPlatformMovingHorizontally)
        {
            transform.position = Vector3.Lerp(left, right, time);
        }
        else if (isPlatformMovingVertically)
        {
            transform.position = Vector3.Lerp(up, down, time);
        }
        else
        {
            transform.position = Vector3.Lerp(left, right, time);
            transform.position = Vector3.Lerp(up, down, time);
        }
        
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
