using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodParticlesScript : MonoBehaviour
{
    [SerializeField] BalanceMinigame minigame;

    void OnParticleCollision(GameObject other) {
        if (other.gameObject.CompareTag("BalanceBowl")) {
            minigame.FoodLanded();
        } else {
            minigame.FoodMissed();
        }
    }
}
