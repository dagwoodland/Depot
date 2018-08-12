using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {
    public static void PlaySound(string name) {
        if (PlayerPrefs.GetString("SFX Enabled", "true") == "true") {
            var clip = Resources.Load<AudioClip>("Audio/" + name);
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
    }
}
