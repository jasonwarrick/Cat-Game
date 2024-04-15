using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameInteract : Interactable
{
    [SerializeField] MinigameManager minigameManager;
    [SerializeField] GameObject required;

    override public void Interact() {
        if (required == null || FindObjectOfType<PlayerInteraction>().GetHeldItem() == required) {
            minigameManager.StartMinigame();
            Debug.Log("interact fired");
        }
    }
}
