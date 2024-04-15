using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSocket : Interactable
{
    public bool available;

    [SerializeField] GameObject pickupParent;

    override public bool CheckAvailable() {
        if (GameStateManager.instance.heldObject == pickupParent.GetComponent<PickupParent>().item) {
            available = true;
        } else {
            available = false;
        }

        return available;
    }

    override public void Interact() {
        if (available) {
            FindObjectOfType<PlayerInteraction>().DropItem(pickupParent);
            pickupParent.GetComponent<PickupParent>().ItemReturned();
        }
    }
}
