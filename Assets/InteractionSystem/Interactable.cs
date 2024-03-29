using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public void Interact() {
        transform.Rotate(new Vector3(0f, 90f, 0f));
    }
}
