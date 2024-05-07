using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource charge;
    [SerializeField] AudioSource hit;
    [SerializeField] AudioSource make;

    public void StartCharge() {
        charge.Play();
    }

    public void StopCharge() {
        charge.Stop();
    }

    public void PoopHit() {
        hit.Play();
    }

    public void PoopMake() {
        make.Play();
        hit.Play();
    }
}
