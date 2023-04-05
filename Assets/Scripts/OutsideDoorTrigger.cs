using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideDoorTrigger : MonoBehaviour
{
    public bool openThisDoor = false;
    // Update is called once per frame
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            openThisDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        openThisDoor = false;
    }
}
