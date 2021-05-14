using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace waqashaxhmi.AndroidNativePlugin{
	public class AndroidNativePluginLibrary : MonoBehaviour {


		public static AndroidNativePluginLibrary Instance;

		#if UNITY_ANDROID
		private AndroidJavaClass androidObject;
		#endif
		// Use this for initialization
		void Awake () {
			Instance = this;
			#if UNITY_ANDROID
			if (Application.platform == RuntimePlatform.Android) {
				androidObject = new AndroidJavaClass ("com.example.androidnativelibrary.AndroidBridge");
			}
			#endif
		}

		#region ShowToast
		/// <summary>
		/// Shows the toast.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="Length">Toast Duration, For Short 0,For Long 1.</param>
		public void ShowToast(string message,int Length = 0){
			#if UNITY_ANDROID
			androidObject.CallStatic("ShowToast", message,Length);
			#endif
		}
		#endregion
		#region OpenGallary
		/// <summary>
		/// Opens the gallary.
		/// </summary>
		public void OpenGallary(){
			#if UNITY_ANDROID
			androidObject.CallStatic("OpenGallary");
			#endif
		}
		#endregion

		#region ShowMessage
		public void ShowMessage(string title,string message,string buttonName= "OK"){
			#if UNITY_ANDROID
			androidObject.CallStatic ("ShowMessage", title, message,buttonName);
			#endif
		}
		#endregion
		public void ShowConfirmationDialouge(string title,string message,string positiveButtonName= "Yes",string negativeButtonName= "NO"){
			#if UNITY_ANDROID
			androidObject.CallStatic ("ConfirmationDialouge", title, message,positiveButtonName,negativeButtonName);
			#endif
		}

		#region ShowProgressBar
		/// <summary>
		/// Shows the progress bar.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="message">Message.</param>
		/// <param name="cancelable">If set to <c>true</c> cancelable.On Click outside disable Progress</param>
		/// <param name="style">Style.Horizontal = 1 Spinner = 2</param>
		public void ShowProgressBar(string title ="",string message="",bool cancelable = true){
			#if UNITY_ANDROID
			androidObject.CallStatic ("ShowProgressBar",title,message,cancelable);
			#endif
		}
		public void DismissProgressBar(){
			#if UNITY_ANDROID
			androidObject.CallStatic ("DismissProgressBar");
			#endif
		}
		#endregion
	}
}
