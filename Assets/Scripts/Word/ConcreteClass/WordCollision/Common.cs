using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������ײ
/// </summary>
public class Common : WordCollisionShoot
{
    private void Awake()
    {
        //absWord = Shoot.abs;
    }


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (CharacterManager.instance.pause)
            return;
        //��absWord��ֵ
        //absWord = Shoot.abs;

        //������ϻ������������Ļ���
        if (this.GetComponentsInChildren<WordCollisionShoot>().Length>1)
            return;


        base.OnTriggerEnter2D(collision);
    }
}
