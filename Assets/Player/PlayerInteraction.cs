using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public delegate void InteractInRange(bool isInRange);
    public static InteractInRange interactInRange;

    [SerializeField] float interactDistance;
    
    bool inRange = false;
    GameObject objectInRange = null;

    Camera cam;
    
    void Start() {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactDistance)) {
            
            if (hit.transform.gameObject.tag == "Interactable") {
                if (!inRange) {
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
}
