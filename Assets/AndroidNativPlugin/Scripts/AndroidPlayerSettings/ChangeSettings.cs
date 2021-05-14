using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChangeSettings : MonoBehaviour {


	void Awake()
	{
		#if UNITY_EDITOR
		UnityEditor.PlayerSettings.Android.forceSDCardPermission = true;
		#endif
	}

}
