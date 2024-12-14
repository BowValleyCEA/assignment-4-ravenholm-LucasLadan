using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")//Checking if the player touched it
        {
            Debug.Log("Died");
            collision.gameObject.GetComponent<FPSController>().killPlayer();
        }
    }
}
