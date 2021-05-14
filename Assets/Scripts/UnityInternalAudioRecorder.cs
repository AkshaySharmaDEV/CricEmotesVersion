using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class UnityInternalAudioRecorder : MonoBehaviour {

	public Text infoText;

	private int bufferSize;
	private int numBuffers;
	private int outputRate = 48000;
	private int headerSize = 44;//Wav file header size
	private string WAV_AUDIO_PATH;
	private bool recordAudio;
	private FileStream fileStream;
	private AudioSource[] allAudioSources;

	void Awake(){
		AudioSettings.outputSampleRate = outputRate;
	}

	void Start () {
		AudioSettings.GetDSPBufferSize (out bufferSize,out numBuffers);
		ResetAudioSource();
	}

	/// <summary>
	/// UI button OnClick event
	/// </summary>
	public void StartRecordingAudio(){
		if (!recordAudio) {
			infoText.text = "";
			//Debug.Log ("Started audio recorder");
			StartCoroutine (DelayCallRecord ());
		}
	}
	private IEnumerator DelayCallRecord()
	{
		yield return new WaitForSeconds(3f);
		//Debug.Log ("Started audio recorder");
		infoText.text = "";
		StartWriting ();
	}

	/// <summary>
	/// UI button OnClick event
	/// </summary>
	public void StopRecordingAudio(){
		recordAudio = false;
		WriteWAVHeader ();
		//Debug.Log ("Internal audio recorder stopped");
		infoText.text = "Encoding & Saving to the Gallary...";
		StartCoroutine (DelayStopRecord ());
	}
	private IEnumerator DelayStopRecord()
	{
		yield return new WaitForSeconds(3f);
		foreach (AudioSource audioS in allAudioSources) {
			audioS.Stop ();
			audioS.Play ();
		}
		infoText.text = ".";
	}

	void StartWriting(){
		WAV_AUDIO_PATH = Application.persistentDataPath + "/sound.wav";

		if (File.Exists (WAV_AUDIO_PATH))
			File.Delete (WAV_AUDIO_PATH);
		
		fileStream = new FileStream (WAV_AUDIO_PATH, FileMode.Create);
		WriteEmptyByte ();
		recordAudio = true;
		ResetAudioSource ();
	}

	/// <summary>
	/// Write 44 empty bytes to reserve space for WAV header
	/// </summary>
	void WriteEmptyByte(){
		byte emptyByte = new byte ();

		for (int i = 0; i < headerSize; i++) {
			fileStream.WriteByte (emptyByte);
		}

	}

	//This is called by unity when bunch of audio samples gets accumulates
	//4096 when tested in hp-pavilion laptop Windows 10 64-bit
	//Todo : Figure out size of chunk in android devices
	void OnAudioFilterRead(float[] data,int channels){
		if (recordAudio) {
			ConvertAndWrite (data);
		}
	}

	void ConvertAndWrite(float[] dataSource){
		Int16[] intData = new Int16[dataSource.Length];
		byte[] bytesData = new byte[dataSource.Length*2];

		int rescaleFactor = 32767;

		for (int i = 0; i < dataSource.Length; i++) {
			intData [i] = (Int16) (dataSource [i] * rescaleFactor);
			byte[] byteArr = new byte[2];
			byteArr = BitConverter.GetBytes (intData [i]);
			byteArr.CopyTo (bytesData, i * 2);
		}
		fileStream.Write (bytesData, 0, bytesData.Length);
	}

	/// <summary>
	/// Insert WAV header at the beginning of raw audio file stream
	/// </summary>
	void WriteWAVHeader(){

		fileStream.Seek (0, SeekOrigin.Begin);

		Byte[] riff = System.Text.Encoding.UTF8.GetBytes ("RIFF");
		fileStream.Write (riff, 0, 4);

		Byte[] chunkSize = BitConverter.GetBytes (fileStream.Length - 8);
		fileStream.Write (chunkSize, 0, 4);

		Byte[] wave = System.Text.Encoding.UTF8.GetBytes ("WAVE");
		fileStream.Write (wave, 0, 4);

		Byte[] fmt = System.Text.Encoding.UTF8.GetBytes ("fmt ");
		fileStream.Write (fmt, 0, 4);

		Byte[] subChunk1 = BitConverter.GetBytes (16);
		fileStream.Write (subChunk1, 0, 4);

		UInt16 two = 2;
		UInt16 one = 1;

		Byte[] audioFormat = BitConverter.GetBytes (one);
		fileStream.Write (audioFormat, 0, 2);

		Byte[] numChannels = BitConverter.GetBytes (two);
		fileStream.Write (numChannels, 0, 2);

		Byte[] sampleRate = BitConverter.GetBytes (outputRate);
		fileStream.Write (sampleRate, 0, 4);

		Byte[] byteRate = BitConverter.GetBytes (outputRate * 4);

		fileStream.Write (byteRate, 0, 4);

		UInt16 four = 4;
		Byte[] blockAlign = BitConverter.GetBytes (four);
		fileStream.Write (blockAlign, 0, 2);

		UInt16 sixteen = 16;
		Byte[] bitsPerSample = BitConverter.GetBytes (sixteen);
		fileStream.Write (bitsPerSample, 0, 2);

		Byte[] dataString = System.Text.Encoding.UTF8.GetBytes ("data");
		fileStream.Write (dataString, 0, 4);

		Byte[] subChunk2 = BitConverter.GetBytes (fileStream.Length - headerSize);
		fileStream.Write (subChunk2, 0, 4);

		fileStream.Close ();
	}

	/// <summary>
	/// Sometimes initializing/releasing MediaRecorder object causes crash of audio sources, which is restarted using following method
	/// </summary>
	private void ResetAudioSource(){
		allAudioSources = FindObjectsOfType (typeof(AudioSource)) as AudioSource[];
		foreach (AudioSource audioS in allAudioSources) {
			audioS.Stop ();
			audioS.Play ();
		}
	}
}