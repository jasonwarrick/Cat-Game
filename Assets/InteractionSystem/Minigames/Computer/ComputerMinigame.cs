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
    bool lockedOut = false;
    public bool atComputer = false;

    [SerializeField] Slider progressBar;
    [SerializeField] TextMeshProUGUI workTracker;
    [SerializeField] ComputerAudioManager compAudioManager;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject errorCanvas;

    void Start() {
        Meter.meterDanger += LockOut;
        Meter.meterReset += Unlock;
    }

    void OnDestroy() {
        Meter.meterDanger -= LockOut;
        Meter.meterReset -= Unlock;
    }

    void Update() {
        if (cam.activeInHierarchy) {
            atComputer = true;
        } else {
            atComputer = false;
        }
        
        if (Input.anyKeyDown && !lockedOut && atComputer) {
            DoWork();
        }
    }

    void OnEnable() {
        if (lockedOut) {
            gameCanvas.SetActive(false);
            errorCanvas.SetActive(true);
        } else {
            gameCanvas.SetActive(true);
            errorCanvas.SetActive(false);
        }
    }

    void LockOut(Meter meter) {
        gameCanvas.SetActive(false);
        errorCanvas.SetActive(true);
        lockedOut = true;
    }

    void Unlock(Meter meter) {
        lockedOut = false;
        gameCanvas.SetActive(true);
        errorCanvas.SetActive(false);
    }

    void DoWork() {
        currentWork++;
        compAudioManager.PlayKeyAudio();
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
