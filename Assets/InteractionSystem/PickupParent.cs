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
        item.GetComponent<BoxCollider>().enabled = false;
    }

    public void ItemReturned() {
        if (!itemPresent) {
            itemPresent = true;
            item.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(Vector3.zero));
            socket.SetActive(false);
            item.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
