using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceMinigame : MonoBehaviour
{
    [SerializeField] float gravityForce;
    [SerializeField] float playerForce;
    [SerializeField] float topFoodRotation;
    [SerializeField] float bottomFoodRotation;

    float topRotation = 359f;
    float bottomRotation = 270f;
    
    float currentRotation;

    bool rotating = false;
    bool inSweetSpot = false;

    [SerializeField] GameObject sack;
    [SerializeField] ParticleSystem foodParticles;

    void OnEnable() {
        sack.transform.localEulerAngles = new Vector3(0f, 0f, topRotation);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            rotating = true;
        } if (Input.GetMouseButtonUp(0)) {
            rotating = false;
        }

        if (sack.transform.localEulerAngles.z < topFoodRotation && sack.transform.localEulerAngles.z > bottomFoodRotation) {
            if (!inSweetSpot) {
                Debug.Log("start pouring");
                inSweetSpot = true;
                foodParticles.Play();
            }
        } else if (inSweetSpot) {
            Debug.Log("stop pouring");
            inSweetSpot = false;
            foodParticles.Stop();
        }

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
        } else if (currentRotation < bottomRotation) {
            currentRotation = bottomRotation;
        }

        sack.transform.localEulerAngles = new Vector3(0f, 0f, currentRotation);
        // Debug.Log(sack.transform.localEulerAngles);
    }
}
