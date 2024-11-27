using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //탄띠, 모자, 체력, 탄약

    protected bool message;
    protected bool interaction;


    //주위에 플레이어가 있을 떄 상호작용되게
    //오버랩 서클을 큐브로
    //상호작용 키에 관한 메시지와 실제 상호작용의 범위가 다르다.

    //상호 작용.
    public virtual void Interaction() { }


}
