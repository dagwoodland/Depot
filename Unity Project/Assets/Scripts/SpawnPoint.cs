using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {
    
    public GameObject parcelPrefab;

    public string row;

    private List<GameObject> parcelsInRow = new List<GameObject>();
    private bool isBlockedByParcel = false;

    private void OnTriggerEnter(Collider other) {
        isBlockedByParcel = true;
    }

    private void OnTriggerExit(Collider other) {
        isBlockedByParcel = false;
    }

    private void Update() {
        if (GameManager.gameRunning == false) {
            if (parcelsInRow.Count > 0) {
                parcelsInRow.Clear();
            }
            isBlockedByParcel = false;
        }
    }

    public void SpawnParcel() {
        if (isBlockedByParcel) {
            var success = false;
            foreach (GameObject parcel in parcelsInRow) {
                if (!parcel.GetComponent<Parcel>().damaged) {
                    success = true;
                    parcel.GetComponent<Parcel>().SetDamaged(particles: true);
                    break;
                }
            }
            if (success == false) {
                GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>().SetRowJammed(row);
            }
        } else {
            var spawnPos = transform.position;
            var parent = GameObject.FindGameObjectWithTag("Parcels").transform;

            var parcelObject = Instantiate(parcelPrefab, spawnPos, Quaternion.identity, parent);
            parcelsInRow.Add(parcelObject);
            var parcel = parcelObject.GetComponent<Parcel>();

            // Fix to avoid parcels, marked 5.0kg, being rejected as too heavy.
            parcel.weight = Random.Range(1.0f, 7.0f);
            if (parcel.weight > 4.9f && parcel.weight < 5.0f) {
                parcel.weight = 5.0f;
            }
            parcel.damaged = (Random.Range(0, 5) == 0);
            parcel.priority = (Random.Range(0, 5) == 0);
            parcel.spawnPoint = this;
            parcel.row = row;
        }
    }

    public void RemoveParcelFromRow(GameObject parcel) {
        parcelsInRow.Remove(parcel);
    }
}
