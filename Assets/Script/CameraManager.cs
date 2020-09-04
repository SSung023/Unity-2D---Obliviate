using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    static public CameraManager instance;

    public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed; // 카메라가 얼마나 빠른 속도로 대상을 쫓을 것인지
    private Vector3 targetPosition; // 대상의 현재 위치 값

    public BoxCollider2D bound;

    //boxCollider 영역의 최소, 최대 xyz값을 지님
    private Vector3 minBound;
    private Vector3 maxBound;

    // camera의 기준이 중앙에 위치하기 때문에, camera에 halfWidth를 더하고, halfHeight를 빼주면 카메라 영역의 왼쪽 위 꼭지점이 나옴
    // 카메라의 반너비, 반 높이를 지닐 변수
    private float halfWidth;
    private float halfHeight;

    //카메라의 반높이 값을 구할 속성을 이용하기 위한 변수
    private Camera theCamera;

    private void Awake()
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
    
    // Start is called before the first frame update
    void Start()
    {
        theCamera = GetComponent<Camera>();
        
        //bound는 맵 크기만큼 설정한 영역을 말함
        //bound.bounds.min은 bound의 최소 영역을 뜻함
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        
        //orthographicSize = mainCamera의 size
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height; //반 너비를 구하는 공식

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

            //Mathf.Clamp(value, min, max)는 value가 min과 max 사이라면 value를 return
            //value가 경계 밖이라면 value랑 더 가까운 값을 return
            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);
            
            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
            
            
        }
        
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound; //새로운 bound가 들어오면 bound를 교체해준다
        minBound = bound.bounds.min; //바운드의 min과 max를 갱신
        maxBound = bound.bounds.max;
        
    }
}