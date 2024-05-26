using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject tutorial1;
    [SerializeField] GameObject tutorial2;

    void Start () {
        ShowMainMenu();
    }

    public void ShowMainMenu() {
        mainMenuCanvas.SetActive(true);
        tutorial1.SetActive(false);
        tutorial2.SetActive(false);
    }

    public void ShowTut1() {
        mainMenuCanvas.SetActive(false);
        tutorial1.SetActive(true);
        tutorial2.SetActive(false);
    }

    public void ShowTut2() {
        mainMenuCanvas.SetActive(false);
        tutorial1.SetActive(false);
        tutorial2.SetActive(true);
    }
}