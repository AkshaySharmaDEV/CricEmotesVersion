using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(NativeScreenRecorderAndMuxer))]
public class NativePluginManager : MonoBehaviour
{
	/// <summary>
	///  This class calls methods in the NativeScreenRecorderAndMuxer class
	///  Screen recording OnClick buttons call methods are present in this class
	/// </summary>

	private NativeScreenRecorderAndMuxer recorder;
	private string WAV_AUDIO_PATH;
	private string AAC_AUDIO_PATH;
	private string RAW_MP4_PATH;

	private void Start ()
	{
		WAV_AUDIO_PATH = Application.persistentDataPath + "/sound.wav";
		AAC_AUDIO_PATH = Application.persistentDataPath + "/sound.aac";
		RAW_MP4_PATH = Application.persistentDataPath + "/visual.mp4";

		recorder = GetComponent<NativeScreenRecorderAndMuxer> ();
		recorder.WriteExternalPermission ();
	}

	/// <summary>
	/// UI button OnClick event
	/// </summary>
	public void StartRecord ()
	{
		recorder.PrepareRecorder ();
		StartCoroutine (DelayCallRecord ());
	}

	private IEnumerator DelayCallRecord ()
	{
		yield return new WaitForSeconds (5f);
		recorder.StartRecording ();
	}

	/// <summary>
	/// UI button OnClick event
	/// </summary>
	public void StopRecord ()
	{
		recorder.StopRecording ();
	}
}
