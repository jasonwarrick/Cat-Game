using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] DoorInteract itemScript;

    public void Interact() {
        itemScript.Interact();
    }
}
