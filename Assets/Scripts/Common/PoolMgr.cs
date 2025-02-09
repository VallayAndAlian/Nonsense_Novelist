using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PoolData
{
   
    public GameObject fatherObj;
    //���������
    public List<GameObject> poolList;

    public PoolData(GameObject obj, GameObject poolObj)
    {
        
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;
        poolList = new List<GameObject>() { };
        PushObj(obj);
    }

 
    public void PushObj(GameObject obj)
    {
        //ʧ�� ��������
        obj.SetActive(false);
        //������
        poolList.Add(obj);
        //���ø�����
        obj.transform.SetParent(fatherObj.transform, false);
    }


    public GameObject GetObj()
    {
        GameObject obj = null;
        if (poolList.Count == 0) Debug.Log("poolList==null");
        if (poolList[0] == null) Debug.Log("poolList[0]==null");
        //ȡ����һ��
        obj = poolList[0];
        poolList.RemoveAt(0);
        //���� ������ʾ
        if (obj == null) Debug.Log("obj==null");
        obj.SetActive(true);
        //�Ͽ��˸��ӹ�ϵ
        obj.transform.SetParent(null, false);

        return obj;
    }
}


public class PoolMgr : BaseManager<PoolMgr>
{       
    private PrefabSO prefabData;
    //��������� ���¹�
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

    private GameObject poolObj;

 
    public void GetSOObj(prefabSOType type,int id,UnityAction<GameObject> callBack)
    {
        if(prefabData==null)
            prefabData = Resources.Load<PrefabSO>("SO/PrefabSO");    

        GameObject obj=null;
        switch(type)
        {
            case prefabSOType.Other:
            {
                obj=prefabData.OtherpPrefabs[id].mPrefab;
                }
            break;
            case prefabSOType.UI:
            {
                obj=prefabData.UIPrefabs[id].mPrefab;
                }
            break;
            case prefabSOType.BattleObj:
            {
                obj=prefabData.BattleObjPrefabs[id].mPrefab;
            }
            break;
        }

        if(obj==null)   
            return;

        GetObj(obj,callBack);

    }
    
    /// <summary>
    /// �����ö���
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public void GetObj(string name, UnityAction<GameObject> callBack)
    {
        //�г��� ���ҳ������ж���
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
        {
            callBack(poolDic[name].GetObj());
        }
        else
        {
            //ͨ���첽������Դ ����������ⲿ��
            GameObject o=ResMgr.GetInstance().Load<GameObject>(name);
                o.name = name;
                callBack(o);
            
        }
    }
     public GameObject GetObj(string name)
    {
        //�г��� ���ҳ������ж���
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
        {
            return poolDic[name].GetObj();
        }
        else
        {
            //ͨ���첽������Դ ����������ⲿ��
            GameObject o=ResMgr.GetInstance().Load<GameObject>(name);
                o.name = name;
            return o;

        }
    }
    public void GetObj(GameObject obj, UnityAction<GameObject> callBack)
    {
        //�г��� ���ҳ������ж���
        if ((poolDic.ContainsKey(obj.name) && poolDic[obj.name].poolList.Count > 0)&&(poolObj!=null))
        {
            callBack(poolDic[obj.name].GetObj());
        }
        else
        {
            //ͨ���첽������Դ ����������ⲿ��
            GameObject o = GameObject.Instantiate(obj);
            callBack(o);

        }
    }
    /// <summary>
    /// ����ʱ���õĶ�������
    /// </summary>
    public void PushObj(string name, GameObject obj)
    {
        if (obj == null) return;
        if (poolObj == null)
            poolObj = new GameObject("Pool");

        //�����г���
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].PushObj(obj);
        }
        //����û�г���
        else
        {
            poolDic.Add(name, new PoolData(obj, poolObj));
        }
    }


    /// <summary>
    /// ��ջ���صķ��� 
    /// ��Ҫ���� �����л�ʱ
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
public class ResMgr : BaseManager<ResMgr>
{
    //ͬ��������Դ
    public T Load<T>(string name) where T : Object
    {
        T res = Resources.Load<T>(name);
        //���������һ��GameObject���͵� �Ұ���ʵ������ �ٷ��س�ȥ �ⲿ ֱ��ʹ�ü���
        if (res is GameObject)
            return GameObject.Instantiate(res);
        else//TextAsset AudioClip
            return res;
    }


   
  
}
