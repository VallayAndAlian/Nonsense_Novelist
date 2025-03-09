using System.Collections.Generic;

public class GamePhase_Combat : GamePhase
{
    protected float mMonsterDelayTime=0;
    protected List<BattleCamp> mFightCamp=new List<BattleCamp>();
    protected bool IsSomeCampDead => CheckCampDead();      // 是否有某个阵营全部死亡
   
    public GamePhase_Combat(List<BattleCamp> fightCamp,float monsterDelayTime)
    {
        mFightCamp=fightCamp;
        mMonsterDelayTime=monsterDelayTime;
    }

    public override void Start()
    {
        Enter();
    }

    public override void Enter()
    {
        UnityEngine.Debug.Log("进入战斗回合" );
        foreach(var camp in mFightCamp)
        {
             // 激活角色战斗状态
            foreach(var chara in Battle.CampManager.GetCampMember(camp))
            {
                
            }
        }
        
        //生成危机
        


        // 切换战斗UI
        

    }
    


   public override void Update(float deltaTime)
    {        
        base.Update(deltaTime);
        UnityEngine.Debug.Log("战斗回合update" );
        if(DetectPhaseEnd())
        {
            Exit();
        }
    }

    public override bool DetectPhaseEnd()
    {
        //检测是否有哪个阵营全部死亡
         return IsSomeCampDead;

    }

    public override void Exit()
    {
        // 关闭角色战斗状态

        //清理场上的怪物

        //复活所有我方角色

        //掉血

        // 切换战斗UI
        

    }

#region Helper Methods
    private bool CheckCampDead()
    {
       return false;
    }

    #endregion

}
