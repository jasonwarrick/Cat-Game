using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class DoorInteract : Interactable
{
    [SerializeField] float openDirection;
    
    bool open = false;

    NavMeshObstacle obstacle;

    void Awake() {
        obstacle = GetComponent<NavMeshObstacle>();
    }
    
    override public void Interact() {
        if (!open) {
            transform.Rotate(new Vector3(0f, openDirection * 90f, 0f));
            obstacle.carving = false;
        } else {
            transform.Rotate(new Vector3(0f, openDirection * -1f * 90f, 0f));
            obstacle.carving = true;
        }

        open = !open;

        Debug.Log("use door");
    }
}
