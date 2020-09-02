using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    static public CameraManager instance;

    public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed; // 카메라가 얼마나 빠른 속도로 대상을 쫓을 것인지
    private Vector3 targetPosition; // 대상의 현재 위치 값

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            // 다른 Scene으로 넘어갈 때 해당 Object를 파괴시키지 말라는 함수
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);  
        }

    }

    // Update is called once per frame
    void Update()
    {
        // 카메라가 대상을 쫓을 때
        if (target.gameObject != null)
        {
            // 대상의 현재 위치 값을 변수 targetPosition 대입
            // this == script가 적용될 객체 (카메라)
            // this로 하는 이유: 카메라와 depth와 캐릭터의 depth가 일치하면 카메라가 캐릭터를 조영하지 못한다. (2D이기 때문)
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
            
            // lerp는 A값과 B값 사이의 선형 보간으로 중간 값을 return (vector a에서 vector b까지 t의 속도로 움직이게 하는 것?)
            // moveSpeed * Time.deltaTime == 1초에 moveSpeed만큼 이동시키겠다는 의미
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        
    }
}