using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责词条实体与角色/墙壁的碰撞
/// </summary>
public class WordCollisionShoot : MonoBehaviour
{
    /// <summary>词条技能 </summary>
    public AbstractWord0 absWord;
    /// <summary>计时器 </summary>
    public float timer;


    Collider2D collider;
    public virtual void Awake()
    {
        collider = this.gameObject.GetComponent<Collider2D>();
        if(collider!=null)
            collider.sharedMaterial = Resources.Load<PhysicsMaterial2D>("Other/word");
    }

    /// <summary>
    /// 词条实体碰撞到角色，将词条施加到角色身上
    /// </summary>
    /// <param name="collision"></param>
    public virtual  void OnTriggerEnter2D(Collider2D collision)
    {

        if (CharacterManager.instance.pause)
            return;



        if (collision.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            AbstractCharacter character = collision.gameObject.GetComponent<AbstractCharacter>();

           
                absWord = this.GetComponent<AbstractWord0>();
            if (absWord == null) return;



            character.CreateFloatWord(absWord.wordName , FloatWordColor.getWord, false);
            //判断该词条是形容词/动词/名词
            //先把absWord脚本挂在角色身上，然后调用角色身上的useAdj
            if (absWord.wordKind == WordKindEnum.verb)
            {
                AbstractVerbs b = this.GetComponent<AbstractVerbs>();
                character.AddVerb(collision.gameObject.AddComponent(b.GetType()) as AbstractVerbs);
                Destroy(this.gameObject);

            }
            else if (absWord.wordKind == WordKindEnum.adj)
            {
                AbstractAdjectives adj= collision.gameObject.AddComponent(absWord.GetType())as AbstractAdjectives;
                adj.UseAdj(collision.gameObject.GetComponent<AbstractCharacter>());
                print(this.gameObject.name);
                Destroy(this.gameObject);
            }
            else if (absWord.wordKind == WordKindEnum.noun)
            {
                AbstractItems noun= collision.gameObject.AddComponent(absWord.GetType())as AbstractItems;
                noun.UseItem(collision.gameObject.GetComponent<AbstractCharacter>());
           
                Destroy(this.gameObject);
            }

           
     
        }
    }


    /// <summary>
    /// 计时器(时间结束返回true
    /// </summary>
    /// <returns></returns>
    public virtual bool VanishTime(float time)
    {
        if (CharacterManager.instance.pause)
            return false;

        timer += Time.deltaTime;
        if (timer >= time)
        {
            timer = 0;
            return true;
        }
        return false;
    }
   
}
