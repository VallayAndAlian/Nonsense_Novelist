using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// ¸´»î
    /// </summary>
    class ReLifeTrigger : AbstractTrigger
    {
        override public void Awake()
        {
            base.Awake();
            id = TriggerID.ReLife;
        }
        public override bool Satisfy(MyState0 myState)
        {
            if (myState.character.hp > 0)
                return true;
            else
                return false;
        }
    }
}
