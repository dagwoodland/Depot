using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    public AudioSource audioSource;
    private GameManager gameManager;

    private void Start() {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update () {
        if (PlayerPrefs.GetString("Music Enabled", "true") == "false") {
            GetComponent<AudioSource>().Pause();
        } else {
               GetComponent<AudioSource>().UnPause();
        }

        audioSource.pitch = 1 + gameManager.gameSpeed / 10 ;

	}
}
