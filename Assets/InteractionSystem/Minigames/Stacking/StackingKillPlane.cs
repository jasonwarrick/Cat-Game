using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingKillPlane : MonoBehaviour
{
    [SerializeField] bool scoring;

    [SerializeField] StackingManager stackingManager;

    void OnCollisionEnter(Collision other) {
        Destroy(other.gameObject);
        
        if (scoring) {
            stackingManager.Scored();
        } else {
            stackingManager.Missed();
        }

        Debug.Log("Poop");
    }
}
