using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    [SerializeField] MazeManager mazeManager;

    void OnCollisionEnter(Collision other) {
        Debug.Log("hit");
        switch (other.gameObject.tag) {
            case "MazeWall":
                mazeManager.WallHit();
                break;

            case "MazeGoal":
                mazeManager.GoalHit();
                break;
        }
    }
}
