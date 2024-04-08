using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    bool inMinigame = false;
    bool paused = false;

    // Start is called before the first frame update
    void Start() {
        instance = this;

        MinigameManager.minigameStarted += SetInMinigame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInMinigame(bool isInMinigame) {
        inMinigame = isInMinigame;

        Debug.Log(inMinigame);
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
}
