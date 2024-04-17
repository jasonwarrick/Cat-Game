using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    bool inMinigame = false;
    bool paused = false;
    public GameObject heldObject;

    void Start() {
        instance = this;

        MinigameManager.minigameStarted += SetInMinigame;
        Meter.meterFull += GameLost;
        Meter.meterDanger += Danger;
    }

    public void SetInMinigame(bool isInMinigame) {
        inMinigame = isInMinigame;

        // Debug.Log(inMinigame);
    }

    public bool GetInMinigame() { return inMinigame; }

    public void PauseGame() {
        if (!paused) { 
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }

        paused = !paused;
    }

    void GameLost(Meter meter) {
        Debug.Log("Lost game due to " + meter.Name);
    }

    void Danger(Meter meter) {
        Debug.Log(meter.Name + " is in danger");
    }
}
