using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeManager : MonoBehaviour {

	public AudioSource source;
	public Vector3 initialPos;
	// Use this for initialization
	void Start () {
		initialPos = gameObject.transform.position;
	}

	void OnCollisionStay(Collision col){
		source.volume = (gameObject.transform.position.y - initialPos.y)*10;
		Debug.Log (source.volume);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
