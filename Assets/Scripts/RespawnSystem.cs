using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnSystem : MonoBehaviour
{

    public UnityEvent Respawn;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            onDeath();
        }
    }
    
    public void onDeath()
    {
        Respawn.Invoke();
    }
}
