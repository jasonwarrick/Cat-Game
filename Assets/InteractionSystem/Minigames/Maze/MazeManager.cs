using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    Vector3 screenPos;
    Vector3 worldPos;

    [SerializeField] GameObject lightObj;
    [SerializeField] Camera cam;

    void OnEnable() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update() {
        screenPos = Input.mousePosition;
        screenPos.z = 1f;
        worldPos = cam.ScreenToWorldPoint(screenPos);
        lightObj.transform.position = worldPos;
    }

    public void WallHit() {
        Debug.Log("Wall hit");
    }

    public void GoalHit() {
        Debug.Log("Goal hit");
    }
}
