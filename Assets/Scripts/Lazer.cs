using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private LayerMask hitLayer;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.up, out hit,
            10000f))

        {
            if (hit.collider.gameObject.GetComponent<FPSController>() != null)
            {
                hit.collider.gameObject.GetComponent<FPSController>().killPlayer();
            }
        }
    }
}
