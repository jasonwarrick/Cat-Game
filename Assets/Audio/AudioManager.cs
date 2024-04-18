using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    void Awake() => instance = this;

    [SerializeField] AudioSource successSFX;
    [SerializeField] AudioSource failSFX;

    public void MinigameWon() {
        successSFX.Play();
    }

    public void MinigameLost() {
        failSFX.Play();
    }
}
