using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingManager : MonoBehaviour
{
    [SerializeField] float spawnRange;
    [SerializeField] float spawnOffset;

    Vector3 screenPos;
    Vector3 worldPos;

    [SerializeField] GameObject poop;
    [SerializeField] GameObject scoopObj;
    [SerializeField] Camera cam;

    void OnEnable() {
        // Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        Vector3 poopPos = new Vector3(-spawnRange, 0.75f, 1f);
        GameObject poopInstance = Instantiate(poop, transform, false);
        poopInstance.transform.position = transform.TransformPoint(poopPos);
    }

    void OnDisable() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        MoveScoop();
    }

    void MoveScoop() {
        screenPos = Input.mousePosition;
        screenPos.z = 1f;
        worldPos = cam.GetComponent<Camera>().ScreenToWorldPoint(screenPos);
        scoopObj.transform.position = worldPos;
    }
}
