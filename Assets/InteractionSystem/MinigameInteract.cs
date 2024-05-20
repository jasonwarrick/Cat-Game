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

        Meter.meterReset += MeterReset;
    }

    void OnDestroy () {
        Meter.meterReset -= MeterReset;
    }

    void MeterReset(Meter meter) {
        if (meter.Name == meterName) {
            available = false;
        }
    }

    override public bool CheckAvailable() {
        if (GameStateManager.instance.heldObject != null && GameStateManager.instance.heldObject.name == required.name && FindObjectOfType<CatBrain>().IsMeterDanger(meterName)) {
            available = true;
        } else {
            available = false;
        }

        return available;
    }

    override public void Interact() {
        if (available) {
            minigameManager.StartMinigame();

            if (pickupParent != null) {
                pickupParent.TurnOffLight();
            }

            // Debug.Log("interact fired");
        }
    }
}
