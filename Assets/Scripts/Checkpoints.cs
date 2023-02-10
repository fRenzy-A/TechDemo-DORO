using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    // Start is called before the first frame update
    Transform respawnPoint;
    void Start()
    {
        respawnPoint = GameObject.Find("Respawn Point").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            respawnPoint.transform.position = gameObject.transform.position;
        }
    }
}
