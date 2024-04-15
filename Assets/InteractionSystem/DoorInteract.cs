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
    public bool available = true;

    NavMeshObstacle obstacle;
    [SerializeField] AudioSource openFX;
    [SerializeField] AudioSource closeFX;

    void Awake() {
        if (!ignoreNav) {
            obstacle = GetComponent<NavMeshObstacle>();
        }
    }

    public override bool CheckAvailable() {
        return available;
    }

    override public void Interact() {
        if (!open) {
            transform.Rotate(new Vector3(0f, openDirection * 90f, 0f));

            if (!ignoreNav) {
                obstacle.carving = false;
            }

            openFX.Play();
            
        } else {
            transform.Rotate(new Vector3(0f, openDirection * -1f * 90f, 0f));

            if (!ignoreNav) {
                obstacle.carving = true;
            }

            closeFX.Play();
        }

        open = !open;

        Debug.Log("use door");
    }
}
