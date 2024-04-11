using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    List<float> meters = new List<float>(); // The value of the meters
    Dictionary<string, int> metersKey = new Dictionary<string, int> { // Holds the location of each meter in the "meters" list with its name and index
        {"feed", 0},
        {"drink", 1},
        {"litter", 2},
        {"play", 3},
        {"nail", 4},
        {"medicine", 5},
    };

    CatNavigation catNavigation;

    void Start() {
        catNavigation = GetComponent<CatNavigation>();

        for (int i = 0; i < 6; i++) {
            meters.Add(0f);
        }
    }

    void FixedUpdate() {
        UpdateMeters();
    }

    void UpdateMeters() {
        for (int i = 0; i < meters.Count; i++) {
            meters[i] += meterAmt;
            Debug.Log("index " + i + " is now " + meters[i]);
        }
    }

    public void UpdateSpecMeter(string meter, float newMeterAmt) {
        meters[metersKey[meter]] += newMeterAmt;
    }

    public float GetMeter(string meter) { 
        if (metersKey.ContainsKey(meter)) {
            return meters[metersKey[meter]]; 
        } else { return -1; }
    }
}
