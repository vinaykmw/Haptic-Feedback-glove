using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class valueChanger : MonoBehaviour {

	public bool useBuffer;
	public int band;
	public float startScale = 2 , scaleMultiplier = 50;
	Material mat;
	public float ColPercentage = 1;

	public Color c0,c1,c2,c3,c4,c5,c,c6,c7;

	Color maxColor;
	// Use this for initialization
	void Start () {
		mat = GetComponent<MeshRenderer> ().materials [0];
		rb = gameObject.GetComponent <Rigidbody> ();


		if (band == 0) {
			maxColor = c0;
		}
		if (band == 1) {
			maxColor = c1;
		}
		if (band == 2) {
			maxColor = c2;
		}
		if (band == 3) {
			maxColor = c3;
		}
		if (band == 4) {
			maxColor = c4;
		}
		if (band == 5) {
			maxColor = c5;
		}
		if (band == 6) {
			maxColor = c6;
		}
		if (band == 7) {
			maxColor = c7;
		}
	}

	Rigidbody rb;

	// Update is called once per frame
	void Update () {
		if (useBuffer) {	//scene 1
			transform.localScale = new Vector3 (0.1f, audioBuffer._audioBandBuffer [band] * scaleMultiplier + startScale, 0.1f);
			Color color = Color.Lerp(Color.black, new Color (audioBuffer._audioBandBuffer [band], audioBuffer._audioBandBuffer [band], audioBuffer._audioBandBuffer [band]) ,audioBuffer._audioBand [band] * ColPercentage);
			mat.SetColor ("_EmissionColor", color);

		} else {	//scene 2
			transform.localScale = new Vector3 (audioBuffer._audioBand [band] * scaleMultiplier + startScale, audioBuffer._audioBand [band] * scaleMultiplier + startScale, audioBuffer._audioBand [band] * scaleMultiplier + startScale);
			//Color color = new Color (audioBuffer._audioBandBuffer [band], audioBuffer._audioBandBuffer [band], audioBuffer._audioBandBuffer [band]);
			Color color = Color.Lerp(Color.black, maxColor ,audioBuffer._audioBand [band] * ColPercentage);
			mat.SetColor ("_EmissionColor", color);
		}

	}
}
