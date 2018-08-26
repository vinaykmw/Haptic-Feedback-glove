using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour {

	public AudioSource source;
	public AudioClip s1, s2, s3;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeMusic(int i){
		if(i==0)
		{
			if (!source.clip.Equals (s1)) {
				source.clip = s1;
				source.Play ();
			}
		}
		if(i==1)
		{
			if (!source.clip.Equals (s2)) {
				source.clip = s2;
				source.Play ();
			}
		}
		if(i==2)
		{
			if (!source.clip.Equals (s3)) {
				source.clip = s3;
				source.Play ();
			}
		}
	}
}
