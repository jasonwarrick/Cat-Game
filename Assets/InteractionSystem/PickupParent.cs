using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupParent : MonoBehaviour
{
    [SerializeField] string meterName;
    [SerializeField] public GameObject item;
    [SerializeField] GameObject socket;
    [SerializeField] Transform minigamePoint;
    [SerializeField] Transform lightPoint;
    [SerializeField] Light dangerLight;

    bool itemPresent;

    void OnEnable() {
        TurnOffLight();

        Meter.meterDanger += TriggerLight;
    }

    void OnDestroy() {
        Meter.meterDanger -= TriggerLight;
    }

    public void ItemPickedUp() {
        dangerLight.transform.parent = minigamePoint;
        dangerLight.transform.position = minigamePoint.position;
        itemPresent = false;
        socket.SetActive(true);
        item.GetComponent<BoxCollider>().enabled = false;
    }

    public void ItemReturned() {
        if (!itemPresent) {
            itemPresent = true;
            item.transform.parent = transform;
            item.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(Vector3.zero));
            socket.SetActive(false);
            item.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void TurnOffLight() {
        dangerLight.enabled = false;
        dangerLight.transform.parent = transform;
        dangerLight.transform.position = lightPoint.position;
    }

    void TriggerLight(Meter meter) {
        if (meter.Name == meterName) {
            dangerLight.enabled = true;
        }   
    }
}
