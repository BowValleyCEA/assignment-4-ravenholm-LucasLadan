using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RespawnSystem : MonoBehaviour
{

    public UnityEvent Respawn;
    [SerializeField] private GameObject pauseUI;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            onDeath();
        }

        if (Input.GetButtonDown("Pause"))
        {
            pauseUI.SetActive(true);
            FindAnyObjectByType<FPSController>().setResume(false);
        }
    }
    
    public void onDeath()
    {
        Respawn.Invoke();
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
