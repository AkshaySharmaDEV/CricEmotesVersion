﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotDelay : MonoBehaviour
{

     public GameObject panal;
     public GameObject ButtonPanal;
     public GameObject ShowShot;
   

     public float sec = 1f;
     

    
     IEnumerator LateCall()
     {
 
         yield return new WaitForSeconds(sec);
 
         panal.SetActive(true);
         ButtonPanal.SetActive(true);
         ShowShot.SetActive(true);

         

         //Do Function here...
         
  
     }

     

     

     public void show()
     {

         
         if (panal.activeInHierarchy)
         {

             panal.SetActive(false);
             ButtonPanal.SetActive(false);

         }

         
         

         StartCoroutine(LateCall());


     }
     



   
}
