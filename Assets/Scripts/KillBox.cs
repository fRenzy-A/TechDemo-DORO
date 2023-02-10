using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KillBox : MonoBehaviour
{
    Transform respawnPoint;
    Transform players;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.Find("Player").GetComponent<Transform>();
        respawnPoint = GameObject.Find("Respawn Point").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Teleport");
        if (other.gameObject.CompareTag("Player"))
        {
            players.transform.position = respawnPoint.transform.position;
        }
    }
}
