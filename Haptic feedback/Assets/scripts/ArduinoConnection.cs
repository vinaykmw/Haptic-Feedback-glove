using UnityEngine;
using System.Collections;

using System.IO;
using System.IO.Ports;
using System;
using System.Threading;


public class ArduinoConnection : MonoBehaviour {

	private string rdata;
	private string[] vec3;
	private bool isRunning=false;
	private Vector3 accelnew ;
	private Vector3 accel ;
	private Vector3 dir;
	int i=0;
	//public Rigidbody rb;

	SerialPort sp = new SerialPort("COM3", 38400); //Set the port (com4) and the baud rate (9600, is standard on most devices) 

	void Start () {
		sp.Open ();
		StartSerialCoroutine ();
		sp.Write("0x");

	}


	void Update ()
	{	
		
	}



	public void StartSerialCoroutine()
	{
		isRunning = true;
	}


	public void GenericSerialLoop(bool status,int portNum)
	{	
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
				{
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
	//void OnTriggerEnter(Collider other)
}


