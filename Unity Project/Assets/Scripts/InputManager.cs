using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private GameObject selectedBox;

    private bool IsRowJammed(string row) {
        var gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        if (row == "top") {
            return gameManager.topJammed;
        } else if (row == "middle") {
            return gameManager.middleJammed;
        } else if (row == "bottom") {
            return gameManager.bottomJammed;
        } else {
            Debug.LogError("Invalid Row: \'" + row + "\'");
            return false;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.GetComponent<Parcel>() != null) {
                    if (GameManager.gameRunning) {
                        selectedBox = hit.transform.gameObject;
                        selectedBox.GetComponent<Rigidbody>().isKinematic = true;
                        selectedBox.GetComponent<Parcel>().spawnPoint.RemoveParcelFromRow(selectedBox);
                        SoundManager.PlaySound("Pickup Box");
                    }
                }
            }
        }

        if (selectedBox != null) {
            var mousePoint = Input.mousePosition;
            mousePoint.z = Vector3.Distance(Camera.main.transform.position, selectedBox.transform.position) - 1f;
            var mousePos = Camera.main.ScreenToWorldPoint(mousePoint);
            var newPos = selectedBox.transform.position;
            newPos.x = mousePos.x;

            if (newPos.x < -2.6) {
                newPos.x = -2.6f;
            }

            if (newPos.x > 2.6) {
                newPos.x = 2.6f;
            }

            newPos.y = mousePos.y;
            newPos.z = -8f;

            selectedBox.transform.position = newPos;
        }

        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
        if (Input.GetMouseButtonUp(0) || !screenRect.Contains(Input.mousePosition) || !GameManager.gameRunning) {
            if (selectedBox != null) {
                SoundManager.PlaySound("Drop Box");
                selectedBox.GetComponent<Rigidbody>().isKinematic = false;
                selectedBox = null;
            }
        }
    }
	
}
