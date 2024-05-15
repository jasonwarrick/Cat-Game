using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour {
    public delegate void MeterFull(bool isMeter);
    public static MeterFull meterFull;

    public delegate void MeterDanger(Meter meter);
    public static MeterDanger meterDanger;

    public delegate void MeterReset(Meter meter);
    public static MeterReset meterReset;

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

    [SerializeField] float dangerInc;
    public bool isEnabled = false;
    public bool danger = false;

    // float dangerPoint = 60f;

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

    void FixedUpdate() {
        IncreaseReading(dangerInc);
    }

    public void IncreaseReading(float newReading) {
        reading += newReading;
        // Debug.Log(name + " is now at " + reading);

        // if (reading >= 60f && !danger) {
        //     danger = true;
        //     meterDanger.Invoke(this);
        // }

        Debug.Log("reading is now " + reading);

        if (reading >= 100f) {
            ResetReading(0f);
            meterFull.Invoke(true);
        }
    }

    public void StartDanger() {
        Debug.Log("Start " + name + " danger");
        danger = true;
        meterDanger.Invoke(this);
    }

    public void ResetReading(float range) {
        reading = Random.Range(0f, range);
        danger = false;
        meterReset.Invoke(this);
        Debug.Log(name + " is reset to " + reading);
    }
}

public class CatBrain : MonoBehaviour
{
    [SerializeField] Transform feedPoint;
    [SerializeField] Transform drinkPoint;
    [SerializeField] Transform litterPoint;
    [SerializeField] Transform playPoint;
    [SerializeField] Transform nailPoint;
    [SerializeField] Transform medicinePoint;

    [SerializeField] float meterAmt;
    [SerializeField] float meterResetRange;
    [SerializeField] float meterStartRange;

    float timer = 0f;
    float counter = 0f;
    [SerializeField] float minTimer;
    [SerializeField] float maxTimer;

    List<Meter> meters = new List<Meter>();
    List<int> availableMeters = new List<int> { 0, 1, 2, 3 };

    void Start() {
        meters.Add(new Meter("feed", 1f, feedPoint));
        meters.Add(new Meter("drink", 1f, drinkPoint));
        meters.Add(new Meter("litter", Random.Range(0f, meterStartRange), litterPoint));
        meters.Add(new Meter("play", 1f, playPoint));
        
        SetTimer();
    }

    void FixedUpdate() {
        if (!GameStateManager.instance.GetInMinigame()) {
            UpdateMeters();
        } else {
            Debug.Log("Blocking in minigame");
        }
    }

    void SetTimer() {
        timer = Random.Range(minTimer, maxTimer);
    }

    void UpdateMeters() {
        counter += Time.deltaTime;

        if (counter >= timer) {
            if (availableMeters.Count <= 0) {
                availableMeters = new List<int> { 0, 1, 2, 3 };
            }

            int newIndex = Random.Range(0, availableMeters.Count);

            meters[availableMeters[newIndex]].StartDanger();
            counter = 0f;
            SetTimer();

            availableMeters.RemoveAt(newIndex);
        }

        foreach (Meter meter in meters) {
            if (meter.isEnabled && meter.danger) {
                meter.IncreaseReading(meterAmt);
            }
        }
    }

    public void UpdateSpecMeter(string meter, float newMeterAmt) { // Not sure if this will ever be used
        GetMeter(meter).IncreaseReading(newMeterAmt);
    }

    public void ResetMeter(string meter) {
        GetMeter(meter).ResetReading(meterResetRange);
    }

    public Meter GetMeter(string meterName) { 
        foreach (Meter meter in meters) {
            if (meter.Name.Equals(meterName)) {
                return meter;
            }
        }

        return null;
    }

    public bool IsMeterDanger(string meterName) {
        return GetMeter(meterName).danger;
    }
}
