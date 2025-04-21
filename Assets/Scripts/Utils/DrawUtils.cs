using UnityEngine;
using UnityEngine.UIElements;
public class DrawUtils
{
    /// <summary>
    /// ������ת�����εĵ���ͼ��
    /// </summary>
    /// <param name="center">���������ĵ㣨�������꣩</param>
    /// <param name="size">�����α߳�</param>
    /// <param name="angleDegrees">��ת�Ƕȣ�������</param>
    /// <param name="color">������ɫ</param>
    /// <param name="duration">��ʾ����ʱ�䣨�룩</param>
    public static void DrawRotatedSquare(
        Vector3 center,
        float size,
        float angleDegrees,
        Color color,
        float duration = 0)
    {
        float halfSize = size / 2f;
        Vector3[] localPoints = new Vector3[4]
        {
            new Vector3( halfSize,  halfSize, 0), 
            new Vector3(-halfSize,  halfSize, 0), 
            new Vector3(-halfSize, -halfSize, 0), 
            new Vector3( halfSize, -halfSize, 0)  
        };
        float angleRad = angleDegrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angleRad);
        float sin = Mathf.Sin(angleRad);
        Vector3[] worldPoints = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            Vector3 original = localPoints[i];
            float rotatedX = original.x * cos - original.y * sin;
            float rotatedY = original.x * sin + original.y * cos;
            worldPoints[i] = center + new Vector3(rotatedX, rotatedY, 0);
        }
        for (int i = 0; i < worldPoints.Length; i++)
        {
            int nextIndex = (i + 1) % worldPoints.Length;
            Debug.DrawLine(worldPoints[i], worldPoints[nextIndex], color);
        }
    }
    /// <summary>
    /// ���ƶ���εĵ���ͼ��
    /// </summary>
    /// <param name="vertexs">����</param>
    /// <param name="color">������ɫ</param>
    public static void DrawPolygon(
        Vector3[] vertexs,
        Color color,
        float duration = 0)
    {
        for (int i = 0; i < vertexs.Length; i++)
        {
            int nextIndex = (i + 1) % vertexs.Length;
            Debug.DrawLine(vertexs[i], vertexs[nextIndex], color);
        }
    }
}