using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timer;

    float counter = 0f;

    void Update() {
        if (counter < timer) {
            counter += Time.deltaTime;
        } else {
            Destroy(gameObject);
        }
    }
}
