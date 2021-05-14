using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NativeScreenRecorderAndMuxer : MonoBehaviour
{
	/// <summary>
	/// This class initializes the native plugin intent.
	/// 
	/// Required plugins and resources:
	/// 1. Assets->Plugins->Android->screenrecorderlib-release_1mbs_2
	/// 2. Assets->Plugins->Android->aspectjrt-1.8.2
	/// 3. Assets->StreamingAssets->isoparser-default.properties
	/// </summary>

	private const string videoName = "visual";
	private float displayWidth = 720f;
    public static UnityAction onAllowCallback, onDenyCallback, onDenyAndNeverAskAgainCallback;
	#if UNITY_ANDROID && !UNITY_EDITOR
	private AndroidJavaObject androidRecorder;
	#endif

    void Start(){
		#if UNITY_ANDROID && !UNITY_EDITOR
	    using (AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
	    {
	        androidRecorder = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
			int width = (int)(Screen.width > displayWidth ? displayWidth : Screen.width);
			int height = Screen.width > displayWidth ? (int)(Screen.height * displayWidth / Screen.width) : Screen.height;
	        androidRecorder.Call("setupVideo", width, height,(int)(1f * width * height / 100 * 240 * 7), 30);
	        androidRecorder.Call("setCallback","AndroidUtils","VideoRecorderCallback");
	    }
		#endif
    }

	/// <summary>
	/// Request this permission to write recorded file to disk
	/// </summary>
	public void WriteExternalPermission(){
		if (!NativeScreenRecorderAndMuxer.IsPermitted(AndroidPermission.WRITE_EXTERNAL_STORAGE))
		{
			NativeScreenRecorderAndMuxer.RequestPermission(AndroidPermission.WRITE_EXTERNAL_STORAGE);
			onDenyCallback = () => { ShowToast("Need this permission to save video"); };
			onDenyAndNeverAskAgainCallback = () => { ShowToast("Need this permission to save video"); };
		}
	}

	public static void RequestPermission(AndroidPermission permission, 
		UnityAction onAllow = null, 
		UnityAction onDeny = null, UnityAction onDenyAndNeverAskAgain = null) {
		#if UNITY_ANDROID && !UNITY_EDITOR
		onAllowCallback = onAllow;
		onDenyCallback = onDeny;
		onDenyAndNeverAskAgainCallback = onDenyAndNeverAskAgain;
		using (var androidUtils = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
		androidUtils.GetStatic<AndroidJavaObject>("currentActivity").Call("requestPermission", GetPermissionStrr(permission));
		}
		#endif
	}

    /// <summary>
	/// This method creates a MediaRecorder object and sets VirtualDisplay from Plugins->Android->screenrecorderlib-release_1mbs_2.aar file
    /// </summary>
    public void PrepareRecorder(){
		#if UNITY_ANDROID && !UNITY_EDITOR
        androidRecorder.Call("setFileName", videoName);
        androidRecorder.Call("prepareRecorder");
		#endif
    }

    public void StartRecording(){
		#if UNITY_ANDROID && !UNITY_EDITOR
		androidRecorder.Call("startRecording");
		#endif
    }

    public void StopRecording(){
		#if UNITY_ANDROID && !UNITY_EDITOR
		androidRecorder.Call("stopRecording");
		#endif
    }

    public void VideoRecorderCallback(string message){
		Debug.Log (message);
    }

    private void OnAllow() {
        if (onAllowCallback != null)
            onAllowCallback();
        ResetAllCallBacks();
    }
		
    private void OnDeny() {
        if (onDenyCallback != null)
            onDenyCallback();
        ResetAllCallBacks();
    }

	private void OnDenyAndNeverAskAgain() {
        if (onDenyAndNeverAskAgainCallback != null)
            onDenyAndNeverAskAgainCallback();
        ResetAllCallBacks();
    }

    private void ResetAllCallBacks() {
        onAllowCallback = null;
        onDenyCallback = null;
        onDenyAndNeverAskAgainCallback = null;
    }

    public static bool IsPermitted(AndroidPermission permission) {
		#if UNITY_ANDROID && !UNITY_EDITOR
	    using (var androidUtils = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
	    {
	        return androidUtils.GetStatic<AndroidJavaObject>("currentActivity").Call<bool>("hasPermission", GetPermissionStrr(permission));
	    }
		#endif
        return true;
    }

    private static string GetPermissionStrr(AndroidPermission permission) {
        return "android.permission." + permission.ToString();
    }

    public static void ShowToast(string message) {
		#if UNITY_ANDROID && !UNITY_EDITOR
	    AndroidJavaObject currentActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
	    currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
	    {
	        new AndroidJavaClass("android.widget.Toast").CallStatic<AndroidJavaObject>("makeText", currentActivity.Call<AndroidJavaObject>("getApplicationContext"), new AndroidJavaObject("java.lang.String", message), 0).Call("show");
	    }));
		#endif
    }
		
	private void OnDestroy(){
		#if UNITY_ANDROID && !UNITY_EDITOR
		androidRecorder.Call("cleanUpRecorder");
		#endif
	}
}


public enum AndroidPermission
{
	ACCESS_COARSE_LOCATION,
	ACCESS_FINE_LOCATION,
	ADD_VOICEMAIL,
	BODY_SENSORS,
	CALL_PHONE,
	CAMERA,
	GET_ACCOUNTS,
	PROCESS_OUTGOING_CALLS,
	READ_CALENDAR,
	READ_CALL_LOG,
	READ_CONTACTS,
	READ_EXTERNAL_STORAGE,
	READ_PHONE_STATE,
	READ_SMS,
	RECEIVE_MMS,
	RECEIVE_SMS,
	RECEIVE_WAP_PUSH,
	RECORD_AUDIO,
	SEND_SMS,
	USE_SIP,
	WRITE_CALENDAR,
	WRITE_CALL_LOG,
	WRITE_CONTACTS,
	WRITE_EXTERNAL_STORAGE
}