using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetBundle : MonoBehaviour
{
    string url = "https://drive.google.com/uc?export=download&id=10xNmExxI4BV08StF9AhM0tZpQquGWit4";
    // Start is called before the first frame update
    void Start()
    {
        WWW www = new WWW(url);
        StartCoroutine(WebReq(www));
        
    }

    // Update is called once per frame

    IEnumerator WebReq(WWW www){
        yield return www;

        while (www.isDone == false){
            yield return null;
        }

        AssetBundle bundle = www.assetBundle;

        if (www.error == null)
        {
            GameObject obj = (GameObject)bundle.LoadAsset("bultlerfinal");
            Instantiate(obj);
        }
        else{
            
            Debug.Log(www.error);
        }
    }

    
    
}
