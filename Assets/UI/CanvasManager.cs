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
    [SerializeField] GameObject mainMenu;

    GameObject[] canvases;
    
    void Start() {
        instance = this;

        canvases = new GameObject[] {
            hud,
            pause,
            settings,
            gameEnd
        };
    }

    public void ChangeAllCameras(Camera cam) {
        foreach (GameObject canvas in canvases) {
            if (canvas.GetComponent<Canvas>().renderMode == RenderMode.ScreenSpaceCamera) {
                canvas.GetComponent<Canvas>().worldCamera = cam;
            }
        }
    }

    public void ShowHUD() {
        hud.SetActive(true);
        pause.SetActive(false);
        settings.SetActive(false);
        gameEnd.SetActive(false);
        
        if (mainMenu != null) {
            mainMenu.SetActive(false);
        }
    }

    public void ShowPause() {
        hud.SetActive(false);
        pause.SetActive(true);
        settings.SetActive(false);
        gameEnd.SetActive(false);
        
        if (mainMenu != null) {
            mainMenu.SetActive(false);
        }
    }

    public void ShowSettings() {
        hud.SetActive(false);
        pause.SetActive(false);
        settings.SetActive(true);
        gameEnd.SetActive(false);
        
        if (mainMenu != null) {
            mainMenu.SetActive(false);
        }
    }

    public void ShowGameEnd() {
        hud.SetActive(false);
        pause.SetActive(false);
        settings.SetActive(false);
        gameEnd.SetActive(true);
        
        if (mainMenu != null) {
            mainMenu.SetActive(false);
        }
    }

    public void ShowMainMenu() {
        hud.SetActive(false);
        pause.SetActive(false);
        settings.SetActive(false);
        gameEnd.SetActive(false);

        if (mainMenu != null) {
            mainMenu.SetActive(true);
        }
    }
}
