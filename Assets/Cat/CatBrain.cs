using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CatBrain : MonoBehaviour
{
    [SerializeField] Transform feedPoint;
    [SerializeField] Transform drinkPoint;
    [SerializeField] Transform litterPoint;
    [SerializeField] Transform playPoint;
    [SerializeField] Transform nailpoint;
    [SerializeField] Transform medicinePoint;

    [SerializeField] float meterAmt;

    Dictionary<string, float> meters = new Dictionary<string, float>();

    CatNavigation catNavigation;

    void Start() {
        catNavigation = GetComponent<CatNavigation>();

        meters.Add("feed", 0f);
        meters.Add("drink", 0f);
        meters.Add("litter", 0f);
        meters.Add("play", 0f);
        meters.Add("nail", 0f);
        meters.Add("medicine", 0f);
    }

    void FixedUpdate() {
        UpdateMeters();
    }

    void UpdateMeters() {
        meters["feed"] += meterAmt;
        meters["drink"] += meterAmt;
        meters["litter"] += meterAmt;
        meters["play"] += meterAmt;
        meters["nail"] += meterAmt;
        meters["medicine"] += meterAmt;
    }

    public void UpdateSpecMeter(string meter, float newMeterAmt) {
        meters[meter] += newMeterAmt;
    }

    public float GetMeter(string meter) { 
        if (meters.ContainsKey(meter)) {
            return meters[meter]; 
        } else { return -1; }
    }
}
