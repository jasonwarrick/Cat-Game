using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupParent : MonoBehaviour
{
    [SerializeField] string meterName;
    [SerializeField] public GameObject item;
    [SerializeField] GameObject socket;
    [SerializeField] Light dangerLight;

    bool itemPresent;

    void Start() {
        dangerLight.enabled = true;
        dangerLight.enabled = false;

        Meter.meterDanger += TriggerLight;
    }

    public void ItemPickedUp() {
        dangerLight.enabled = false;
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

    void TriggerLight(Meter meter) {
        if (meter.Name == meterName) {
            dangerLight.enabled = true;
        }   
    }
}
