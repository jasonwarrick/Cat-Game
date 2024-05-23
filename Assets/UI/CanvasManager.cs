using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    [SerializeField] GameObject hud;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject gameEnd;

    void Start() {
        instance = this;
    }

    public void ShowHUD() {
        hud.SetActive(true);
        pause.SetActive(false);
        settings.SetActive(false);
        gameEnd.SetActive(false);
    }

    public void ShowPause() {
        hud.SetActive(false);
        pause.SetActive(true);
        settings.SetActive(false);
        gameEnd.SetActive(false);
    }

    public void ShowSettings() {
        hud.SetActive(false);
        pause.SetActive(false);
        settings.SetActive(true);
        gameEnd.SetActive(false);
    }

    public void ShowGameEnd() {
        hud.SetActive(false);
        pause.SetActive(false);
        settings.SetActive(false);
        gameEnd.SetActive(true);
    }
}
