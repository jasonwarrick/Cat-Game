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
            Debug.Log(hit.transform.name);
            
            if (hit.transform.gameObject.tag == "Interactable") {
                if (!inRange || inRange && objectInRange != hit.transform.gameObject) {
                    inRange = true;
                    interactInRange.Invoke(inRange);
                    objectInRange = hit.transform.gameObject;
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
            if (inRange) {
                objectInRange.GetComponent<Interactable>().Interact();
            }
        }
    }

    public void HoldItem(GameObject item) {
        if (heldObject == null) {
            Debug.Log("Picked up");
            item.transform.parent = holdPoint;
            item.transform.position = holdPoint.position;
            heldObject = item;
        }   
    }

    public void DropItem(GameObject itemParent) {
        Debug.Log("Dropped");
        heldObject.transform.parent = itemParent.transform;
        heldObject.transform.position = itemParent.transform.position;
        heldObject = null;
    }
}
