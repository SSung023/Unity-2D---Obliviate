using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{

    public string transferMapName; // 이동할 맵의 이름

    private MovingObject thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // boxCollider 접촉 시 자동으로 실행되는 내장 함수
    // transferPoint의 isTrigger를 체크해준다
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        // 만일 collisionBox와 Player가 충돌한다면
        if (collision.gameObject.name == "Player")
        {
            //Scene을 불러오는 코드
            SceneManager.LoadScene(transferMapName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
