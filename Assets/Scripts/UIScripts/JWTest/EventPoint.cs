using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPoint : MonoBehaviour
{
    public static bool[] isEvent= {true,true,true,true,true }; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //事件气泡位置若有纸条，则不生成气泡
        if (collision.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
        {
            if (this.gameObject.name == "point0") isEvent[0] = false;
            else if (this.gameObject.name == "point1") isEvent[1] = false;
            else if (this.gameObject.name == "point2") isEvent[2] = false;
            else if (this.gameObject.name == "point3") isEvent[3] = false;
            else if (this.gameObject.name == "point4") isEvent[4] = false;
            else if (this.gameObject.name == "point5") isEvent[5] = false;

        }
    }
}
