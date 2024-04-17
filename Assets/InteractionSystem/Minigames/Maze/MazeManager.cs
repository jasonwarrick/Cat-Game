using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    Vector3 screenPos;
    Vector3 worldPos;
    bool mouseHeld = false;
    bool runStarted = false;

    [SerializeField] GameObject lightObj;
    [SerializeField] Camera cam;
    [SerializeField] MinigameManager minigameManager;
    [SerializeField] GameObject start;

    void OnEnable() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        start.SetActive(true);
    }

    void OnDisable() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        screenPos = Input.mousePosition;
        screenPos.z = 1f;
        worldPos = cam.ScreenToWorldPoint(screenPos);
        lightObj.transform.position = worldPos;

        if (Input.GetMouseButtonDown(0)) {
            mouseHeld = true;
            Cursor.visible = false;
            lightObj.SetActive(true);
        } else if (Input.GetMouseButtonUp(0)) {
            mouseHeld = false;
            runStarted = false;
            Cursor.visible = true;
            lightObj.SetActive(false);
        }
    }

    public void StartMaze() {
        runStarted = true;
        start.SetActive(false);
    }

    public void StopMaze() {
        runStarted = false;
        start.SetActive(true);
    }

    public void WallHit() {
        Debug.Log("Wall hit");
        StopMaze();
    }

    public void GoalHit() {
        Debug.Log("Goal hit");

        if (runStarted) {
            StopMaze();
            minigameManager.CompleteMinigame();
        }
    }
}
