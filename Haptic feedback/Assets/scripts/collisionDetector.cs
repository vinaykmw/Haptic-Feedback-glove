using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Ports;
using System;
using System.Threading;

public class collisionDetector : MonoBehaviour {

	public soundManager s;
	public ArduinoConnection ad;
	private string rdata;
	private string[] vec3;
	private bool isRunning=false;
	private Vector3 accelnew ;
	private Vector3 accel ;
	private Vector3 dir;
	int i=0;
	//public Rigidbody rb;

	static SerialPort sp = new SerialPort("COM3", 38400); //Set the port (com4) and the baud rate (9600, is standard on most devices) 

	void Start () {
		sp.Open ();
		sp.Write("0x");
		sp.Write("0y");


	}

	void CalculateDistance(GameObject obj){
		Debug.Log ((obj.transform.position - gameObject.transform.position).magnitude + " " + gameObject.tag);
	}

	void OnCollisionStay(Collision col){
		//CalculateDistance (col.gameObject);
	}

	void OnCollisionExit (Collision col)		//TODO add functions here!!!
	{
		GenericSerialLoop (false, 1);
		if (gameObject.tag.Equals ("index")) {
			sp.Write(120 + ""+"y");

			//GenericSerialLoop (true, 1);
		} else if (gameObject.tag.Equals ("thumb")) {
			sp.Write(120 + ""+"x");

			//GenericSerialLoop (true, 0);
		}
	}

	void OnCollisionEnter (Collision col)		//TODO add functions here!!!
	{
		if(col.gameObject.tag == "sphere")
		{
			Debug.Log ("Touching!");
		}
		if(col.gameObject.tag == "b1")
		{
			s.changeMusic (0);
		}
		if(col.gameObject.tag == "b2")
		{
			s.changeMusic (1);
		}
		if(col.gameObject.tag == "b3")
		{
			s.changeMusic (2);
		}
		if (gameObject.tag.Equals ("index")) {
			sp.Write(0 + ""+"y");

			//GenericSerialLoop (true, 1);
		} else if (gameObject.tag.Equals ("thumb")) {
			sp.Write(180 + ""+"x");

			//GenericSerialLoop (true, 0);
		}
	}


	public void GenericSerialLoop(bool status,int portNum)
	{	//Debug.Log ("SDdddddddddddddddddddd");
		try
		{	
			// Check that the port is open. If not skip and do nothing
			if (sp.IsOpen)
			{	if(i==180)
				i=0;
				//if(Input.GetKeyDown(KeyCode.Space))
				//	{
				if(status){
					Debug.Log("hello");
					i=0;
				}else{
					i=120;
				}

				if(portNum==1){
					sp.Write(i.ToString()+"y");
				}
				if(portNum==0){
					{			Debug.Log ("tatti");
						
						sp.Write(i.ToString()+"x");
					}
					//}
				}
			}
		}
		catch (Exception ex)
		{
			// This could be thrown if we close the port whilst the thread 
			// is reading data. So check if this is the case!
			if (sp.IsOpen) {
				// Something has gone wrong!
				Debug.Log (ex.Message.ToString ());
			} else
				print ("Port closed");
		}
	}
}
