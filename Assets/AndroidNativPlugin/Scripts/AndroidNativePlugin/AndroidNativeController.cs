using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace waqashaxhmi.AndroidNativePlugin{
	public class AndroidNativeController : MonoBehaviour {

		#region UnityMethod
		void Awake(){
			gameObject.AddComponent<AndroidNativePluginLibrary> ();
		}
		#endregion

		#region Pick Files Callback
		public static System.Action<string> OnFileSelectSuccessEvent;
		/// <summary>
		/// Raises the success event.
		/// Callback by Android Library on picking file from device
		/// </summary>
		/// <param name="path">Path: Path of selected File</param>
		public void OnSuccess(string path){
			Debug.Log (path);
			if (OnFileSelectSuccessEvent != null)
				OnFileSelectSuccessEvent (path);
		}

		public static System.Action<string> OnFileSelectFailureEvent;
		/// <summary>
		/// Raises the failure event.
		/// Callback by Android Library on Failure of picking file from device.
		/// </summary>
		/// <param name="error">Error: Error on picking file.</param>
		public void OnFailure(string error){
			Debug.Log (error);
			if (OnFileSelectFailureEvent != null)
				OnFileSelectFailureEvent (error);
		}
		#endregion

		#region Dialogues Callback
		public static System.Action<string> OnPositiveButtonPressEvent;
		/// <summary>
		/// Positives the button press.
		/// </summary>
		/// <param name="message">Message.</param>
		public void PositiveButtonPress(string message){
			Debug.Log (message);
			if (OnPositiveButtonPressEvent != null)
				OnPositiveButtonPressEvent (message);
		}

		public static System.Action<string> OnNegativeButtonPressEvent;
		/// <summary>
		/// Negatives the button press.
		/// </summary>
		/// <param name="message">Message.</param>
		public void NegativeButtonPress(string message){
			Debug.Log (message);
			if (OnNegativeButtonPressEvent != null)
				OnNegativeButtonPressEvent (message);
		}
		#endregion
	}
}
