using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTimingManager : MonoBehaviour
{
    bool waterIsRunning = false;

    [SerializeField] ParticleSystem waterParticles;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            waterIsRunning = true;
            waterParticles.Play();
        } if (Input.GetMouseButtonUp(0)) {
            waterIsRunning = false;
            waterParticles.Stop();
        }
    }
}
