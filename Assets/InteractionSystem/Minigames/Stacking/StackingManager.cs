using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingManager : MonoBehaviour
{
    [SerializeField] float maxPower;
    [SerializeField] float powerRate;
    [SerializeField] Vector3 chargeDirection;

    int poopNeeded = 5;
    int poopMade = 0;
    bool charging = false;
    float counter = 0f;
    float launchPower = 0f;
    GameObject poopInstance;
    
    [SerializeField] GameObject poop;
    [SerializeField] GameObject scoopObj;
    [SerializeField] Transform spawnPoint;
    [SerializeField] MinigameManager minigameManager;

    void OnEnable() {
        
    }

    void OnDisable() {
        
    }

    void FixedUpdate() {
        if (charging) {
            launchPower += powerRate;
            Debug.Log("charging = " + launchPower);

            if (launchPower > maxPower) {
                launchPower = maxPower;
            }
        }
    }

    void Update() {
        // Scored();

        if (Input.GetMouseButtonDown(0) && !charging) {
            charging = true;
            poopInstance = Instantiate(poop, spawnPoint.TransformPoint(Vector3.zero), Quaternion.identity);
            poopInstance.GetComponent<Rigidbody>().isKinematic = true;
        } else if (Input.GetMouseButtonUp(0) && charging) {
            LaunchPoop();
        }
    }

    void LaunchPoop() {
        counter = 0f;
        charging = false;

        poopInstance.GetComponent<Rigidbody>().isKinematic = false;
        poopInstance.GetComponent<Rigidbody>().AddForce(spawnPoint.TransformDirection(chargeDirection) * launchPower);

        launchPower = 0f;
    }

    public void Missed() {
        AudioManager.instance.MinigameLost();
    }

    public void Scored() {
        poopMade++;
        Debug.Log(poopNeeded - poopMade + " to go");

        if (poopMade >= poopNeeded) {
            AudioManager.instance.MinigameWon();
            minigameManager.CompleteMinigame();
        }
    }
}
