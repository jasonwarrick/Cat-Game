using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Meter : MonoBehaviour {
    public delegate void MeterFull(Meter meter);
    public static MeterFull meterFull;

    public delegate void MeterDanger(Meter meter);
    public static MeterDanger meterDanger;

    new string name;
    public string Name {
        get { return name; }
        set { name = value; }
    }

    float reading;
    public float Reading {
        get { return reading; }
    }

    Transform waitPoint;
    public Transform WaitPoint {
        get { return waitPoint; }
    }

    public bool isEnabled = false;
    public bool danger = false;

    float dangerPoint = 75f;

    public Meter(string newName, float startReading, Transform newWaitPoint) {
        name = newName;
        reading = startReading;
        waitPoint = newWaitPoint;
        isEnabled = true;
    }

    public Meter(string newName, float startReading, Transform newWaitPoint, bool newIsEnabled) {
        name = newName;
        reading = startReading;
        waitPoint = newWaitPoint;
        isEnabled = newIsEnabled;
    }

    public void IncreaseReading(float newReading) {
        reading += newReading;
        // Debug.Log(name + " is now at " + reading);

        if (reading >= dangerPoint && !danger) {
            danger = true;
            meterDanger.Invoke(this);
        }

        if (reading >= 100f) {
            ResetReading();
        }
    }

    public void ResetReading() {
        reading = 0f;
        meterFull.Invoke(this);
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
    [SerializeField] Transform nailPoint;
    [SerializeField] float nailStart;
    [SerializeField] Transform medicinePoint;
    [SerializeField] float medicineStart;

    [SerializeField] float meterAmt;

    List<Meter> meters = new List<Meter>();

    CatNavigation catNavigation;

    void Start() {
        catNavigation = GetComponent<CatNavigation>();

        meters.Add(new Meter("feed", feedStart, feedPoint));
        meters.Add(new Meter("drink", drinkStart, drinkPoint));
        meters.Add(new Meter("litter", litterStart, litterPoint));
        meters.Add(new Meter("play", playStart, playPoint));
        meters.Add(new Meter("nail", nailStart, nailPoint, false));
        meters.Add(new Meter("medicine", medicineStart, medicinePoint, false));
    }

    void FixedUpdate() {
        UpdateMeters();
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
