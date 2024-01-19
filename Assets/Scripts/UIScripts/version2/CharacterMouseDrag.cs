using System.Collections;
using UnityEngine;

/// <summary>
/// 版本二：拖拽角色站位
/// before start
/// </summary>
public class CharacterMouseDrag : MonoBehaviour
{
    /// <summary>OnMouseDown相关参数</summary>
    private Vector3 targetScreenpos;//拖拽物体的屏幕坐标
    private Vector3 targetWorldpos;//拖拽物体的世界坐标
    private Transform target;//拖拽物体
    private Vector3 mouseScreenpos;//鼠标的屏幕坐标
    private Vector3 offset;//偏移量

    /// <summary>记录目前所在的站位</summary>
    private Transform nowParentTF;
    /// <summary>记录上一个所在的站位</summary>
    private Transform lastParentTF;
    /// <summary>角色和站位position的Y偏移量</summary>
    public static float offsetY =0.2f;

    //颜色
    private Color colorOnMouseOver = new Color((float)100 / 255, (float)100 / 255, (float)50 / 255, (float)255 / 255);
    private Color colorOnMouseExit = new Color((float)255 / 255, (float)255 / 255, (float)255 / 255, (float)255 / 255);

    private SpriteRenderer sr;

    //角色战后大小
    private float afterScale=0.28f;
    private void Start()
    {
        nowParentTF = transform.parent;
        target = transform;
        sr= GetComponentInChildren<AI.MyState0>().GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        
    }
    #region OnMouseDrag()随鼠标移动(废弃)

    /* private void OnMouseDrag()
     {
         //随鼠标移动
         var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         transform.position = new Vector3(pos.x, pos.y, transform.position.z);
         
     }*/
    #endregion

    /// <summary>
    /// 鼠标悬停
    /// </summary>
    private void OnMouseOver()
    {
        //颜色变黄
        sr.color = colorOnMouseOver;
       
    }
    private void OnMouseExit()
    {
        //颜色恢复
        //鼠标进入黄圈时也会执行此脚本。原因未知
        sr.color = colorOnMouseExit;
      
    }
    //被移动物体需要添加collider组件，以响应OnMouseDown()函数
    //基本思路。当鼠标点击物体时（OnMouseDown（），函数体里面代码只执行一次），
    //记录此时鼠标坐标和物体坐标，并求得差值。如果此后用户仍然按着鼠标左键，那么保持之前的差值不变即可。
    //由于物体坐标是世界坐标，鼠标坐标是屏幕坐标，需要进行转换。具体过程如下所示。
    IEnumerator OnMouseDown()
    {
        //先让点击物体中心与鼠标点击处同步。
        Vector2 offsetCenter = GetComponent<Collider2D>().offset;
        mouseScreenpos = new Vector3(Input.mousePosition.x, Input.mousePosition.y , Camera.main.WorldToScreenPoint(target.position).z);
        target.position= Camera.main.ScreenToWorldPoint(mouseScreenpos)-new Vector3(offsetCenter.x/4,offsetCenter.y/4,0);

        targetScreenpos = Camera.main.WorldToScreenPoint(target.position);
        offset = target.position - Camera.main.ScreenToWorldPoint(mouseScreenpos);

      

        while (Input.GetMouseButton(0))//鼠标左键被持续按下。
        {
            mouseScreenpos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenpos.z);
            targetWorldpos = Camera.main.ScreenToWorldPoint(mouseScreenpos) + offset;
            target.position = targetWorldpos;
            yield return new WaitForFixedUpdate();
        }
    }
    private void OnMouseUp()
    {
        //加上了检测层级（忽略角色本身）
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,100,LayerMask.GetMask("Situation"));
        if (hit.collider != null)
        {
           

            if ((hit.collider.gameObject.layer == LayerMask.NameToLayer("Situation"))&& (hit.collider.transform.childCount==0))
            {

                var temp = lastParentTF;
                //角色拖拽到站位上且位置校准
                lastParentTF = nowParentTF;
                nowParentTF = hit.transform;
                this.transform.SetParent(nowParentTF);
                 if(temp == null) this.transform.localScale = Vector3.one * afterScale;
                
                transform.position = new Vector3(nowParentTF.position.x, nowParentTF.position.y + offsetY, nowParentTF.position.z);

                //隐藏/恢复站位颜色（透明度为0
    
                if (lastParentTF.gameObject.GetComponent<SpriteRenderer>())
                    lastParentTF.gameObject.GetComponent<SpriteRenderer>().color = colorOnMouseExit;
                nowParentTF.gameObject.GetComponent<SpriteRenderer>().color = Color.grey;


                //根据站位给角色站位赋值
                AbstractCharacter c = this.GetComponent<AbstractCharacter>();   
                c.situation = hit.collider.gameObject.GetComponent<Situation>();

                //根据站位给角色阵营赋值
                if (hit.collider.gameObject.GetComponent<Situation>().number < 5)
                {
                    //图片翻转方向
                    if (c.camp == CampEnum.right)
                    {
                        this.GetComponent<AbstractCharacter>().turn(); 
                    }
                    c.camp = CampEnum.left;
                    //去重
                    if (CharacterManager.charas_right.Contains(c))
                    {
                        CharacterManager.charas_right.Remove(c);
                    }
                    //加入阵营
                    if (!CharacterManager.charas_left.Contains(c)) CharacterManager.charas_left.Add(c);
                }
                else
                {
                    if (c.camp != CampEnum.right)
                    {
                        this.GetComponent<AbstractCharacter>().turn();
             
                    }
                    c.camp = CampEnum.right;
                    if (CharacterManager.charas_left.Contains(c)) CharacterManager.charas_left.Remove(c);
                    if (!CharacterManager.charas_right.Contains(c)) CharacterManager.charas_right.Add(c);
                }
            }
            else//没有检测到站位
            {
                transform.position = new Vector3(nowParentTF.position.x, nowParentTF.position.y + offsetY, nowParentTF.position.z);

            }
        }
        else//没有检测到站位
        {
            transform.position = new Vector3(nowParentTF.position.x, nowParentTF.position.y + offsetY, nowParentTF.position.z);

        }
       
    }
} 

