using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// ���빥����Χ(���ã�
    /// </summary>
    class WalkToAttackTrigger_x : AbstractTrigger
    {
        override public void Awake()
        {
            base.Awake();
            //id = TriggerID.IntoAttack;
        }
        public override bool Satisfy(MyState0 myState)
        {
            if (myState.aim[0] != null && Vector3.Distance(myState.transform.position, myState.aim[0].transform.position) <= myState.character.attackDistance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
