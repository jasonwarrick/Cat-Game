using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public delegate void InteractInRange(bool isInRange, bool isAvailable);
    public static InteractInRange interactInRange;

    [SerializeField] float interactDistance;
    [SerializeField] Transform holdPoint;
    
    bool inRange = false;
    bool isAvailable = false;
    GameObject objectInRange = null;

    GameObject heldObject;
    Camera cam;
    
    void Start() {
        cam = GetComponentInChildren<Camera>();
    }

    public void UpdateIsAvailable(GameObject givenObject, bool newAvailable) {
        if (givenObject == objectInRange) {
            isAvailable = newAvailable;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Time.timeScale != 0f) {
            RaycastHit hit;
            
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactDistance)) {
                if (hit.transform.gameObject.tag == "Interactable") {
                    if (!inRange || inRange && objectInRange != hit.transform.gameObject) {
                        isAvailable = false;
                        inRange = true;
                        objectInRange = hit.transform.gameObject; 
                        foreach (Interactable interactable in objectInRange.GetComponents<Interactable>()) {
                            if (GameStateManager.instance.GetInMinigame()) { break; }
                            
                            if (interactable.CheckAvailable()) {
                                isAvailable = true;
                            }
                        }
                    }
                } else {
                    if (inRange) {
                        NullifyRay();
                    }
                }
            } else {
                if (inRange) {
                    NullifyRay();
                }
            }

            interactInRange.Invoke(inRange, isAvailable);

            if (InputReader.instance.interact) {
                if (inRange && isAvailable && !GameStateManager.instance.GetInMinigame()) {
                    
                    foreach (Interactable interactable in objectInRange.GetComponents<Interactable>()) {
                        if (GameStateManager.instance.GetInMinigame()) { break; }
                        
                        interactable.Interact();
                    }
                }
            }
        }
        
    }

    void NullifyRay() {
        inRange = false;
        objectInRange = null;
    }

    public bool HoldItem(GameObject item) {
        if (heldObject == null) {
            Debug.Log("Picked up");
            item.transform.parent = holdPoint;
            item.transform.position = holdPoint.position;
            heldObject = item;
            GameStateManager.instance.heldObject = item;
            return true;
        }

        return false;
    }

    public void DropItem(GameObject itemParent) {
        Debug.Log("Dropped");
        heldObject.transform.parent = itemParent.transform;
        heldObject.transform.position = itemParent.transform.position;
        heldObject = null;
        GameStateManager.instance.heldObject = null;
    }

    public GameObject GetHeldItem() {
        return heldObject;
    }
}
