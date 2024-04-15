using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameInteract : Interactable
{
    [SerializeField] MinigameManager minigameManager;
    [SerializeField] GameObject required;
    public bool available = false;

    void Update() {
        if (required == null || GameStateManager.instance.heldObject == required) {
            available = true;
        }
    }

    override public void Interact() {
        if (available) {
            minigameManager.StartMinigame();
            Debug.Log("interact fired");
        }
    }
}
