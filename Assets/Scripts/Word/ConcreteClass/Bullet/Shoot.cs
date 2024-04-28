using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using Unity.Burst.CompilerServices;

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
    public float forceAmount = 1;
    /// <summary>有无发射</summary>
    private bool fired = false;
    /// <summary>蓄力Slider</summary>
    public Slider aimSlider;
    /// <summary>存储WordCollisionShoot的词条属性</summary>
    public static AbstractWord0 abs;
    /// <summary>手动，词条信息板 </summary>
    public WordInformation information;
    /// <summary>手动，轨迹脚本 </summary>
    public Track track;
    public float dotScale = 0.15f;
    private Vector2 normall;
    public static Vector2 pointt;
    private LayerMask mask = 9;
    RaycastHit2D hit;
    RaycastHit2D hit2;
    public float ra = 1f;
    private void Update()
    {
        if (CreateOneCharacter.isTwoSides && CreateOneCharacter.isAllCharaUp)
        {
            aimSlider.value = 0; // 重置slider的值
           //ordGrid();
            if (crtForce >= maxForce && !fired)// 蓄力到最大值
            {
                crtForce = maxForce;
            }

            if (!Input.GetButton("Fire1"))
            {
                track.Hide();
            }


            if (Input.GetButtonDown("Fire1"))
            {
                crtForce = minForce; // 重置力的大小
                fired = false; // 设置开火状态为未开火
                track.Show();
            }
            else if (Input.GetButton("Fire1") && !fired)// 一直按着
            {
                crtForce += forceSpeed * Time.deltaTime; // 蓄力
                aimSlider.value = crtForce / maxForce; // 更新slider的值


                //2d射线检测需要指定参与碰撞的layer 并且添加射线长度，否则会失效
                LayerMask layer = 1 << 9;
                hit = Physics2D.Raycast(new Vector3(gang.GetChild(0).position.x, gang.GetChild(0).position.y, 0), gang.GetChild(0).transform.up, Mathf.Infinity, layer);
                //Debug.DrawLine(new Vector3(gang.GetChild(0).position.x, gang.GetChild(0).position.y, 0), hit.point, Color.black);
                hit2 = Physics2D.CircleCast(new Vector3(gang.GetChild(0).position.x, gang.GetChild(0).position.y, 0), ra, gang.GetChild(0).transform.up, Mathf.Infinity, layer);
                if (hit2.collider != null)
                {

                    pointt = hit2.point;
                    normall = hit2.normal;
                }
                Vector3 re = Track.Reflectt(gang.GetChild(0).transform.up, normall);
                track.UpDateDots0(new Vector3(gang.GetChild(0).position.x, gang.GetChild(0).position.y, 0), dotScale * gang.GetChild(0).transform.up * crtForce, pointt, dotScale * re * crtForce);
            }
            else if (Input.GetButtonUp("Fire1") && !fired&&wordCount!=0)
            {
                ShootWordBullet();
                track.Hide();
            }
        }
    }


    private Vector3 oriScale;
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
        bulletInstance.transform.localEulerAngles = Vector3.zero;
        bulletInstance.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
        /*//给小球增加词条属性【原版】
        abs = GameObject.Find("WordCollisionShoot").GetComponent<WordCollisionShoot>().absWord = 
            bulletInstance.AddComponent(GameMgr.instance.GetNowListOne()) as AbstractWord0;*/
        if (CreateOneCharacter.isStart)//true，开始后抽一个
        {
            //给小球增加词条属性【测试】
            abs = GameObject.Find("WordCollisionShoot").GetComponent<WordCollisionShoot>().absWord =
                bulletInstance.AddComponent(GameMgr.instance.GetGoingUseListOne()) as AbstractWord0;

        }
        else//开始时抽3个
        {
            //给小球增加词条属性【测试】
            abs = GameObject.Find("WordCollisionShoot").GetComponent<WordCollisionShoot>().absWord =
                bulletInstance.AddComponent(GameMgr.instance.GetGoingUseList()) as AbstractWord0;
        }
        //小球信息
        foreach (var _col in (bulletInstance.GetComponentsInChildren<WordCollisionShoot>()))
            _col.absWord = abs;
        information.ChangeInformation(abs);

        //
        oriScale = bulletInstance.transform.localScale;
        bulletInstance.transform.localScale = Vector3.zero;
    }
    bool isShoot = false;
    /// <summary>
    /// 产生词条实体
    /// </summary>
    void ShootWordBullet()
    {
        fired = true; // 设置开火状态为已开火
        //给词条添加一个初始的力
        bulletInstance.transform.localScale = oriScale;
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletInstance.transform.up * crtForce * forceAmount);
        bulletInstance.transform.SetParent(afterShootTF);
        bulletInstance.GetComponent<Collider2D>().isTrigger = false;

        bulletInstance.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        //清空待使用牌库第一个
        GameMgr.instance.wordGoingUseList.RemoveAt(0);
        //槽位变量
        wordCount--;
        isShoot = true;

        ReadyWordBullet();
        DestroyWordBullet();
    }
    float gridTime = 3f;
    float timer = 0f;
    int wordCount = 3;
    public static int sunWordCount = 3;
    /// <summary>
    /// 槽位更新
    /// </summary>
   /* public void WordGrid()
    {
        GameObject a = GameObject.Find("combatCanvas");
        if (isShoot)//发射1次刷新1次
        {
            timer += Time.deltaTime;
            if (timer > gridTime)
            {
                timer = 0;
                wordCount++;
                if (wordCount == sunWordCount)
                {
                    isShoot = false;
                }
            }
        }
        *//*a.transform.Find("ShootTime").transform.GetChild(1).GetComponent<Slider>().value = 1;
        a.transform.Find("ShootTime").transform.GetChild(2).GetComponent<Slider>().value = 1;
        a.transform.Find("ShootTime").transform.GetChild(0).GetComponent<Slider>().value = 1;*//*

        if (wordCount == 3&&wordCount<=sunWordCount)
        {

            *//*a.transform.Find("ShootTime/Slider0/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[0] + "");
            a.transform.Find("ShootTime/Slider1/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[1] + "");
            a.transform.Find("ShootTime/Slider2/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[2] + "");
            *//*
        }
        else if (wordCount==2 )
        {
            if (sunWordCount == 3)//3号在加载
            {
                a.transform.Find("ShootTime").transform.GetChild(2).GetComponent<Slider>().value = (float)(timer / 3f);

                //a.transform.Find("ShootTime/Slider0/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[0]+"");
                //a.transform.Find("ShootTime/Slider1/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[1] + "");

            }
            else if(sunWordCount == 2)
            {
                //a.transform.Find("ShootTime/Slider0/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[0]+"");
                //a.transform.Find("ShootTime/Slider1/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[1] + "");

            }
        }
        else if (wordCount == 1 && wordCount <= sunWordCount)
        {
            if (sunWordCount == 3)//2号加载，3号排队
            {
                //a.transform.Find("ShootTime/Slider0/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[0] + "");
                a.transform.Find("ShootTime").transform.GetChild(1).GetComponent<Slider>().value = (float)(timer / 3f);
                a.transform.Find("ShootTime").transform.GetChild(2).GetComponent<Slider>().value = 1;

                //加载排队的图片
                //a.transform.Find("ShootTime/Slider2/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>("");

            }
            else if( sunWordCount == 2)//2号加载
            {
                a.transform.Find("ShootTime").transform.GetChild(1).GetComponent<Slider>().value = (float)(timer / 3f);

            }
        }
        else if (wordCount == 0 && wordCount <= sunWordCount)
        {
            if (sunWordCount == 3)//1号加载，2、3号排队
            {
                a.transform.Find("ShootTime").transform.GetChild(0).GetComponent<Slider>().value = (float)(timer / 3f);
                a.transform.Find("ShootTime").transform.GetChild(1).GetComponent<Slider>().value = 1;
                //排队
                //a.transform.Find("ShootTime/Slider1/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[1] + "");
                //a.transform.Find("ShootTime/Slider1/Fill").GetComponent<Image>().sprite = Resources.Load<Sprite>(GameMgr.instance.wordGoingUseList[1] + "");

            }
            else if (sunWordCount == 2)//1号加载，2号排队
            {

            }
            else if (sunWordCount == 1)//1号加载，2号排队
            {

            }
        }
    }*/
    private void DestroyWordBullet()
    {
        if (afterShootTF.childCount > 10)
        {
            Destroy(afterShootTF.GetChild(0).gameObject);
        }
    }
}
