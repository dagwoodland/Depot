using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : MonoBehaviour {

    private TextMesh textMesh;

	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMesh>();
        InvokeRepeating("UpdateHighscoreText", 0, 0.5f);
	}

    public void UpdateHighscoreText() {
        textMesh.text = "Highscore: " + PlayerPrefs.GetInt("Highscore", 0).ToString();
    }
	
}
