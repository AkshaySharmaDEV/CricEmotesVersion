using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyB : MonoBehaviour {

    public Material skyOne;
    public Material skyTwo;
    public Material skyThree;
    public Material skyFour;
    public Material skyFive;


    // Use this for initialization
    void Start () {

        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Sky1(){

        RenderSettings.skybox = skyOne;

    }

    public void Sky2(){

        RenderSettings.skybox = skyTwo;

    }
    public void Sky3(){

        RenderSettings.skybox = skyThree;

    }
    public void Sky4(){

        RenderSettings.skybox = skyFour;

    }
    public void Sky5(){

        RenderSettings.skybox = skyFive;

    }
}