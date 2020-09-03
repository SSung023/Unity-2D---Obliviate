using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferMap : MonoBehaviour
{

    public string transferMapName;
    public Transform target; // 동일한 Scene 내에서 다른 Map으로 이동하기 위해 필요한 변수

    private MovingObject thePlayer;
    private CameraManager theCamera; // 맵 전환 시, 카메라 전환을 위한 변수
    
    // Start is called before the first frame update
    void Start()
    {
        //카메라 전환을 위한 변수
        theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<MovingObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName; //player의 위치가 어디인지 저장
            
            // 카메라의 위치와 player의 위치를 target에 설정해뒀던 위치로 이동
            theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
            thePlayer.transform.position = target.transform.position;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
