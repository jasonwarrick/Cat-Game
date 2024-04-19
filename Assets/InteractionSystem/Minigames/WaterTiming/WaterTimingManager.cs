using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterTimingManager : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float sweetSpotRange;

    bool waterIsRunning = false;
    bool inSweetSpot = false;

    float timer = 0f;
    float timerRatio = 0f;
    float sweetSpotStart = 0f;

    [SerializeField] MinigameManager minigameManager;
    [SerializeField] ParticleSystem waterParticles;
    [SerializeField] Image indicator;
    [SerializeField] GameObject tutorialText;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            SetSweetSpot();
            indicator.enabled = true;
            tutorialText.SetActive(false);
            waterIsRunning = true;

            waterParticles.Play();
        } if (Input.GetMouseButtonUp(0)) {
            tutorialText.SetActive(true);

            if (inSweetSpot) {
                MinigameWon();
            } else if (waterIsRunning) { 
                MinigameLost();
            }
        }

        if (waterIsRunning) {
            UpdateIndicator();
        }
    }

    void OnEnable() {
        ResetMinigame();
    }

    void SetSweetSpot() {
        sweetSpotStart = (duration / 2) + Random.Range(0f, duration / 2);

        if (sweetSpotStart + sweetSpotRange > duration) {
            sweetSpotStart -= sweetSpotStart + sweetSpotRange - duration;
        }

        Debug.Log("minimum is " + sweetSpotStart + "; maximum is " + (sweetSpotStart + sweetSpotRange));
    }

    void UpdateIndicator() {
        timer += Time.deltaTime;

        if (timer >= sweetSpotStart && timer <= sweetSpotStart + sweetSpotRange) {
            indicator.color = Color.green;
            inSweetSpot = true;
        } else {
            inSweetSpot = false;
            indicator.color = Color.red;
        }

        if (timerRatio >= 1f) {
            MinigameLost();
        }
    }

    void MinigameLost() {
        ResetMinigame();
        waterIsRunning = false;
        waterParticles.Stop();
        AudioManager.instance.MinigameLost();
    }

    void MinigameWon() {
        ResetMinigame();
        inSweetSpot = false;
        waterIsRunning = false;
        waterParticles.Stop();
        AudioManager.instance.MinigameWon();
        minigameManager.CompleteMinigame();
    }

    void ResetMinigame() {
        timer = 0f;
        timerRatio = 0f;
        indicator.color = Color.red;
        indicator.enabled = false;
    }
}
