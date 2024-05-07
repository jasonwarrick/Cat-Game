using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingManager : MonoBehaviour
{
    [SerializeField] float powerFactor;
    [SerializeField] float chargeTime;
    [SerializeField] Vector3 chargeDirection;

    int poopNeeded = 5;
    int poopMade = 0;
    bool charging = false;
    float counter = 0f;
    
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
            counter += Time.deltaTime;
            Debug.Log("charging = " + counter);

            if (counter >= chargeTime) {
                LaunchPoop();
            }
        }
    }

    void Update() {
        // Scored();

        if (Input.GetMouseButtonDown(0) && !charging) {
            charging = true;
        } else if (Input.GetMouseButtonUp(0)) {
            counter = 0f;
            charging = false;
        }
    }

    void LaunchPoop() {
        Debug.Log("launch");
        counter = 0f;
        charging = false;
        GameObject poopInstance = Instantiate(poop, spawnPoint.TransformPoint(Vector3.zero), Quaternion.identity);
        // poopInstance.transform.position = spawnPoint.TransformPoint(Vector3.zero);
        Debug.DrawRay(spawnPoint.position, spawnPoint.TransformDirection(chargeDirection), Color.green, 5f);
        poopInstance.GetComponent<Rigidbody>().AddForce(spawnPoint.TransformDirection(chargeDirection) * powerFactor);
    }

    public void Missed() {
        AudioManager.instance.MinigameLost();
    }

    public void Scored() {
        poopMade++;

        if (poopMade >= poopNeeded) {
            AudioManager.instance.MinigameWon();
            minigameManager.CompleteMinigame();
        }
    }
}
