using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    /// <summary>
    /// ����״̬��Ŀǰֻ���ڽ�ɫ��
    /// </summary>
    public abstract class AbstractState : MonoBehaviour
    {
        [HideInInspector] public StateID id;
        /// <summary>���д�������</summary>
        public List<AbstractTrigger> triggers=new List<AbstractTrigger>();
        /// <summary>����������Ӧ��ϵ</summary>
        public Dictionary<TriggerID,StateID> map=new Dictionary<TriggerID, StateID>();

        virtual public void Awake()
        {
            triggers.Add(gameObject.AddComponent<NoHealthTrigger>());
            map.Add(TriggerID.NoHealth, StateID.dead);
            triggers.Add(gameObject.AddComponent<BeDizzyTrigger>());
            map.Add(TriggerID.BeDizzy, StateID.dizzy);
        }

        /// <summary>
        /// �����״̬ʱ
        /// </summary>
        public abstract void EnterState(MyState0 myState);
        /// <summary>
        /// �˳���״̬ʱ
        /// </summary>
        public abstract void Exit(MyState0 myState);
        /// <summary>
        /// �ڸ�״̬�ڼ��������
        /// </summary>
        public abstract void Action(MyState0 myState);
        /// <summary>
        /// ����Ƿ���������
        /// </summary>
        public virtual void CheckTrigger(MyState0 myState)
        {
            for(int i=0;i<triggers.Count;i++)
            {
                if(triggers[i].Satisfy(myState))
                {
                    
                    print("map[triggers[i].id="+(map[triggers[i].id]).ToString());
                    myState.ChangeActiveState(map[triggers[i].id]);
                }
            }
        }

    }
}
