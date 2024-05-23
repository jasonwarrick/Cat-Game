using System.Collections;
using System.Collections.Generic;
using Picturesque.Darkbringer;
using UnityEngine;

public class FilterToggle : MonoBehaviour
{
    [SerializeField] DarkbringerEffect fx;

    void Start() {
        SettingsManager.filterToggled += ToggleFilter;
        
        // fx = GetComponent<DarkbringerEffect>();
    }

    void OnEnable() {
        if (GameStateManager.instance.filterActive) {
            fx.enabled = true;
        } else {
            fx.enabled = false;
        }
    }

    void OnDestroy() {
        SettingsManager.filterToggled -= ToggleFilter;
    }

    public void ToggleFilter() {
        fx.enabled = !fx.enabled;
    }
}
