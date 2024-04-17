using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    void Awake() => instance = this;

    [SerializeField] float dangerTimer;

    bool dangerFlashing = false;
    float dangerCounter = 0f;

    Canvas canvas;
    [SerializeField] GameObject crosshairs;
    [SerializeField] Image crosshair;
    [SerializeField] Image grabIcon;
    [SerializeField] Image cantGrabIcon;
    [SerializeField] TextMeshProUGUI dangerText;

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

    void Start() {
        canvas = GetComponent<Canvas>();
        
        UpdateCrosshair(false, false);

        PlayerInteraction.interactInRange += UpdateCrosshair;
        MinigameManager.minigameStarted += ToggleHUD;
        Meter.meterDanger += StartDangerText;
    }

    void Update() {
        if (dangerFlashing) {
            dangerCounter += Time.deltaTime;

            if (dangerCounter >= dangerTimer) {
                StopDangerText();
            }
        }
    }

    void UpdateCrosshair(bool isInRange, bool isAvailable) {
        crosshair.enabled = !isInRange;
        grabIcon.enabled = isInRange && isAvailable;
        cantGrabIcon.enabled = isInRange && !isAvailable;
    }

    void ToggleHUD(bool inMinigame) {
        // canvas.enabled = !inMinigame;
        crosshairs.SetActive(!inMinigame);
    }

    void StartDangerText(Meter meter) {
        dangerText.gameObject.SetActive(true);
        dangerText.text = dangerKey[meter.Name];

        dangerFlashing = true;
    }

    void StopDangerText() {
        dangerFlashing = false;
        dangerCounter = 0f;

        dangerText.gameObject.SetActive(false);
    }

    public void SwitchCamera(Camera cam) {
        if (cam == null) {
            canvas.worldCamera = playerCamera;
        } else {
            canvas.worldCamera = cam;
        }   
    }
}
