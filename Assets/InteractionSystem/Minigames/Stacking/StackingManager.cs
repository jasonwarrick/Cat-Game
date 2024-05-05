using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingManager : MonoBehaviour
{
    [SerializeField] float chargeRate;
    [SerializeField] Vector3 chargeDirection;

    int poopNeeded = 5;
    int poopMade = 0;
    bool charging = false;
    
    [SerializeField] GameObject poop;
    [SerializeField] GameObject scoopObj;
    [SerializeField] MinigameManager minigameManager;

    void OnEnable() {
        
    }

    void OnDisable() {
        
    }

    void Update() {
        Scored();

        if (Input.GetMouseButton(0) && !charging) {
            charging = true;
        }
    }

    public void Missed() {
        AudioManager.instance.MinigameLost();
    }

    public void Scored() {
        poopMade++;

        if (poopMade >= poopNeeded) {
            AudioManager.instance.MinigameWon();
            minigameManager.CompleteMinigame();
        }
    }
}
