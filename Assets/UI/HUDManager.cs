using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    Canvas canvas;
    [SerializeField] Image crosshair;
    [SerializeField] Image grabIcon;

    void Start() {
        PlayerInteraction.interactInRange += UpdateCrosshair;
        MinigameManager.minigameStarted += ToggleHUD;

        canvas = GetComponent<Canvas>();
        
        UpdateCrosshair(false);
    }

    void UpdateCrosshair(bool isInRange) {
        crosshair.enabled = !isInRange;
        grabIcon.enabled = isInRange;
    }

    void ToggleHUD(bool inMinigame) {
        canvas.enabled = !inMinigame;
    }
}
