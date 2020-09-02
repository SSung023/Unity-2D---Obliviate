using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{

    public string transferMapName; // 이동할 맵의 이름

    // 플레이어의 현재 맵의 위치에 전달하기 위해 transferMapName을 대입해야함
    // Player에게 정보를 전달하기 위한 변수
    private MovingObject thePlayer; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        // 하이어라키 뷰에 있는 모든 MovingObject 컴포넌트들을 참조 가능 && 다수의 객체
        thePlayer = FindObjectOfType <MovingObject>();
    }

    // boxCollider 접촉 시 자동으로 실행되는 내장 함수
    // transferPoint의 isTrigger를 체크해준다
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        // 만일 collisionBox와 Player가 충돌한다면
        if (collision.gameObject.name == "Player")
        {
            // MovingObject의 currentMapName에 값을 저장
            thePlayer.currentMapName = transferMapName;
            //Scene을 불러오는 코드
            SceneManager.LoadScene(transferMapName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
