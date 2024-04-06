using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class DoorInteract : Interactable
{
    [SerializeField] float openDirection;
    [SerializeField] bool ignoreNav;
    
    bool open = false;

    NavMeshObstacle obstacle;

    void Awake() {
        if (!ignoreNav) {
            obstacle = GetComponent<NavMeshObstacle>();
        }
    }
    
    override public void Interact() {
        if (!open) {
            transform.Rotate(new Vector3(0f, openDirection * 90f, 0f));

            if (!ignoreNav) {
                obstacle.carving = false;
            }
            
        } else {
            transform.Rotate(new Vector3(0f, openDirection * -1f * 90f, 0f));

            if (!ignoreNav) {
                obstacle.carving = false;
            }
        }

        open = !open;

        Debug.Log("use door");
    }
}
