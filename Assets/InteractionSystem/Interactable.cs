using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Interactable : MonoBehaviour
{
    private bool available;
    
    abstract public void Interact();

    abstract public bool CheckAvailable();
}
