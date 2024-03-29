using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSocket : Interactable
{
    [SerializeField] GameObject pickupParent;

    override public void Interact() {
        FindObjectOfType<PlayerInteraction>().DropItem(pickupParent);
        pickupParent.GetComponent<PickupParent>().ItemReturned();
    }
}
