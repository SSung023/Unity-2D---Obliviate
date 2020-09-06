using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{

    private BoxCollider2D bound;

    private CameraManager theCamera; //SetBound 함수를 이용하기 위해 선언
    
    // Start is called before the first frame update
    void Start()
    {
        //bound객체의 boxCollider2D를 가져오는 코드
        bound = GetComponent<BoxCollider2D>();
        theCamera = FindObjectOfType<CameraManager>();
        theCamera.SetBound(bound);
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}

