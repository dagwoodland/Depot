using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool gameRunning = false;

    [HideInInspector]
    public float gameSpeed = 1;

    public float startSpawnInterval;

    [Header("Parcel Property Chances")]
    public float minWeight;
    public float maxWeight;
    public int damagedRarity;
    public int priorityRarity;

    private float gameTime = 0;
    private float spawnDelay = 0;

    public int correctlySorted;
    public int incorrectlySorted;

    public bool topJammed;
    public bool middleJammed;
    public bool bottomJammed;

    public TextMesh scoreText;
    public TextMesh breakdownText;
    public GameObject newHighscoreText;

	// Update is called once per frame
	void Update () {
        if (GameManager.gameRunning) {
            gameSpeed += Time.deltaTime / 30;
            gameTime += Time.deltaTime;

            spawnDelay -= Time.deltaTime;
            if (spawnDelay <= 0) {
                var spawns = GameObject.FindGameObjectsWithTag("Parcel Spawn");
                var randomNumber = Random.Range(0, spawns.Length);
                spawns[randomNumber].GetComponent<SpawnPoint>().SpawnParcel();

                spawnDelay = startSpawnInterval - gameSpeed / 2;
                if (spawnDelay < 0.5f) {
                    spawnDelay = 0.5f;
                }
            }

            if (topJammed || middleJammed || bottomJammed) {
                GameOver(true);
            }
        }
	}

    public void GameOver(bool goToGameOver) {
        var highscore = PlayerPrefs.GetInt("Highscore", 0);
        var score = correctlySorted - incorrectlySorted;
        scoreText.text = score.ToString();
        breakdownText.text = "Correct (" + correctlySorted.ToString() + ") - Incorrect (" + incorrectlySorted.ToString() + ") = " + score.ToString();

        if (score > highscore) {
            PlayerPrefs.SetInt("Highscore", score);
            newHighscoreText.SetActive(true);
        } else {
            newHighscoreText.SetActive(false);
        }

        gameSpeed = 1;
        topJammed = false;
        middleJammed = false;
        bottomJammed = false;
        gameTime = 0;
        spawnDelay = 0;
        correctlySorted = 0;
        incorrectlySorted = 0;
        GameManager.gameRunning = false;

        GameObject.FindGameObjectWithTag("Canvas").GetComponent<MainCanvas>().UpdateTotals();

        if (goToGameOver) {
            Invoke("PanToGameOver", 2.0f);
            Invoke("DestroyRemainingParcels", 2.5f);
            Invoke("ResetJammedText", 2.5f);
        } else {
            Invoke("DestroyRemainingParcels", 0.5f);
            Invoke("ResetJammedText", 0.5f);
        }
    }

    public void PanToGameOver() {
        Camera.main.GetComponent<Animator>().SetTrigger("PanToGameOver");
    }

    public void ResetJammedText() {
        GameObject.FindGameObjectWithTag("Top Conveyor").transform.Find("Jammed Text").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Middle Conveyor").transform.Find("Jammed Text").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Bottom Conveyor").transform.Find("Jammed Text").gameObject.SetActive(false);
    }

    public void DestroyRemainingParcels() {
        var parcels = GameObject.FindGameObjectWithTag("Parcels").transform;

        for (int i = 0; i < parcels.childCount; i++) {
            Destroy(parcels.GetChild(i).gameObject);
        }
    }

    // row = top, middle, bottom
    public void SetRowJammed(string row) {
        Debug.Log(row + "jammed");
        if (row == "top") {
            topJammed = true;
            GameObject.FindGameObjectWithTag("Top Conveyor").transform.Find("Jammed Text").gameObject.SetActive(true);
        } else if (row == "middle") {
            GameObject.FindGameObjectWithTag("Middle Conveyor").transform.Find("Jammed Text").gameObject.SetActive(true);
            middleJammed = true;
        } else if (row == "bottom") {
            GameObject.FindGameObjectWithTag("Bottom Conveyor").transform.Find("Jammed Text").gameObject.SetActive(true);
            bottomJammed = true;
        } else {
            Debug.LogError("Invalid Row: \'" + row + "\'");
        }
    }

}
