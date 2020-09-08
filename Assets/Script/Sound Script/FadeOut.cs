using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    BgmManager bgm;
    
    
    // Start is called before the first frame update
    void Start()
    {
        bgm = FindObjectOfType<BgmManager>();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(test());
    }

    IEnumerator test()
    {
        bgm.FadeOutMusic();
        
        yield return  new WaitForSeconds(3f);
        
        //bgm.FadeInMusic();
    }
}
