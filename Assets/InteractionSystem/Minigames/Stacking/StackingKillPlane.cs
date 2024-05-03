using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingKillPlane : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        Destroy(other.gameObject);
        Debug.Log("Poop");
    }
}
