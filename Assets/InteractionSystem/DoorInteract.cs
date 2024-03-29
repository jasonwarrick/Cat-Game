using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] float openDirection;
    bool open = false;
    
    public void Interact() {
        if (!open) {
            transform.Rotate(new Vector3(0f, openDirection * 90f, 0f));
        } else {
            transform.Rotate(new Vector3(0f, openDirection * -1f * 90f, 0f));
        }

        open = !open;

        Debug.Log("use door");
    }
}
