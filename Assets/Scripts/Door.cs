using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door;
    [Header("Door Open Height")]
    public float top = 6f;
    public float bottom = -6f;
    [Header("Outside Triggers")]
    public bool isDoorTriggeredOutside = false;
    public OutsideDoorTrigger outsideDoorTrigger;
    [Header("Door Delay")]
    public float doorDelayTime;
    float delayCD;
    [Header("Door Speed")]
    public float doorOpenSpeed = 2.0f;


    float originalPos;
    public bool doorOpen = false;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        originalPos = door.transform.position.y;
        top = door.transform.position.y + 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDoorTriggeredOutside)
        {
            //doorTrigger.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            if (outsideDoorTrigger.openThisDoor == true)
            {
                if (door.transform.position.y <= top)
                {
                    delayCD = doorDelayTime;
                    door.transform.Translate(0, top * (Time.deltaTime * doorOpenSpeed), 0);
                }
            }
            else if (outsideDoorTrigger.openThisDoor == false)
            {
                if (door.transform.position.y >= originalPos)
                {
                    delayCD -= Time.deltaTime;
                    if (delayCD <= 0)
                    {
                        door.transform.Translate(0, bottom * (Time.deltaTime * doorOpenSpeed), 0);
                    }                
                }
            }
        }

        if (!isDoorTriggeredOutside)
        {
            if (doorOpen)
            {
                if (door.transform.position.y <= top)
                {
                    door.transform.Translate(0, top * (Time.deltaTime * doorOpenSpeed), 0);
                }
            }
            else if (doorOpen == false)
            {
                if (door.transform.position.y >= originalPos)
                {
                    door.transform.Translate(0, bottom * (Time.deltaTime * doorOpenSpeed), 0);
                }
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
