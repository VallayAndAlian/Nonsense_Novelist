using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff������
/// </summary>
public class YiLuan : AbstractBuff
{
    static public string s_description = "<sprite name=\"san\">-20%���ڼ��и��ʱ�����";
    static public string s_wordName = "����";
    override protected void Awake()
    {


        buffName = "����";
        description = "<sprite name=\"san\">-20%���ڼ��и��ʱ�����";
        book = BookNameEnum.allBooks;
        isBad = true;
        isAll = false;
        upup = 2;

        base.Awake();

        chara.sanMul -= 0.3f;
        //��������״̬ʱÿ3s����һ�μ�⣬20%��������3s�����ԡ�����Ϊ2�����ң������Ը���Ϊ40%
        if (coroutine != null)
        {
            StopCoroutine(coroutine); coroutine = null;
        }
            
        coroutine = StartCoroutine(Detact());

    }
    Coroutine coroutine;
    WaitForSeconds waitTwoS = new WaitForSeconds(3);
    IEnumerator Detact()
    {
        while (true)
        {
            int _random=0;
            var _count = chara.gameObject.GetComponents<YiLuan>();
            if (_count.Length > 1)//���һ����Ч
            {
                if (_count[0] == this)
                {
                    _random = Random.Range(0, 100);
                    if (_random < 40)
                    {
                       var _b=chara.gameObject.AddComponent<QingMi>();
                        _b.maxTime = 3;
                    }
                }
            }
            else //�������ֻ��һ��
            {
                _random = Random.Range(0, 100);
                if (_random < 20)
                {
                    var _b = chara.gameObject.AddComponent<QingMi>();
                    _b.maxTime = 3;
                }
            }
            yield return waitTwoS;
        }
    }

    public override void Update()
    {
        if (CharacterManager.instance.pause) return;
        maxTime -= Time.deltaTime;
        if (maxTime < 0)
        {
            Destroy(this);
            if (coroutine != null)
            {
                StopCoroutine(coroutine); coroutine = null;
            }

        }
    }

    public override void OnDestroy()
    {
        chara.sanMul += 0.3f;
        if (coroutine != null)
        {
            StopCoroutine(coroutine); coroutine = null;
        }
         base.OnDestroy();
    }
}
