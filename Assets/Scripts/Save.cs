using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���л�,���л���ָ�洢�ͻ�ȡ�����ļ����ڴ�������ط��еĶ���
[System.Serializable]
public class Save
{
    public int id;//���µ���ţ���0��ÿ��+1
    public int rand;//���µĵ÷�
    public string title;//���µı���
    public List<string> content;//���µ�����
    public string reply;//ϵͳ����
    public bool hasRead;

 //   public List<int> livingTargetPosition = new List<int>();
	//public List<int> livingMonsterTypes = new List<int>();

	public int shootNum = 0;
	public int score = 0;
}

