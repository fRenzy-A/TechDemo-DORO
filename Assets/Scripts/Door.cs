using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door;
    public float top;

    // Start is called before the first frame update
    void Start()
    {
        top = door.transform.position.y + 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (door.transform.position.y <= top)
        {
            door.transform.Translate(0, 3 * Time.deltaTime, 0);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
