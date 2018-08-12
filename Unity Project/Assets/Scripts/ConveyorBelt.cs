using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {

    public float speed;
    public float scrollSpeed;

    public string row;

    private GameManager gameManager;

    private void Start() {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }

    public void OnCollisionStay(Collision collision) {
        var oldVelocity = collision.rigidbody.velocity;
        var newVelocity = oldVelocity;
        newVelocity.x = -speed * gameManager.gameSpeed;
        collision.rigidbody.velocity = newVelocity;
    }

    private void Update() {
        var jammed = false;
        if (row == "top") {
            if (gameManager.topJammed) {
                jammed = true;
            }
        } else if (row == "middle") {
            if (gameManager.middleJammed) {
                jammed = true;
            }
        } else if (row == "bottom") {
            if (gameManager.bottomJammed) {
                jammed = true;
            }
        } else {
            Debug.LogError("Invalid Row: \'" + row + "\'");
        }

        if (!jammed) {
            var cube = transform.Find("Cube");
            var oldTextureOffset = cube.GetComponent<Renderer>().material.GetTextureOffset("_MainTex");
            var newOffset = new Vector2(oldTextureOffset.x - scrollSpeed * gameManager.gameSpeed * Time.deltaTime, oldTextureOffset.y);
            cube.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", newOffset);
        }
    }
}
