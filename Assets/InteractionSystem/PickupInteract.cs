using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteract : Interactable
{
    [SerializeField] GameObject pickupParent;

    override public void Interact() {
        bool isAvailable = FindObjectOfType<PlayerInteraction>().HoldItem(this.gameObject);
        if (isAvailable) { pickupParent.GetComponent<PickupParent>().ItemPickedUp(); }
    }
}
