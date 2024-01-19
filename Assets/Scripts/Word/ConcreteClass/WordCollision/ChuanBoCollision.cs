using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ײ���ƣ����� ����
/// </summary>
public class ChuanBoCollision : WordCollisionShoot
{

    static public string s_description = "���к󣬸��ƴ���Ч�������ڸ��ӵĽ�ɫ";
    static public string s_wordName = "���� ����";


    /// <summary>��ײ���� </summary>
    private int count = 0;
    Color color = Color.black;
    public override void Awake()
    {
        base.Awake();
        //��absWord��ֵ
        absWord = Shoot.abs;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CharacterManager.instance.pause)
            return;
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        ////��absWord��ֵ
        //absWord = Shoot.abs;
        if (CharacterManager.instance.pause)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Character"))
        {

            //��ȡ��ɫ�����ڽ�ɫ
            AbstractCharacter character = collision.gameObject.GetComponent<AbstractCharacter>();
            AbstractCharacter[] nearCharacter = CharacterManager.instance.GetNearBy_C(character.situation);

            character.CreateFloatWord(absWord.wordName, FloatWordColor.getWord, false);
            //�жϸô��������ݴ�/����/����
            //�Ȱ�absWord�ű����ڽ�ɫ���ϣ�Ȼ����ý�ɫ���ϵ�useAdj
            if (absWord.wordKind == WordKindEnum.verb)
            {
                AbstractVerbs b = this.GetComponent<AbstractVerbs>();

                character.AddVerb(collision.gameObject.AddComponent(b.GetType()) as AbstractVerbs);

                //����λ����
                foreach (var cha in nearCharacter)
                {
                    if (cha != null)
                    {
                        character.AddVerb(collision.gameObject.AddComponent(b.GetType()) as AbstractVerbs);
                    }
                }

                Destroy(this.gameObject);

            }
            else if (absWord.wordKind == WordKindEnum.adj)
            {
                AbstractAdjectives adj = collision.gameObject.AddComponent(absWord.GetType()) as AbstractAdjectives;
                adj.UseAdj(collision.gameObject.GetComponent<AbstractCharacter>());

                //����λ����
                foreach (var cha in nearCharacter)
                {
                    if (cha != null)
                    {
                        AbstractAdjectives _adj = cha.gameObject.AddComponent(absWord.GetType()) as AbstractAdjectives;
                        
                        _adj.UseAdj(cha);
                    }
                }

                Destroy(this.gameObject);
            }
            else if (absWord.wordKind == WordKindEnum.noun)
            {
                AbstractItems noun = collision.gameObject.AddComponent(absWord.GetType()) as AbstractItems;
                noun.UseItem(collision.gameObject.GetComponent<AbstractCharacter>());

                //����λ����
                foreach (var cha in nearCharacter)
                {
                    if (cha != null)
                    {
                        AbstractItems _noun = collision.gameObject.AddComponent(absWord.GetType()) as AbstractItems;
                        noun.UseItem(cha);
                    }
                }

                Destroy(this.gameObject);
            }
        }
    }
}
interface IChuanBo
{
    public void ChuanBo(bool value);
}
