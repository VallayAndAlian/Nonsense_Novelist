using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 各种类型墙壁
/// </summary>
public class OneWayMove : MonoBehaviour
{
    /// <summary>水平方向</summary>
    private float hori_way;
    public float value1 = 10;
    /// <summary>垂直方向</summary>
    private float ver_way;
    public float value2 = 10;
    /// <summary>旋转角度</summary>
    public float angle = 1;
    /// <summary>消失时间</summary>
    public float disappearTime = 2;
    /// <summary>出口位置</summary>
    public Transform exitPoint;
    /// <summary>出口速度系数</summary>
    public float k = 0.5f;
    public CampEnum camp;
    private bool addHP;
    private float addHPTimer;
    private float addHPTime;
    private float cdTime;
    private bool cdOK=true;

    /// <summary>判断</summary>
    public int or = 0;

    // Update is called once per frame
    void Update()
    {
        if (or == 0)//水平方向
        {
            hori_way = Mathf.PingPong(Time.time, value1);
            transform.position = new Vector3(hori_way, transform.position.y, transform.position.z);
        }
        else if (or == 1)//竖直方向
        {
            ver_way = Mathf.PingPong(Time.time, value2);
            transform.position = new Vector3(transform.position.x, ver_way, transform.position.z);
        }
        else if (or == 2)//定点旋转
        {
            transform.RotateAround(this.transform.position, Vector3.forward, angle);

        }
        else if (addHP)//治疗机关，每秒恢复血量
        {
            cdOK = false;

            addHPTimer += Time.deltaTime;
            addHPTime += Time.deltaTime;
            if (addHPTime <= 10)//10秒内每秒恢复血量
            {
                if (addHPTimer > 1)
                {
                    AddCampHP();
                    addHPTimer = 0;
                }
            }
            else
            {
                addHP = false;
            }
        }
        else if (!cdOK)//治疗机关，冷却时间
        {
            cdTime += Time.deltaTime;
            if (cdTime > 120)
            {
                cdOK = true;
                cdTime = 0;
            }
        }
    }
    /// <summary>
    /// 治疗机关，每秒恢复血量
    /// </summary>
    private void AddCampHP()
    {
        if (camp == CampEnum.left)
        {
            AbstractCharacter[] left = CharacterManager.charas_left.ToArray();
            for (int i = 0; i < left.Length; i++)
            {
                left[i].CreateFloatWord(0.05f * left[i].hp, FloatWordColor.heal, false);
                left[i].hp += 0.05f * left[i].hp;
            }

        }
        else if (camp == CampEnum.right)
        {
            AbstractCharacter[] right = CharacterManager.charas_right.ToArray();
            for (int i = 0; i < right.Length; i++)
            {
                right[i].CreateFloatWord(0.05f * right[i].hp, FloatWordColor.heal, false);
                right[i].hp += 0.05f * right[i].hp;
            }
        }
    }

    /// <summary>
    /// 墙壁消失后再出现
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "bullet")
        {
            if (or == 3)//消失后再出现
            {
                this.gameObject.SetActive(false);
                Invoke("EnableBack", disappearTime);
            }
            if (or == 5&&cdOK)//治疗机关
            {
                addHP = true;
            }
        }
    }
    public void EnableBack()
    {
        this.gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (or == 4)//单向传送门（出口无碰撞）
        {
            if (collision.transform.tag == "bullet")
            {
                Vector3 a = collision.GetComponent<Rigidbody2D>().velocity * k;

                while (a.magnitude <= 1)//防止速度过慢
                {
                    a = collision.GetComponent<Rigidbody2D>().velocity * 2;
                }
                collision.transform.position = exitPoint.position + a;
            }
        }
    }
}
