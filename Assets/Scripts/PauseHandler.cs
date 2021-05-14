using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public GameObject objectToDeActivate;
    public GameObject objectToActivate;


    

    public void PauseStart()
    {

        StartCoroutine(DeactivateRoutine());

    }

    private IEnumerator DeactivateRoutine()
    {        
        //Wait for 14 secs.
        yield return new WaitForSeconds(4);

        //Turn My game object that is set to false(off) to True(on).
        objectToDeActivate.SetActive(false);

        //Turn the Game Oject back off after 1 sec.
        yield return new WaitForSeconds(20);

        //Game object will turn off
        objectToActivate.SetActive(true);
    }
}
 
