using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnSystem : MonoBehaviour
{

    public UnityEvent Respawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            onDeath();
        }
    }
    // Update is called once per frame
    public void onDeath()
    {
        Respawn.Invoke();
    }
}
