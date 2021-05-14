using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanalControl : MonoBehaviour
{
    private Animator animation;
    

    public bool toggleState;



    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

      


    public void toggleStateFunction()
    {
        if (toggleState)
        {
            Debug.Log("Toggle disabled!");
            // animator.SetTrigger("myToggleDisabledAnimation")
            animation.Play("EmotesBack");
            toggleState=false;
        }
        else
        {
            Debug.Log("Toggle enabled!");
            // animator.SetTrigger("myToggleEnabledAnimation")
            animation.Play("Emotes");
            toggleState=true;
        }
    }

    
}


