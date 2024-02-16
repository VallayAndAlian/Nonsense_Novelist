using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// 生命值为0
    /// </summary>
    class NoHealthTrigger : AbstractTrigger
    {
        override public void Awake()
        {
            base.Awake();
            id = TriggerID.NoHealth;
        }

        public delegate void Live();
        public event Live OnLive;

        public override bool Satisfy(MyState0 myState)
        {
           
            if (myState.character.hp <= 0)
            {
                
                if (myState.character.reLifes > 0)//复活
                {
                    ReLifeEffect(myState);
                  

                    //删除relife效果
                    if (myState.character.GetComponent<ReLife>() != null)
                    {
                        Destroy(myState.character.GetComponent<ReLife>());
                        var _buffs = myState.character.GetComponents<AbstractBuff>();
                        foreach (var _buff in _buffs)
                        {
                            if (_buff.isBad) Destroy(_buff);
                        }
                    }
                    else
                        myState.character.reLifes--;



                    //if (OnLive != null) OnLive();
                    return false;
                }
                else//死亡
                {
                    //如果没有复活buff，但有日轮挂坠，复活
                    if (GetComponent<RiLunGuaZhui>() != null)
                    {
                        ReLifeEffect(myState);
                        Destroy(GetComponent<RiLunGuaZhui>());
                    }
                      return true;
                }
                  
            }
            else//hp>0
                return false;
        }


        /// <summary>
        ///  部分词条有复活时强化的效果。写在这。
        /// </summary>
        void ReLifeEffect(MyState0 myState)
        {
            myState.character.hp = myState.character.maxHp;
            //荷鲁斯之眼
            if (myState.character.GetComponent<herusizhiyan>()!=null)
            {
                myState.character.atk += 1;
            }
        }
    }
}
