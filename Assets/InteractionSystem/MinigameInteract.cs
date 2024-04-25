using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameInteract : Interactable
{
    public bool available = false;
    string meterName;

    [SerializeField] MinigameManager minigameManager;
    [SerializeField] GameObject required;

    void Start() {
        meterName = minigameManager.meterName;
    }

    override public bool CheckAvailable() {
        if (required == null || GameStateManager.instance.heldObject == required && FindObjectOfType<CatBrain>().IsMeterDanger(meterName)) {
            available = true;
        } else {
            available = false;
        }

        return available;
    }

    override public void Interact() {
        if (available) {
            minigameManager.StartMinigame();
            Debug.Log("interact fired");
        }
    }
}
