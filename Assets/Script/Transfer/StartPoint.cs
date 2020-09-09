using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{

    public string startPointLocation; //맵이 이동되면 Player가 시작될 위치
    
    //진입점이 두 군데일 때, 어디로 갈지 컨트롤하는 변수
    public bool isOneEntry;
    public Transform location;
    
    private PlayerManager thePlayer;

    private CameraManager theCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        if (startPointLocation == thePlayer.currentMapName)
        {
            if (isOneEntry)
            {
                theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
                thePlayer.transform.position = this.transform.position;
            }
            else if(!isOneEntry)
            {
                theCamera.transform.position = new Vector3(location.transform.position.x, location.transform.position.y, theCamera.transform.position.z);
                thePlayer.transform.position = location.transform.position;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
