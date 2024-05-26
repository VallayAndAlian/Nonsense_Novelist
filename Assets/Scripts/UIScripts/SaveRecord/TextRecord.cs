using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TextRecord", menuName = "GameTextRecord/newRecord")]
public class TextRecord : ScriptableObject
{
    public int id;//���µ���ţ���0��ÿ��+1
    public int rand;//���µĵ÷�
    public string title;//���µı���
    public List<string> content;//���µ�����
    public string reply;//ϵͳ����
    public bool hasRead;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
