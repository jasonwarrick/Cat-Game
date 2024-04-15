using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public delegate void MinigameStarted(bool isStarting);
    public static MinigameStarted minigameStarted;

    [SerializeField] GameObject cam;
    GameObject player;

    bool isStarted = false;

    void Start() {
        cam.SetActive(false);
        
        player = FindObjectOfType<FirstPersonMovement>().gameObject;
    }

    public void StartMinigame() {
        isStarted = true;

        minigameStarted.Invoke(isStarted);

        cam.SetActive(isStarted);

        Debug.Log("mingame started");

        player.SetActive(false);
    }

    void Update() {
        if (isStarted && Input.GetMouseButtonDown(0)) {
            StopMinigame();
        }
    }

    public void StopMinigame() {
        isStarted = false;
        player.SetActive(true);
        
        minigameStarted.Invoke(isStarted);

        cam.SetActive(isStarted);
        Debug.Log("mingame ended");
    }
}
