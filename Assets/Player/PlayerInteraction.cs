using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactDistance;

    Camera cam;
    
    void Start() {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
