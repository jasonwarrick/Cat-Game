using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Meter : MonoBehaviour {
    public delegate void MeterFull(string meterName);
    public static MeterFull meterFull;

    new string name;
    public string Name {
        get { return name; }
        set { name = value; }
    }

    float reading;
    public float Reading {
        get { return reading; }
    }

    public bool isEnabled = false;

    public Meter(string newName, float startReading) {
        name = newName;
        reading = startReading;
        isEnabled = true;
    }

    public Meter(string newName, float startReading, bool newIsEnabled) {
        name = newName;
        reading = startReading;
        isEnabled = newIsEnabled;
    }

    public void IncreaseReading(float newReading) {
        reading += newReading;

        if (reading >= 100f) {
            ResetReading();
        }
    }

    public void ResetReading() {
        reading = 0f;
        meterFull.Invoke(name);
    }
}

public class CatBrain : MonoBehaviour
{
    [SerializeField] Transform feedPoint;
    [SerializeField] float feedStart;
    [SerializeField] Transform drinkPoint;
    [SerializeField] float drinkStart;
    [SerializeField] Transform litterPoint;
    [SerializeField] float litterStart;
    [SerializeField] Transform playPoint;
    [SerializeField] float playStart;
    [SerializeField] Transform nailpoint;
    [SerializeField] float nailStart;
    [SerializeField] Transform medicinePoint;
    [SerializeField] float medicineStart;

    [SerializeField] float meterAmt;

    List<Meter> meters = new List<Meter>();

    CatNavigation catNavigation;

    void Start() {
        catNavigation = GetComponent<CatNavigation>();

        meters.Add(new Meter("feed", feedStart));
        meters.Add(new Meter("drink", drinkStart));
        meters.Add(new Meter("litter", litterStart));
        meters.Add(new Meter("play", playStart));
        meters.Add(new Meter("nail", nailStart, false));
        meters.Add(new Meter("medicine", medicineStart, false));
    }

    void FixedUpdate() {
        
    }

    void UpdateMeters() {
        foreach (Meter meter in meters) {
            if (meter.isEnabled) {
                meter.IncreaseReading(meterAmt);
            }
        }
    }

    public void UpdateSpecMeter(string meter, float newMeterAmt) { // Not sure if this will ever be used
        GetMeter(meter).IncreaseReading(newMeterAmt);
    }

    public Meter GetMeter(string meterName) { 
        foreach (Meter meter in meters) {
            if (meter.Name.Equals(meterName)) {
                return meter;
            }
        }

        return null;
    }
}
