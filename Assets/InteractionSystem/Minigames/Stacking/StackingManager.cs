using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingManager : MonoBehaviour
{
    [SerializeField] float maxPower;
    [SerializeField] float powerRate;
    [SerializeField] Vector3 chargeDirection;

    int poopNeeded = 3;
    int maxMissed = 5;
    int poopMade = 0;
    int poopMissed = 0;
    bool charging = false;
    bool gameStarted = false;
    float counter = 0f;
    float launchPower = 0f;
    GameObject poopInstance;
    
    [SerializeField] GameObject poop;
    [SerializeField] GameObject scoopObj;
    [SerializeField] Transform spawnPoint;
    [SerializeField] MinigameManager minigameManager;
    [SerializeField] LaunchAudioManager lam;
    [SerializeField] GameObject tutCanvas;

    void OnEnable() {
        gameStarted = false;
        tutCanvas.SetActive(true);
        SpawnPoop();
    }

    void OnDisable() {
        gameStarted = false;
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
            if (!gameStarted) {
                gameStarted = true;
                tutCanvas.SetActive(false);
            }
            
            charging = true;
            lam.StartCharge();
        } else if (Input.GetMouseButtonUp(0) && charging) {
            LaunchPoop();
            lam.StopCharge();
        }
    }

    void SpawnPoop() {
        Debug.Log("spawned");
        poopInstance = Instantiate(poop, spawnPoint.TransformPoint(Vector3.zero), Quaternion.identity);
        poopInstance.GetComponent<Rigidbody>().isKinematic = true;
    }

    void LaunchPoop() {
        counter = 0f;
        charging = false;

        poopInstance.GetComponent<Rigidbody>().isKinematic = false;
        poopInstance.GetComponent<Rigidbody>().AddForce(spawnPoint.TransformDirection(chargeDirection) * launchPower);

        launchPower = 0f;
    }

    public void Missed() {
        lam.PoopHit();
        poopMissed++;
        SpawnPoop();

        if (poopMissed > maxMissed) {
            GameOver();
        }
    }

    void GameOver() {
        AudioManager.instance.MinigameLost();
    }

    public void Scored() {
        poopMade++;
        lam.PoopMake();
        SpawnPoop();
        Debug.Log(poopNeeded - poopMade + " to go");

        if (poopMade >= poopNeeded) {
            poopMade = 0;
            poopMissed = 0;
            AudioManager.instance.MinigameWon();
            minigameManager.CompleteMinigame();
        }
    }
}
