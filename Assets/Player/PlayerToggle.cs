using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToggle : MonoBehaviour
{
    [SerializeField] GameObject camObject;
    [SerializeField] GameObject meshObject;
    [SerializeField] FirstPersonMovement fpMove;
    [SerializeField] FirstPersonLook fpLook;
    [SerializeField] PlayerInteraction playerInteraction;

    public void SetPlayer(bool value) {
        camObject.SetActive(value);
        meshObject.SetActive(value);
        fpMove.enabled = value;
        fpLook.enabled = value;
        playerInteraction.canInteract = value;
        
        if (playerInteraction.GetHeldItem() != null) {
            playerInteraction.GetHeldItem().SetActive(value);
        }
    }
}
