using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBrain : MonoBehaviour
{
    [SerializeField] Transform feedPoint;
    [SerializeField] Transform drinkPoint;
    [SerializeField] Transform litterPoint;
    [SerializeField] Transform playPoint;
    [SerializeField] Transform nailpoint;
    [SerializeField] Transform medicinePoint;

    float feedMeter = 0f;
    float drinkMeter = 0f;
    float litterMeter = 0f;
    float playMeter = 0f;
    float nailmeter = 0f;
    float medicineMeter = 0f;

    CatNavigation catNavigation;

    void Start() {
        catNavigation = GetComponent<CatNavigation>();

        
    }

    void Update() {
        
    }
}
