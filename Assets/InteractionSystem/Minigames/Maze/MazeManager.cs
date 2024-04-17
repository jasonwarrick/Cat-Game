using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    Vector3 screenPos;
    Vector3 worldPos;
    bool mouseHeld = false;
    bool runStarted = false;

    [SerializeField] List<GameObject> mazes = new List<GameObject>();
    GameObject maze;
    [SerializeField] GameObject borders;

    [SerializeField] GameObject lightObj;
    [SerializeField] Camera cam;
    [SerializeField] MinigameManager minigameManager;
    [SerializeField] GameObject start;
    [SerializeField] GameObject tipCanvas;

    void OnEnable() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        start.SetActive(true);

        SetUpMaze();
    }

    void SetUpMaze() {
        maze = mazes[Random.Range(0, mazes.Count)];

        ToggleBorders(false);
    }

    void ToggleBorders(bool toggle) {
        maze.SetActive(toggle);
        borders.SetActive(toggle);
        tipCanvas.SetActive(!toggle);
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

        ToggleBorders(true);
    }

    public void StopMaze() {
        runStarted = false;
        start.SetActive(true);

        ToggleBorders(false);
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
