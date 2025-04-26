using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//ÿ��������е�����
public class poolData
{
    //���������������ڳ����еĸ��ڵ�
    public GameObject fatherObj;
    //���������
    public List<GameObject> poolList ;

    /// <summary>
    /// ��ʼ��һ���������
    /// 
    /// ����һ������Դ������ͬ�Ļ������ڵ�
    /// ��Ϊ������Դ���ڵ�,������������ؽڵ���
    /// ����������Դ����˽ڵ�
    /// </summary>
    /// <param name="obj">�������ĸ��ڵ�</param>
    /// <param name="poolObj">����ظ��ڵ�</param>
    public poolData(GameObject obj, GameObject poolObj)
    {
        fatherObj = new GameObject(obj.name);
        //���û����Ϊ�������ĸ�����
        fatherObj.transform.parent = poolObj.transform;
        poolList = new List<GameObject>();
        //������ѹ�뻺����
        PushObj(obj);
    }

    /// <summary>
    /// �򻺳�����д洢
    /// </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        //�������
        poolList.Add(obj);
        //���ø��ڵ�Ϊ��������ڵ�
        obj.transform.parent = fatherObj.transform;
        //���ö���ʧ��
        obj.SetActive(false);
    }

    /// <summary>
    /// ȡ��һ������
    /// </summary>
    /// <returns></returns>
    public GameObject GetObj()
    {
        GameObject obj = null;
        if (poolList.Count > 0)
        {
            //ȡ��һ������
            obj = poolList[0];
            //���ö��󼤻�
            obj.SetActive(true);
            //�ӻ������ڵ���ȡ��
            //���ڵ��ÿ�,ʹ�ò��ڻ�����
            obj.transform.parent = null ;
            //��List��ȡ��
            poolList.RemoveAt(0);
        }
        return obj;
    }
}

/// <summary>
/// �����ģ��
/// ����ʱ���õĶ��󴢴��ڴ˴�����ʱ�ó�
/// �����ظ����ڴ��������������˷�
/// </summary>
public class BufferPoolManager : SingletonBase<BufferPoolManager>
{
    //<���������,ÿ�����͵Ĵ洢����>
    Dictionary<string, poolData> bufferPool=new Dictionary<string, poolData>();
    //����صĳ����еĸ�����
    private GameObject poolObj;


    /// <summary>
    /// ȡ���������Ķ���
    /// 2���޶�:�����Դ�첽����ʹ����Դ����Ҳ�������Կ���
    /// </summary>
    /// <param name="name">��������ơ����������ơ���Դ·��������ͬ</param>
    /// <param name="callback">�ص�����,������Ǽ�����ɵ���Դ</param>
    public void GetObj(string name,UnityAction<GameObject> callback)
    {
        GameObject obj = null;
        //�Ѿ�������һ���ͻ���������������
        if (bufferPool.ContainsKey(name) && bufferPool[name].poolList.Count > 0) 
        {
            //ȡ����������һ��,����ص��������ں��ڴ���
            callback(bufferPool[name].GetObj());
        }
        else
        {
            //obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            //�������ͻ����������ͬ,��ʹ��ʱ����ѹ���Ӧ��
            //obj.name = name;
            ResourceManager.GetInstance().LoadResourceAsync<GameObject>(name, (o) =>
            {
                o.name = name;
                callback(o);
            });
        }
        //return obj ;
    }

    /// <summary>
    /// ����ʱ��ͬ�Ķ�����뻺���
    /// ������еĻ���ؽڵ���Ӧ�ý����ڵ���벻ͬ�Ķ��󣬱���ֱ��
    /// </summary>
    /// <param name="name">��������ƻ��߻����������Լ�·����ͬ</param>
    /// <param name="obj">Ҫ����Ķ���</param>
    public void PushObject(string name, GameObject obj)
    {
        if (obj == null)
            return;

        //�ڳ�����������ؽڵ�
        if (poolObj == null)
            poolObj = new GameObject("PoolBuffer");

        //����Ϊ��Դ������Ϊ�����
        obj.transform.SetParent(poolObj.transform);

        //ʧ��,����
        obj.SetActive(false);
        //���ڴ˻���������
        if (bufferPool.ContainsKey(name))
        {
            bufferPool[name].PushObj(obj);
        }
        //�����ڵĻ�����������ѹ��
        else

        {
            //ѹ��Ļ�����,�����ڹ�������ض���poolObj��
            bufferPool.Add(name, new poolData(obj, poolObj)); 
        }
    }

    /// <summary>
    /// �����л�ʱʹ��
    /// �л�����ʱӦ�ý���һ���������������,��ֹ������
    /// </summary>
    public void ClearBufferr()
    {
        bufferPool.Clear();
        poolObj = null; 
    }

}
