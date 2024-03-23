using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ÇúÏßÔ¤²âÆ÷
/// </summary>
public class Track : MonoBehaviour
{
    [SerializeField]private int dotNum = 20;
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
    /// ×¼±¸¹ì¼£µã
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
            dot.localScale = 0.1f*Vector3.one * scale;
            dot.position = Vector3.zero;
            if (scale > dotMinScale)
                scale -= scaleFactor;
            dotList[i] = dot;
        }
    }
    public void UpdateDots(Vector3 birdPos,Vector2 pushSpeed)
    {
        timeStamp = dotSpacing;
        for(int i = 0; i < dotNum; ++i)
        {
            pos.x = birdPos.x + pushSpeed.x * timeStamp;
            pos.y=(birdPos.y+pushSpeed.y*timeStamp)-0.5f* Physics2D.gravity.magnitude * timeStamp*timeStamp;
            dotList[i].position = pos;
            timeStamp += dotSpacing;
        }
    }
    /// <summary>
    /// ÏÔÊ¾Ô¤²â¹ì¼£
    /// </summary>
    public void Show()
    {
        dotsParent.SetActive(true);
    }
    /// <summary>
    /// Òþ²ØÔ¤²â¹ì¼£
    /// </summary>
    public void Hide()
    {
        dotsParent.SetActive(false);
    }

}
