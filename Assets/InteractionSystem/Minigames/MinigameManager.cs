using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public delegate void MinigameStarted(bool isStarting);
    public static MinigameStarted minigameStarted;

    [SerializeField] string meterName;

    bool isStarted = false;
    
    [SerializeField] GameObject cam;
    GameObject player;
    CatBrain catBrain;

    void Start() {
        cam.SetActive(false);
        
        player = FindObjectOfType<FirstPersonMovement>().gameObject;
        catBrain = FindObjectOfType<CatBrain>();
    }

    public void StartMinigame() {
        isStarted = true;

        minigameStarted.Invoke(isStarted);

        cam.SetActive(isStarted);

        Debug.Log("mingame started");

        player.SetActive(false);
    }

    void Update() {
        if (isStarted && Input.GetKeyDown(KeyCode.Space)) {
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

    public void CompleteMinigame() {
        catBrain.ResetMeter(meterName);
        StopMinigame();
    }
}
