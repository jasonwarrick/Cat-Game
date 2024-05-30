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

    bool itemPresent = true;
    bool lightNeeded = false;
    bool meterCompleted = false;

    void OnEnable() {
        TurnOffLight();

        Meter.meterDanger += TriggerLight;
        Meter.meterReset += CompleteLight;
    }

    void OnDestroy() {
        Meter.meterDanger -= TriggerLight;
        Meter.meterReset -= CompleteLight;
    }

    public void ItemPickedUp() {
        if (lightNeeded) {
            MoveToMiningame();
            // pickupEvent.Invoke(item.name);
        }

        itemPresent = false;
        socket.SetActive(true);
        item.GetComponent<BoxCollider>().enabled = false;
    }

    void Update() {
        if (GameStateManager.instance.heldObject != null && GameStateManager.instance.heldObject.name != item.name && lightNeeded) {
            TurnOffLight();
        } else if (lightNeeded) {
            TurnOnLight();
        }
    }

    public void ItemReturned() {
        if (!itemPresent) {
            itemPresent = true;
            item.transform.parent = transform;
            item.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(Vector3.zero));
            socket.SetActive(false);
            item.GetComponent<BoxCollider>().enabled = true;

            if (lightNeeded) {
                MoveToPickup();
            }
        }
    }

    public void TurnOffLight() {
        dangerLight.enabled = false;
    }

    public void TurnOnLight() {
        dangerLight.enabled = true;
    }

    void TriggerLight(Meter meter) {
        if (meter.Name == meterName) {
            lightNeeded = true;
            dangerLight.enabled = true;

            if (itemPresent) {
                MoveToPickup();
            } else {
                MoveToMiningame();
            }
            
        }   
    }

    void MoveToPickup() {
        dangerLight.transform.parent = transform;
        dangerLight.transform.position = lightPoint.position;
    }

    void MoveToMiningame() {
        dangerLight.transform.parent = minigamePoint;
        Vector3 newPosition = new Vector3(minigamePoint.position.x, minigamePoint.position.y + .25f, minigamePoint.position.z);
        dangerLight.transform.position = newPosition;
    }

    void CompleteLight(Meter meter) {
        if (meter.Name == meterName) {
            lightNeeded = false;
            meterCompleted = true;
            TurnOffLight();
        }   
    }
}
