using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    void OnEnable() => instance = this;

    [SerializeField] AudioSource successSFX;
    [SerializeField] AudioSource failSFX;
    [SerializeField] AudioSource dangerSound;

    void Start() {
        Meter.meterDanger += Danger;
    }

    void OnDestroy() {
        Meter.meterDanger -= Danger;
    }

    public void MinigameWon() {
        successSFX.Play();
    }

    public void MinigameLost() {
        failSFX.Play();
    }

    public void Danger(Meter meter) {
        dangerSound.Play();
    }
}
