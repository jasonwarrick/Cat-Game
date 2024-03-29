using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteract : Interactable
{
    [SerializeField] GameObject pickupParent;

    override public void Interact() {
        FindObjectOfType<PlayerInteraction>().HoldItem(this.gameObject);
        pickupParent.GetComponent<PickupParent>().ItemPickedUp();
    }
}
