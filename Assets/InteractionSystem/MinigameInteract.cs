using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameInteract : Interactable
{
    public bool available = false;

    [SerializeField] MinigameManager minigameManager;
    [SerializeField] GameObject required;

    override public bool CheckAvailable() {
        if (required == null || GameStateManager.instance.heldObject == required) {
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
