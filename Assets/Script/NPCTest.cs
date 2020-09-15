using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestMove
{
    public string name;
    public string direction;
}

public class NPCTest : MonoBehaviour
{
    [SerializeField]
    public TestMove[] moveCase;

    public string direction;
    public bool isCollied; //player와 collider가 충돌했는지의 여부
    private bool isCoolTimePassed;
    public float coolTime;
    
    private OrderManager theOrder;
    
    private HashSet<GameObject> hadCollied = new HashSet<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();

        isCollied = false;
        isCoolTimePassed = false;
        coolTime = 2f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // if (isCollied)
            // {
            //     theOrder.preLoadCharacter(); //모든 npc를 불러온다
            //     isCollied = false;
            //     StartCoroutine(moveNPC());
            // }
            
            theOrder.preLoadCharacter(); //모든 npc를 불러온다
            
            print("OnTriggerEnter2D " + Time.time + "\n");
            
            //여기서 npc가 반복해서 움직이는 버그가 생기는 것으로 추정, 한번만 실행되어야 하는데 여러번 실행됨
            for (int i = 0; i < moveCase.Length; i++)
            {
                theOrder.Move(moveCase[i].name, moveCase[i].direction);
            
            }

            Destroy(this.gameObject);
            //StartCoroutine(moveNPC());

            //theOrder.Move(moveCase[0].name, moveCase[0].direction);

            // theOrder.Turn("npc1", direction);
            // theOrder.setInvisible("npc1");
        }
    }

    IEnumerator moveNPC()
    {
        // for (int i = 0; i < moveCase.Length; i++)
        // {
        //     theOrder.Move(moveCase[i].name, moveCase[i].direction);
        //
        // }
        yield return new WaitForSeconds(2f);
        // isCollied = true;
        //
        // print("코루틴 끝남\n");
    }


}
