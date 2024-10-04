using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// ¿ªÊ¼¹¥»÷
    /// </summary>
    class IdleToAttackTrigger : AbstractTrigger
    {
        override public void Awake()
        {
            base.Awake();
            id = TriggerID.IdleToAttack;
        }
        public override bool Satisfy(MyState0 myState)
        {
            print(myState.character.name + myState.character.Camp);
            if (myState.character.hp <= 0) return false;
            if (CharacterManager.instance.pause)return false;
            switch (GameMgr.instance.nowStageType )
            {
                case StageType.fight_Pve_l:
                    {
                        if (myState.character.Camp == CampEnum.right) return false;
                    }
                    break;
                case StageType.fight_Pve_r:
                    {
                        if (myState.character.Camp == CampEnum.left) return false;
                    }
                    break;
                case StageType.fight_Pve_boss:
                    {
                       
                    }
                    break;
                case StageType.fight_Pvp:
                    {
                        
                    }
                    break;
                default:
                    {
                        return false;
                    }
            }
            if (myState.aim != null)
            {
                return true;
            }
        
            return false;
        }
    }
}
