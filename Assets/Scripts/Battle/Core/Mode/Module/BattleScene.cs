

using UnityEngine;

public class BattleScene : BattleModule
{

    
    public override void Init() 
    {
        FindAllWall();
    }

    public void FindAllWall()//临时的，先把场上放的墙壁读进来
    {        Debug.Log("FindAllWall");
        var A= GameObject.Find("WallCol").GetComponentsInChildren<Collider2D>();
        foreach(var aSS in A)
        {
            Debug.Log(aSS.gameObject.name+"!!!");
            Battle.ObjectFactory.CreateWall<NormarWall>(aSS);
        }
    }
    public override void Update(float deltaSec)
    {
       
    }
}