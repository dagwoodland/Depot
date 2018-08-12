using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : OnClick {
    public Material musicEnabledMaterial;
    public Material musicDisabledMaterial;

    private void Start() {
        var musicEnabled = PlayerPrefs.GetString("Music Enabled", "true");
        if (musicEnabled == "false") {
            GetComponent<Renderer>().material = musicDisabledMaterial;
        }
    }

    public override void Clicked() {
        var musicEnabled = PlayerPrefs.GetString("Music Enabled", "true");
        if (musicEnabled == "true") {
            PlayerPrefs.SetString("Music Enabled", "false");
            GetComponent<Renderer>().material = musicDisabledMaterial;
        } else if (musicEnabled == "false") {
            PlayerPrefs.SetString("Music Enabled", "true");
            GetComponent<Renderer>().material = musicEnabledMaterial;
        } else {
            Debug.LogError("Invalid Music Enabled Value: \'" + musicEnabled + "\'");
        }
    }
}
