using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour {
    public delegate void MeterFull(Meter meter);
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
            meterFull.Invoke(this);
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
    // [SerializeField] float feedStart;
    [SerializeField] Transform drinkPoint;
    // [SerializeField] float drinkStart;
    [SerializeField] Transform litterPoint;
    // [SerializeField] float litterStart;
    [SerializeField] Transform playPoint;
    // [SerializeField] float playStart;
    [SerializeField] Transform nailPoint;
    // [SerializeField] float nailStart;
    [SerializeField] Transform medicinePoint;
    // [SerializeField] float medicineStart;

    [SerializeField] float meterAmt;
    [SerializeField] float meterResetRange;
    [SerializeField] float meterStartRange;
    [SerializeField] float meterStartBuffer;

    float timer = 0f;
    float counter = 0f;
    [SerializeField] float minTimer;
    [SerializeField] float maxTimer;

    List<Meter> meters = new List<Meter>();
    List<float> meterStarts = new List<float>();

    void Start() {

        // for (int i = 0; i < 3; i++) {
        //     float newStart = Random.Range(0f, meterStartRange);

        //     foreach (float start in meterStarts) {
        //         if (newStart > start - meterStartBuffer && newStart < start + meterStartBuffer) {
        //             newStart = start - meterStartBuffer;

        //             if (newStart < 0) {
        //                 newStart = start + meterStartBuffer;
        //             }
        //         }
        //     }

        //     meterStarts.Add(newStart);

        //     // Debug.Log(newStart);
        // }

        meters.Add(new Meter("feed", 1f, feedPoint));
        meters.Add(new Meter("drink", 1f, drinkPoint));
        meters.Add(new Meter("litter", Random.Range(0f, meterStartRange), litterPoint));
        meters.Add(new Meter("play", 1f, playPoint));
        // meters.Add(new Meter("nail", Random.Range(0f, meterStartRange), nailPoint, false));
        // meters.Add(new Meter("medicine", Random.Range(0f, meterStartRange), medicinePoint, false));

        SetTimer();
    }

    void FixedUpdate() {
        UpdateMeters();
    }

    void SetTimer() {
        timer = Random.Range(minTimer, maxTimer);
    }

    void UpdateMeters() {
        counter += Time.deltaTime;

        if (counter >= timer) {
            List<int> openIndices = new List<int> {};

            for (int i = 0; i < meters.Count; i++) {
                if (!meters[i].danger) {
                    openIndices.Add(i);
                }
            }

            if (openIndices.Count > 0) {
                int newIndex = Random.Range(0, openIndices.Count);

                meters[openIndices[newIndex]].StartDanger();
                counter = 0f;
                SetTimer();

                openIndices.Clear();
            }
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
