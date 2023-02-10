using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door;
    public float top = 6f;
    public float bottom = -6f;
    float originalPos;
    public bool doorOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = door.transform.position.y;
        top = door.transform.position.y + 4;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (doorOpen)
        {
            if (door.transform.position.y <= top)
            {
                door.transform.Translate(0, top * Time.deltaTime, 0);
            }
        }
        else if (doorOpen == false)
        { 
            if (door.transform.position.y >= originalPos)
            {
                door.transform.Translate(0, bottom * Time.deltaTime, 0);
            }           
        }      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorOpen = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        doorOpen = false;
    }
}
