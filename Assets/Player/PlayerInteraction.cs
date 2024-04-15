using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public delegate void InteractInRange(bool isInRange);
    public static InteractInRange interactInRange;

    [SerializeField] float interactDistance;
    [SerializeField] Transform holdPoint;
    
    bool inRange = false;
    bool isAvailable = false;
    GameObject objectInRange = null;

    GameObject heldObject;
    Camera cam;
    
    void Start() {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactDistance)) {
            if (hit.transform.gameObject.tag == "Interactable") {
                if (!inRange || inRange && objectInRange != hit.transform.gameObject) {
                    inRange = true;
                    interactInRange.Invoke(inRange);
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
                    inRange = false;
                    interactInRange.Invoke(inRange);
                    objectInRange = null;
                }
            }
        } else {
            if (inRange) {
                inRange = false;
                interactInRange.Invoke(inRange);
                objectInRange = null;
            }
        }

        if (InputReader.instance.interact) {
            if (inRange && isAvailable && !GameStateManager.instance.GetInMinigame()) {
                
                foreach (Interactable interactable in objectInRange.GetComponents<Interactable>()) {
                    if (GameStateManager.instance.GetInMinigame()) { break; }
                    
                    interactable.Interact();
                }
            }
        }
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
