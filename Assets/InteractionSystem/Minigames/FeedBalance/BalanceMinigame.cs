using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceMinigame : MonoBehaviour
{
    [SerializeField] int foodRequired;
    [SerializeField] int foodError;
    [SerializeField] float gravityForce;
    [SerializeField] float playerForce;
    [SerializeField] float topFoodRotation;
    [SerializeField] float bottomFoodRotation;

    float topRotation = 359f;
    float bottomRotation = 270f;
    float currentRotation;
    int currentFood = 0;
    int missedFood = 0;

    bool rotating = false;
    bool inSweetSpot = false;

    [SerializeField] GameObject sack;
    [SerializeField] ParticleSystem foodParticles;
    [SerializeField] MinigameManager minigameManager;
    [SerializeField] GameObject tutorialText;

    void OnEnable() {
        sack.transform.localEulerAngles = new Vector3(0f, 0f, topRotation);
        currentFood = 0;
        missedFood = 0;
        tutorialText.SetActive(true);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            rotating = true;
            tutorialText.SetActive(false);
        } if (Input.GetMouseButtonUp(0)) {
            rotating = false;
        }

        if (sack.transform.localEulerAngles.z < topFoodRotation && sack.transform.localEulerAngles.z > bottomFoodRotation) {
            if (!inSweetSpot) {
                inSweetSpot = true;
                foodParticles.Play();
            }
        } else if (inSweetSpot) {
            inSweetSpot = false;
            foodParticles.Stop();
        }

        // if (sack.transform.localEulerAngles.z == topRotation && !tutorialText.activeSelf) {
        //     tutorialText.SetActive(true);
        // }

        RotateSack();
    }

    void RotateSack() {
        currentRotation = sack.transform.localEulerAngles.z;

        if (rotating) {
            // Debug.Log("Rotating sack");
            currentRotation -= playerForce * Time.deltaTime;
        } else {
            currentRotation += gravityForce * Time.deltaTime;
        }

        if (currentRotation > topRotation) {
            currentRotation = topRotation;
            tutorialText.SetActive(true);
        } else if (currentRotation < bottomRotation) {
            currentRotation = bottomRotation;
        }

        sack.transform.localEulerAngles = new Vector3(0f, 0f, currentRotation);
        // Debug.Log(sack.transform.localEulerAngles);
    }

    public void FoodLanded() {
        currentFood++;
        Debug.Log(currentFood + " food hit");

        if (currentFood >= foodRequired) {
            currentFood = 0;
            missedFood = 0;
            AudioManager.instance.MinigameWon();
            minigameManager.CompleteMinigame();
        }
    }

    public void FoodMissed() {
        missedFood++;
        Debug.Log(missedFood + " food missed");

        if (missedFood >= foodError) {
            currentFood = 0;
            missedFood = 0;
            AudioManager.instance.MinigameLost();

            currentRotation = 359f;
            sack.transform.localEulerAngles = new Vector3(0f, 0f, currentRotation);
            rotating = false;
            inSweetSpot = false;
            foodParticles.Stop();
        }
    }
}
