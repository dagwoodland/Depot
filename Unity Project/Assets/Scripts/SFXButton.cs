using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXButton : OnClick {

    public Material sfxEnabledMaterial;
    public Material sfxDisabledMaterial;

    private void Start() {
        var sfxEnabled = PlayerPrefs.GetString("SFX Enabled", "true");
        if (sfxEnabled == "false") {
            GetComponent<Renderer>().material = sfxDisabledMaterial;
        }
    }

    public override void Clicked() {
        var sfxEnabled = PlayerPrefs.GetString("SFX Enabled", "true");
        if (sfxEnabled == "true") {
            PlayerPrefs.SetString("SFX Enabled", "false");
            GetComponent<Renderer>().material = sfxDisabledMaterial;
        } else if (sfxEnabled == "false") {
            PlayerPrefs.SetString("SFX Enabled", "true");
            GetComponent<Renderer>().material = sfxEnabledMaterial;
        } else {
            Debug.LogError("Invalid SFX Enabled Value: \'" + sfxEnabled + "\'");
        }
    }
}
