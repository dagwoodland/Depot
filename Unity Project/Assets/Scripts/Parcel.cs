using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parcel : MonoBehaviour {

    public bool priority;
    public bool damaged;
    public float weight;

    [Space]
    public Material redStampMaterial;

    public GameObject stamp;
    public TextMesh weightText;

    public Mesh damagedMesh;
    public Material damagedMaterial;

    public SpawnPoint spawnPoint;

    public string row;

	// Use this for initialization
	void Start () {
        if (priority) {
            stamp.GetComponent<Renderer>().material = redStampMaterial;
        }

        if (damaged) {
            SetDamaged(particles: false);
        }

        weightText.text = weight.ToString("F1") + "kg";
	}

    public void SetDamaged(bool particles) {
        if (particles) {
            transform.Find("Damaged Particles").GetComponent<ParticleSystem>().Play();
            SoundManager.PlaySound("Box Crush");
        }

        damaged = true;
        transform.Find("Model").GetComponent<MeshFilter>().mesh = damagedMesh;
        transform.Find("Model").GetComponent<Renderer>().material = damagedMaterial;
        //transform.Find("Model").localScale = new Vector3(50, 50, 50);
    }
}
