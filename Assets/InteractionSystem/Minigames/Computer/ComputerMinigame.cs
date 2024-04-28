using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerMinigame : MonoBehaviour
{
    [SerializeField] float workToComplete;
    [SerializeField] float workFactor;

    float currentWork = 0;
    int workDone = 0;

    [SerializeField] Slider progressBar;
    [SerializeField] TextMeshProUGUI workTracker;
    [SerializeField] ComputerAudioManager cam;

    void Update() {
        if (Input.anyKeyDown) {
            DoWork();
        }
    }

    void DoWork() {
        currentWork++;
        cam.PlayKeyAudio();
        // Debug.Log (currentWork + " work done out of " + workToComplete + " total work");

        if (currentWork >= workToComplete) {
            FinishWork();
        }

        UpdateProgress();
    }

    void FinishWork() {
        workDone++;
        currentWork = 0;
        workToComplete *= workFactor;

        workTracker.text = workDone + " work done";
    }

    void UpdateProgress() {
        progressBar.value = currentWork / workToComplete;
    }
}
