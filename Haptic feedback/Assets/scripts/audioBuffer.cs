using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audioBuffer : MonoBehaviour {
	AudioSource source;
	public static float[] values = new float[512]; 
	public static float[] freqBands = new float[8];
	public static float[] _bandBuffer = new float[8];
	float[] decreaseBuffer = new float[8];

	//Microphone Input
	public AudioClip _audioClip;
	public bool _useMicrophone;
	public string _selectedDevice;
	public AudioMixerGroup mixerGroupMic, mixerGroupSource;

	float[] _freqBandHeighest = new float[8];
	public static float[] _audioBand = new float[8];
	public static float[] _audioBandBuffer = new float[8];

	public static float _Amplitude, _AmplitudeBuffer;
	float _AmplitudeHighest;

	// Use this for initialization
	void Start () {
		source = FindObjectOfType<AudioSource> ();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		if (_useMicrophone) {
			if (Microphone.devices.Length > 0) {
				source.outputAudioMixerGroup = mixerGroupMic;

				_selectedDevice = Microphone.devices [0].ToString ();
				source.clip = Microphone.Start (_selectedDevice, true, 10, AudioSettings.outputSampleRate);
			}

		} else {
			source.outputAudioMixerGroup = mixerGroupSource;
			source.clip = _audioClip;
		}

		source.Play ();
	}

	void getAmplitude(){
		float _CurrentAmplitude = 0;
		float _CurrentAmpitudeBuffer = 0;

		for (int i = 0; i < 8; i++) {
			_CurrentAmplitude += _audioBand [i];
			_CurrentAmpitudeBuffer += _audioBandBuffer [i];
		}
		if (_CurrentAmplitude > _AmplitudeHighest) {
			_AmplitudeHighest = _CurrentAmplitude;
		}
		_Amplitude = _CurrentAmplitude / _AmplitudeHighest;
		_AmplitudeBuffer = _CurrentAmpitudeBuffer / _AmplitudeHighest;
	}

	void getSpectrumAudioSource(){
		source.GetSpectrumData (values , 0 , FFTWindow.Blackman);
	}

	// Update is called once per frame
	void Update () {
		getSpectrumAudioSource ();	
		makeFrequencyBands ();
		bandBuffer ();
		ceateAudioBands ();
		getAmplitude ();
	}

	void ceateAudioBands(){
		for (int i = 0; i < 8; i++) {
			if (freqBands [i] > _freqBandHeighest[i]) {
				_freqBandHeighest [i] = freqBands [i];
			}
			_audioBand [i] = (freqBands [i] / _freqBandHeighest [i]);
			_audioBandBuffer [i] = _bandBuffer [i] / _freqBandHeighest [i];
		}
	}

	void bandBuffer(){
		for (int i = 0; i < 8; i++) {
			if (freqBands [i] > _bandBuffer [i]) {
				_bandBuffer [i] = freqBands [i];
				decreaseBuffer [i] = 0.005f;
			} else {
				_bandBuffer [i] -= decreaseBuffer [i];
				decreaseBuffer [i] *= 1.2f;
			}
		}
	}

	void makeFrequencyBands(){
		int count = 0;

		for (int i = 0; i < 8; i++) {

			float avg = 0;
			int sampleCount =(int) Mathf.Pow (2, i)*2;

			if (i == 7) {
				sampleCount += 2;
			}

			for (int j = 0; j < sampleCount; j++) {
				avg += values [count] * (count + 1);
				count++;
			}

			avg /= count;

			freqBands [i] = avg * 10;
		}
	}
}
