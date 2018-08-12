using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcelSensor : MonoBehaviour {

    public enum SensorType { Damaged, TooHeavy, Normal, Priority };

    public SensorType sensorType;

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.GetComponent<Parcel>() != null) {
            var parcel = col.gameObject.GetComponent<Parcel>();

            switch (sensorType) {
                case SensorType.Damaged:
                    if (parcel.damaged) {
                        CorrectParcel();
                    } else {
                        IncorrectParcel(parcel);
                    }
                    break;
                case SensorType.TooHeavy:
                    if (parcel.weight >= 5 && !parcel.damaged) {
                        CorrectParcel();
                    } else {
                        IncorrectParcel(parcel);
                    }
                    break;
                case SensorType.Normal:
                    if (parcel.weight < 5 && !parcel.damaged && !parcel.priority) {
                        CorrectParcel();
                    } else {
                        IncorrectParcel(parcel);
                    }
                    break;
                case SensorType.Priority:
                    if (parcel.weight < 5 && !parcel.damaged && parcel.priority) {
                        CorrectParcel();
                    } else {
                        IncorrectParcel(parcel);
                    }
                    break;
            }

        }
    }

    private void IncorrectParcel(Parcel parcel) {
        SoundManager.PlaySound("Incorrect");
        GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>().incorrectlySorted += 1;
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<MainCanvas>().DisplayIncorrect(parcel);
    }

    private void CorrectParcel() {
        SoundManager.PlaySound("Correct");
        GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>().correctlySorted += 1;
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<MainCanvas>().DisplayCorrect();
    }
}
