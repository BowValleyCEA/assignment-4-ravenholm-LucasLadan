using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryParticles : MonoBehaviour
{

    [SerializeField] private GameObject particles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            particles.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            particles.SetActive(false);
        }
    }
}
