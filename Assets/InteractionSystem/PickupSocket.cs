using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSocket : Interactable
{
    [SerializeField] GameObject pickupParent;
    public bool available = false;

    override public void Interact() {
        FindObjectOfType<PlayerInteraction>().DropItem(pickupParent);
        pickupParent.GetComponent<PickupParent>().ItemReturned();
    }
}
