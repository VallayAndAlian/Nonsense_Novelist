using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// ���󴥷���
    /// </summary>
    public abstract class AbstractTrigger : MonoBehaviour
    {
      public TriggerID id;

     virtual public void Awake()
        {

        }
        /// <summary>
        /// �Ƿ���������
        /// </summary>
      public abstract bool Satisfy(MyState0 myState);
    }

}
