using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [SerializeField] float dangerTimer;

    bool dangerFlashing = false;
    [SerializeField] float filterAmt;
    float dangerAlpha = 0f;
    float dangerCounter = 0f;

    Canvas canvas;
    [SerializeField] GameObject crosshairs;
    [SerializeField] Image crosshair;
    [SerializeField] Image grabIcon;
    [SerializeField] Image cantGrabIcon;
    [SerializeField] TextMeshProUGUI clockText;
    [SerializeField] Image dangerFilter;

    [SerializeField] Camera playerCamera;

    Dictionary<string, string> dangerKey = new Dictionary<string, string>()
    {
    {"feed", "FAUGN K'YARNAK"},
    {"drink", "DAGON NW KN'A"},
    {"litter", "LI'HEE NNNULN"},
    {"play", "PHLEGETH H'SHUGG"},
    {"nail", "I have to clip its nails"},
    {"medicine", "I have to give it medicine"},
    };

    void OnEnable() {
        instance = this;
        canvas = GetComponent<Canvas>();
        
        UpdateCrosshair(false, false);

        PlayerInteraction.interactInRange += UpdateCrosshair;
        MinigameManager.minigameStarted += ToggleHUD;
        Meter.meterDanger += StartDangerFilter;
        GameStateManager.timeChanged += UpdateClock;
    }

    void OnDestroy() {
        PlayerInteraction.interactInRange -= UpdateCrosshair;
        MinigameManager.minigameStarted -= ToggleHUD;
        Meter.meterDanger -= StartDangerFilter;
        GameStateManager.timeChanged -= UpdateClock;
    }

    void UpdateClock(int hours, int minutes) {
        if (minutes < 10) {
            clockText.text = hours + ":" + "0" + minutes;
        } else {
            clockText.text = hours + ":" + minutes;
        }
    }

    void Update() {
        if (dangerFlashing) {
            dangerCounter += Time.deltaTime;

            dangerAlpha = (dangerCounter / dangerTimer) / filterAmt;
            dangerFilter.color = new Color(255f, 0f, 0f, dangerAlpha);

            if (dangerCounter >= dangerTimer) {
                StopDangerText();
            }
        }
    }

    void UpdateCrosshair(bool isInRange, bool isAvailable) {
        if (crosshair != null) {
            crosshair.enabled = !isInRange;
            grabIcon.enabled = isInRange && isAvailable;
            cantGrabIcon.enabled = isInRange && !isAvailable;
        }
    }

    void ToggleHUD(bool inMinigame) {
        // canvas.enabled = !inMinigame;
        crosshairs.SetActive(!inMinigame);
    }

    void StartDangerFilter(Meter meter) {
        dangerFilter.enabled = true;
        dangerCounter = 0f;
        dangerAlpha = 0f;

        dangerFlashing = true;
    }

    void StopDangerText() {
        dangerFilter.enabled = false;
        dangerCounter = 0f;
        dangerAlpha = 0f;
        dangerFilter.color = new Color(255f, 0f, 0f, dangerAlpha);
    }

    public void SwitchCamera(Camera cam) {
        if (cam == null) {
            canvas.worldCamera = playerCamera;
        } else {
            Debug.Log(cam.gameObject.name + " is the active camera");
            canvas.worldCamera = cam;
        }   
    }
}
