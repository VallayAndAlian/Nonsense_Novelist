using UnityEngine.UI;
using UnityEngine;
using System;

/// <summary>
/// 发射词弹
/// </summary>
public class Shoot : MonoBehaviour
{
    /// <summary>发射位置</summary>
    public Transform gang;
    /// <summary>词条</summary>
    public GameObject bullet;
    /// <summary>词条</summary>
    private GameObject bulletInstance;
    /// <summary>发射前词条的父物体</summary>
    public Transform bulletRoot;
    /// <summary>发射后词条的父物体</summary>
    public Transform afterShootTF;
    /// <summary>当前的力</summary>
    [SerializeField]
    private float crtForce = 0;
    /// <summary>最小力</summary>
    public float minForce = 0;
    /// <summary>最大力</summary>
    public float maxForce = 200;
    /// <summary>蓄力速度</summary>

    public float forceSpeed = 80;
    /// <summary>蓄力比值</summary>
    public float forceAmount=1;
    /// <summary>有无发射</summary>
    private bool fired = false;
    /// <summary>蓄力Slider</summary>
    public Slider aimSlider;
    /// <summary>存储WordCollisionShoot的词条属性</summary>
    public static AbstractWord0 abs;
    /// <summary>手动，词条信息板 </summary>
    public WordInformation information;

    private void Update()
    {
        if (CreateOneCharacter. isTwoSides && CreateOneCharacter.isAllCharaUp)
        {
            aimSlider.value = 0; // 重置slider的值

            if (crtForce >= maxForce && !fired)// 蓄力到最大值
            {
                crtForce = maxForce;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                crtForce = minForce; // 重置力的大小
                fired = false; // 设置开火状态为未开火
            }
            else if (Input.GetButton("Fire1") && !fired)// 一直按着
            {
                crtForce += forceSpeed * Time.deltaTime; // 蓄力
                aimSlider.value = crtForce / maxForce; // 更新slider的值
            }
            else if (Input.GetButtonUp("Fire1") && !fired)
            {
                ShootWordBullet();
            }

        }
    }
    /// <summary>
    /// 下一个词条小球准备
    ///点击start后，在CreateOneCharacter 中调用一次
    /// </summary>
    public void ReadyWordBullet()
    {
        bulletInstance = Instantiate(bullet);

        //预制体相关
        bulletInstance.transform.SetParent(gang);
        bulletInstance.transform.localPosition = Vector3.zero;
       //bulletInstance.transform.localScale = Vector3.one;
        bulletInstance.transform.localEulerAngles = Vector3.zero;
        bulletInstance.transform.SetParent(bulletRoot);
        bulletInstance.GetComponent<SpriteRenderer>().color -=  new Color(0, 0, 0, 1);

        //增加词条图像

        //给小球增加词条属性

        abs = GameObject.Find("WordCollisionShoot").GetComponent<WordCollisionShoot>().absWord=bulletInstance.AddComponent(AllSkills.CreateSkillWord()) as AbstractWord0;
        foreach (var _col in (bulletInstance.GetComponentsInChildren<WordCollisionShoot>()))
            _col.absWord = abs;
        information.ChangeInformation(abs);
    }
    /// <summary>
    /// 产生词条实体
    /// </summary>
    void ShootWordBullet()
    {
        fired = true; // 设置开火状态为已开火

        //给词条添加一个初始的力
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletInstance.transform.up * crtForce* forceAmount);
        bulletInstance.transform.SetParent(afterShootTF);
        bulletInstance.GetComponent<Collider2D>().isTrigger = false;
        bulletInstance.GetComponent<SpriteRenderer>().color +=new Color(0,0,0,1);

        ReadyWordBullet();
        DestroyWordBullet();
    }
    private void DestroyWordBullet()
    {
        if (afterShootTF.childCount > 10)
        {
            Destroy(afterShootTF.GetChild(0).gameObject);
        }
    }
}
