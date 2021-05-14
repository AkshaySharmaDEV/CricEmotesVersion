using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateToUnlock : MonoBehaviour
{

    public GameObject RemoveWatermark;
    public GameObject RemoveWatermark1;
    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {


        counter = PlayerPrefs.GetInt ("Counter", 0);

        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (counter>0)
        {
            Destroy(RemoveWatermark);
            Destroy(RemoveWatermark1);
        }
        
    }




    public void RateUs()
    {
        Application.OpenURL("market://details?id=");
        
        
        counter ++;


        
        

        
        




    }

    
}
