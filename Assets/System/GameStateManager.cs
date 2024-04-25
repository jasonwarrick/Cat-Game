using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public delegate void TimeChanged(int hours, int minutes);
    public static TimeChanged timeChanged;

    [SerializeField] float timeFactor; // The higher the factor, the quicker time passes
    [SerializeField] int[] startTime = new int[2];
    
    int[] time = new int[2] { 10, 0 } ; // { Hours, Minutes }
    float timeCounter = 0f;

    bool inMinigame = false;
    bool paused = false;
    public GameObject heldObject;

    void Start() {
        instance = this;
        time = startTime;
        timeChanged.Invoke(time[0], time[1]);

        MinigameManager.minigameStarted += SetInMinigame;
        Meter.meterFull += GameLost;
        Meter.meterDanger += Danger;
    }
    
    void Update() {
        timeCounter += Time.deltaTime;

        if (timeCounter >= 1f / timeFactor) {
            timeCounter = 0f;
            AddMinute();
        }
    }

    void AddMinute() {
        time[1]++;

        if (time[1] >= 60) {
            time[1] = 0;
            time[0]++;

            if (time[0] > 12) {
                time[0] = 1;
            }
        }

        timeChanged.Invoke(time[0], time[1]);
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
