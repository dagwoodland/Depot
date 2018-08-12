using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour {

    [Header("OnClick")]
    public bool darkenOnHover = true;
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.gameObject.name == gameObject.name) {
                if (darkenOnHover) {
                    GetComponent<Renderer>().material.SetColor("_Color", new Color(1,1,1,0.7f));
                }
                if (Input.GetMouseButtonDown(0)) {
                    Clicked();
                    SoundManager.PlaySound("Button Click");
                }
            } else {
                GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            }
        }
	}

    public virtual void Clicked() {
        Debug.Log("Clicked");
    }
}
