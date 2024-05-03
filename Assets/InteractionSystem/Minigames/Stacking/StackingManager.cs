using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingManager : MonoBehaviour
{
    [SerializeField] float spawnRange;
    [SerializeField] float spawnOffset;

    [SerializeField] GameObject poop;

    void OnEnable() {
        Vector3 poopPos = new Vector3(-spawnRange, 0.75f, 1f);
        GameObject poopInstance = Instantiate(poop, transform, false);
        poopInstance.transform.position = transform.TransformPoint(poopPos);
    }
}
