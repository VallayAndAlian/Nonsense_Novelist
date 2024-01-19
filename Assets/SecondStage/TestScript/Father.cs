using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father : MonoBehaviour
{
    public AbstractWord0 abs;

    public virtual void Awake()
    {
        //abs = gameObject.AddComponent<ShenHuanFeiYan>();
        print("111");

    }
    public virtual void Update()
    {
        //print("111");
    }
    public virtual void AAA()
    {
        print("111");
    }
}
