using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// ���ѣ��״̬
    /// </summary>
    class OutDizzyTrigger : AbstractTrigger
    {
        override public void Awake()
        {
            base.Awake();
            id = TriggerID.OutDizzy;
        }
        public override bool Satisfy(MyState0 myState)
        {
            return myState.character.dizzyTime<=0;
        }
    }
}
