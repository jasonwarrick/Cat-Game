using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteract : Interactable
{
    [SerializeField] GameObject pickupParent;
    public bool available = false;

    override public void Interact() {
        available = FindObjectOfType<PlayerInteraction>().HoldItem(this.gameObject);
        if (available) { pickupParent.GetComponent<PickupParent>().ItemPickedUp(); }
    }
}
