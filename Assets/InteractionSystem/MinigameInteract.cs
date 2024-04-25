using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameInteract : Interactable
{
    public bool available = false;
    string meterName;

    [SerializeField] MinigameManager minigameManager;
    [SerializeField] GameObject required;
    [SerializeField] PickupParent pickupParent;

    void Start() {
        meterName = minigameManager.meterName;

        Meter.meterDanger += InDanger;
        Meter.meterReset += MeterReset;
    }

    void InDanger(Meter meter) {
        if (meter.Name == meterName) {
            available = true;
        }
    }

    void MeterReset(Meter meter) {
        if (meter.Name == meterName) {
            available = false;
        }
    }

    override public bool CheckAvailable() {
        if (available) {
            if (required == null || GameStateManager.instance.heldObject == required && FindObjectOfType<CatBrain>().IsMeterDanger(meterName)) {
                available = true;
            }
        }

        return available;
    }

    override public void Interact() {
        if (available) {
            minigameManager.StartMinigame();
            pickupParent.TurnOffLight();
            Debug.Log("interact fired");
        }
    }
}
