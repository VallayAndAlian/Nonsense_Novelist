using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/** ˵��
 * ����Unity��˵�̳�Mono����Ҳ���õ�����
 * �̳�Mono�ĵ�����ÿ�ι��ؽű�ʱ,����Awake�и�ֵinstance
 * ע��:
 * �����߱��뱣֤,������ֻ��һ������Ұ����˴˽ű�
 * �����ж�����󶼹��ڴ˽ű�,���е�thisֻ��ָ�������ص��Ǹ�,����˷�
 */


/// <summary>
/// �̳�MonoBehaviour�ĵ�������
/// �̳��˴����������Ϊ�̳���MonoBehaviour����,�Ҿ��е�������
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMonoBase<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance;

    //�������͵��麯����֤���಻�ᶥ��������߼�
    protected virtual void Awake()
    {
        instance = this as T;
    }

    private T GetInstance()
    {
        return instance;
    }
}