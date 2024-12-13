using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    [SerializeField] private GameObject door;
    [SerializeField] private bool reversed;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Grabbable")
        {
            if (reversed)
            {
                door.GetComponent<Collider>().isTrigger = false;
                door.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                door.GetComponent<Collider>().isTrigger = true;
                door.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Grabbable")
        {
            if (!reversed)
            {
                door.GetComponent<Collider>().isTrigger = false;
                door.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                door.GetComponent<Collider>().isTrigger = true;
                door.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
