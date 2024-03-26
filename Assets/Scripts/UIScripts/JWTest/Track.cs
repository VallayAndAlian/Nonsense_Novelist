using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����Ԥ����
/// </summary>
public class Track : MonoBehaviour
{
    [SerializeField]private int dotNum = 40;
    public GameObject dotsParent;
    public GameObject dotPrefab;
    public float dotSpacing = 0.01f;
    [Range(0.01f, 0.3f)] public float dotMinScale = 0.1f;
    [Range(0.3f, 1f)] public float dotMaxScale = 1f;

    private Transform[] dotList;
    private Vector2 pos;
    private float timeStamp;

    private void Start()
    {
        Hide();
        PrepareDots();
    }
    /// <summary>
    /// ׼���켣��
    /// </summary>
    private void PrepareDots()
    {
        dotList = new Transform[dotNum];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;
        float scale = dotMaxScale;
        float scaleFactor = scale / dotNum;

        for(int i = 0; i < dotNum; ++i)
        {
            var dot = Instantiate(dotPrefab).transform;
            dot.parent = dotsParent.transform;
            dot.localScale = 0.2f*Vector3.one * scale;
            dot.position = Vector3.zero;
            if (scale > dotMinScale)
                scale -= scaleFactor;
            dotList[i] = dot;
        }
    }
    bool istrue=false;
    /// <summary>
    /// ���µ��λ��
    /// </summary>
    /// <param name="startPos">��ʼ��</param>
    /// <param name="pushSpeed">���䷽��</param>
    /// <param name="secondPos">�Ӵ���</param>
    /// <param name="reflect">���䷽��</param>
    /*public void UpDateDots0(Vector3 startPos, Vector2 shoot, Vector3 secondPos, Vector2 reflect)
    {
        timeStamp = dotSpacing;
        
        for (int i = 0; i < dotNum; ++i)
        {
            if ((dotList[i].position.x<0&& dotList[i].position.x>Shoot.pointt.x&& dotList[i].position.y < Shoot.pointt.y+ shoot.y * timeStamp) ||(dotList[i].position.x > 0 && dotList[i].position.x < Shoot.pointt.x && dotList[i].position.y < Shoot.pointt.y+ reflect.y * timeStamp))//����
            {
                pos.x = startPos.x + shoot.x * timeStamp;
                pos.y = (startPos.y + shoot.y * timeStamp) - 0.5f * Physics2D.gravity.magnitude * timeStamp * timeStamp;
                dotList[i].position = pos;
                timeStamp += dotSpacing;
            }
            else//����
            {
                if (istrue==false)
                {
                    timeStamp = dotSpacing;
                    istrue = true;
                }
                pos.x = secondPos.x + reflect.x * timeStamp;
                pos.y = (secondPos.y + reflect.y * timeStamp) - 0.5f * Physics2D.gravity.magnitude * timeStamp * timeStamp;
                dotList[i].position = pos;
                timeStamp += dotSpacing;
            }

        }
    }*/
    public void UpDateDots0(Vector3 startPos, Vector2 shoot, Vector3 secondPos, Vector2 reflect)
    {
        timeStamp = dotSpacing;
        bool isReflecting = false;
        Vector2 lastPos = startPos;

        for (int i = 0; i < dotNum; ++i)
        {
            if (!isReflecting)
            {
                pos.x = startPos.x + shoot.x * timeStamp;
                pos.y = (startPos.y + shoot.y * timeStamp) - 0.5f * Physics2D.gravity.magnitude * timeStamp * timeStamp;

                // ʹ�õ���ƶ���������ǹ̶��������жϷ���
                if ((shoot.x > 0 && pos.x > secondPos.x) || (shoot.x < 0 && pos.x < secondPos.x))
                {
                    isReflecting = true;
                    startPos = pos; // ���·������
                    timeStamp = 0; // ����ʱ���
                }
            }
            else
            {
                pos.x = startPos.x + reflect.x * timeStamp;
                pos.y = (startPos.y + reflect.y * timeStamp) - 0.5f * Physics2D.gravity.magnitude * timeStamp * timeStamp;
            }

            dotList[i].position = pos;
            timeStamp += dotSpacing;
            lastPos = pos; // �������һ��λ�ã�������һ��ѭ��
        }
    }
    public static float Dott(Vector3 lhs, Vector3 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
    }
    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="inDirection">���䷽��</param>
    /// <param name="inNormal">����</param>
    /// <returns></returns>
    public static Vector3 Reflectt(Vector3 inDirection, Vector3 inNormal)
    {
        float num = -2f * Dott(inNormal, inDirection);
        return new Vector3(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y, num * inNormal.z + inDirection.z);
    }
    /// <summary>
    /// ��ʾԤ��켣
    /// </summary>
    public void Show()
    {
        dotsParent.SetActive(true);
    }
    /// <summary>
    /// ����Ԥ��켣
    /// </summary>
    public void Hide()
    {
        dotsParent.SetActive(false);
    }

}
