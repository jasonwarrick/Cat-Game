using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    [SerializeField] MazeManager mazeManager;

    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case "MazeWall":
                mazeManager.WallHit();
                break;

            case "MazeGoal":
                mazeManager.GoalHit();
                break;
            
            case "MazeStart":
                Debug.Log("Start hit");
                mazeManager.StartMaze();
                break;
        }
    }
}
