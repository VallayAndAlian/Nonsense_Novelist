using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 杂耍火球
/// </summary>
class FireBall_x : AbstractVerbs
{
    private GameObject bullet;

    public override void Awake()
    {
        base.Awake();
        skillID = 10;
        wordName = "杂耍火球";
        bookName = BookNameEnum.StudentOfWitch;

        skillMode = gameObject.AddComponent<DamageMode>();
        skillMode.attackRange = new SingleSelector();
        skillEffectsTime = 0.3f;
        needCD = 5;
        description = "花哨且伤害不俗的杂技把戏。";

        bullet = Resources.Load<GameObject>("FirstStageLoad/" + "bullet/Fireball_bullet");
    }

    private AbstractCharacter aimState;//目标的抽象角色类
    /// <summary>
    /// 造成150%攻击力的伤害
    /// </summary>
    /// <param name="useCharacter">施法者</param>
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
       /* if (aims != null)
        {
            skillMode.UseMode(useCharacter, useCharacter.atk  * (1 - aims[0].def / (aims[0].def + 20)), aims[0]);
            SpecialAbility(useCharacter);
        }*/
    }
    /// <summary>
    /// 晕眩0.3秒
    /// </summary>
    public override void BasicAbility(AbstractCharacter useCharacter)
    {
       /* DanDao danDao = bullet.GetComponent<DanDao>();
            danDao.aim = aims[0].gameObject;
            danDao.bulletSpeed = 0.5f;
            danDao.birthTransform = this.transform;
            ARPGDemo.Common.GameObjectPool.instance.CreateObject(bullet.gameObject.name, bullet.gameObject, this.transform.position, aims[0].transform.rotation);

        aims[0].dizzyTime = skillEffectsTime;
        aims[0].AddBuff(5);*/
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null || aimState == null)
            return null;

        return character.wordName + "动了动手指，几个火球伴随着低声吟唱的咒语从之间跃出，以花哨的动作旋转着并朝" + aimState.wordName + "冲了过去。";

    }
}
