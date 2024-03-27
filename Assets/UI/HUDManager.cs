using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Image crosshair;
    [SerializeField] Image grabIcon;

    void Start() {
        PlayerInteraction.interactInRange += UpdateCrosshair;
        UpdateCrosshair(false);
    }

    void UpdateCrosshair(bool isInRange) {
        crosshair.enabled = !isInRange;
        grabIcon.enabled = isInRange;
    }
}
