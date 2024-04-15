using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteract : Interactable
{
    bool available = false;

    [SerializeField] GameObject pickupParent;

    override public bool CheckAvailable() {
        if (GameStateManager.instance.heldObject == null) {
            available = true;
        } else {
            available = false;
        }

        return available;
    }

    override public void Interact() {
        if (available) { 
            FindObjectOfType<PlayerInteraction>().HoldItem(this.gameObject);
            pickupParent.GetComponent<PickupParent>().ItemPickedUp();
        }
    }
}
