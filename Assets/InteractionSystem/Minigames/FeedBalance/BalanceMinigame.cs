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
    bool gameDone = false;

    [SerializeField] GameObject sack;
    [SerializeField] ParticleSystem foodParticles;
    [SerializeField] MinigameManager minigameManager;
    [SerializeField] GameObject tutorialText;

    [SerializeField] GameObject clinkSFX;
    [SerializeField] GameObject thumpSFX;

    void OnEnable() {
        sack.transform.localEulerAngles = new Vector3(0f, 0f, topRotation);
        currentFood = 0;
        missedFood = 0;
        tutorialText.SetActive(true);
        gameDone = false;
    }

    void Update() {
        if (!gameDone) {
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

            RotateSack();
        }
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
        Instantiate(clinkSFX, transform);

        if (currentFood >= foodRequired) {
            currentFood = 0;
            missedFood = 0;
            AudioManager.instance.MinigameWon();
            minigameManager.CompleteMinigame();
            gameDone = true;
            foodParticles.Stop();
        }
    }

    public void FoodMissed() {
        missedFood++;
        Debug.Log(missedFood + " food missed");
        Instantiate(thumpSFX, transform);

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
