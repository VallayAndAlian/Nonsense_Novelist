using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
/// <summary>
/// ֱ��AddComponent��ʽ
/// </summary>
public class AbstractBuff : MonoBehaviour
{
    public string buffName;
    public BookNameEnum book=BookNameEnum.allBooks;
    /// <summary>�ɵ��Ӵ�����Ĭ�����ޣ�</summary>
    public int upup=999;
    protected AbstractCharacter chara;
    public float maxTime=0.5f;
    public string description;
    public bool isBad = false;
    public bool isAll = false;
       static public string s_description = "";
    static public string s_wordName = "";
    protected virtual void Awake()
    {
        chara = GetComponent<AbstractCharacter>();
        int num = 0;
        AbstractBuff[] buffs=GetComponents<AbstractBuff>();
        for (int i = buffs.Length-1; i > -1; i--)
        {//����,�����buff�ȱ�ɾ��
            if (buffs[i].buffName == buffName)
            {
                num++;
                if (num > upup)
                {
                    Destroy(buffs[i]);
                }
            }
        }
    }

    virtual public void Update()
    {
        if (CharacterManager.instance.pause) return;
        maxTime -= Time.deltaTime;
        if(maxTime<0)
        {
            Destroy(this);
        }
    }
    virtual public void OnDestroy()
    {
        chara.CreateFloatWord("<s>" + this.buffName + "</s>", FloatWordColor.removeWord, false);
    }
}
