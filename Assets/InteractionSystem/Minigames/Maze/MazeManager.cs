using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    Vector3 screenPos;
    Vector3 worldPos;
    bool runStarted = false;
    bool minigameFinished = false;

    [SerializeField] List<GameObject> mazes = new List<GameObject>();
    GameObject maze;

    [SerializeField] GameObject lightObj;
    [SerializeField] GameObject start;
    [SerializeField] GameObject goal;

    [SerializeField] Camera cam;
    [SerializeField] MinigameManager minigameManager;
    [SerializeField] GameObject tipCanvas;

    void OnEnable() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        start.SetActive(true);

        SetUpMaze();
    }

    void SetUpMaze() {
        maze = mazes[Random.Range(0, mazes.Count)];
        start.transform.position = maze.GetComponent<MazeScript>().startPoint.position;
        goal.transform.position = maze.GetComponent<MazeScript>().goalPoint.position;

        ToggleBorders(false);
    }

    void ToggleBorders(bool toggle) {
        maze.SetActive(toggle);

        if (!minigameFinished) {
            tipCanvas.SetActive(!toggle);
        }
    }

    void OnDisable() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        minigameFinished = false;
    }

    void Update() {
        MoveLight();
    }

    void MoveLight() {
        screenPos = Input.mousePosition;
        screenPos.z = 1f;
        worldPos = cam.ScreenToWorldPoint(screenPos);
        lightObj.transform.position = worldPos;

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;
            lightObj.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopMaze();
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

        // if (!minigameFinished) {
            
        // }
    }

    public void WallHit() {
        Debug.Log("Wall hit");
        AudioManager.instance.MinigameLost();
        StopMaze();
    }

    public void GoalHit() {
        Debug.Log("Goal hit");

        if (runStarted) {
            minigameFinished = true;
            AudioManager.instance.MinigameWon();
            StopMaze();
            minigameManager.CompleteMinigame();
        }
    }
}
