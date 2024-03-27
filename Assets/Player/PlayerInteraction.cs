using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public delegate void InteractInRange(bool isInRange);
    public static InteractInRange interactInRange;

    [SerializeField] float interactDistance;
    
    bool inRange = false;

    InputActionsManager inputActionsManager;

    InputAction interactAction;

    Camera cam;
    
    void Start() {
        cam = Camera.main;

        inputActionsManager = new InputActionsManager();
        
        interactAction = inputActionsManager.Player.Interact;
        interactAction.Enable();
    }

    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactDistance)) {
            
            if (hit.transform.gameObject.tag == "Interactable") {
                if (!inRange) {
                    inRange = true;
                    interactInRange.Invoke(inRange);
                }
            } else {
                if (inRange) {
                    inRange = false;
                    interactInRange.Invoke(inRange);
                }
            }
        } else {
            if (inRange) {
                inRange = false;
                interactInRange.Invoke(inRange);
            }
        }

        if (interactAction.triggered) {
            Debug.Log("Click");
        }
    }
}
