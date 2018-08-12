using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour {
    
    public Text incorrectCountText;
    public Text correctCountText;

    [Space]
    public GameObject incorrectPanel;
    public GameObject correctPanel;

    public float textDisplayTime;

    private void Update() {
        if (GameManager.gameRunning) {
            incorrectCountText.gameObject.SetActive(true);
            correctCountText.gameObject.SetActive(true);
        } else {
            incorrectCountText.gameObject.SetActive(false);
            correctCountText.gameObject.SetActive(false);
        }
    }

    public void UpdateTotals() {
        incorrectCountText.text = "Incorrect: " + GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>().incorrectlySorted.ToString();
        correctCountText.text = "Correct: " + GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>().correctlySorted.ToString();
    }

    public void DisplayCorrect() {
        incorrectPanel.SetActive(false);
        correctPanel.SetActive(true);
        StartCoroutine(HideCorrect());
        UpdateTotals();
    }

    public void DisplayIncorrect(Parcel parcel) {
        correctPanel.SetActive(false);
        incorrectPanel.SetActive(true);

        var reasonText = "It was ";

        if (parcel.damaged) {
            reasonText += "damaged.";
        } else if (parcel.weight >= 5) {
            reasonText += "too heavy.";
        } else if (!parcel.priority) {
            reasonText += "normal.";
        } else {
            reasonText += "priority.";
        }

        incorrectPanel.transform.Find("Reason Text").GetComponent<Text>().text = reasonText;
        StartCoroutine(HideIncorrect());
        UpdateTotals();
    }

    IEnumerator HideCorrect() {
        yield return new WaitForSeconds(textDisplayTime);
        correctPanel.SetActive(false);
    }

    IEnumerator HideIncorrect() {
        yield return new WaitForSeconds(textDisplayTime);
        incorrectPanel.SetActive(false);
    }
}
