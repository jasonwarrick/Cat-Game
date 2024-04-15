using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupParent : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] GameObject socket;

    bool itemPresent;

    public void ItemPickedUp() {
        itemPresent = false;
        socket.SetActive(true);
        socket.GetComponent<PickupSocket>().available = true;
        item.GetComponent<BoxCollider>().enabled = false;
        item.GetComponent<PickupInteract>().available = false;
    }

    public void ItemReturned() {
        if (!itemPresent) {
            itemPresent = true;
            item.GetComponent<PickupInteract>().available = true;
            item.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(Vector3.zero));
            socket.GetComponent<PickupSocket>().available = false;
            socket.SetActive(false);
            item.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
