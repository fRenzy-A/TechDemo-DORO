using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideDoorTrigger : MonoBehaviour
{
    public bool openThisDoor;
    // Update is called once per frame
    void Start()
    {

        openThisDoor = false;
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
}
