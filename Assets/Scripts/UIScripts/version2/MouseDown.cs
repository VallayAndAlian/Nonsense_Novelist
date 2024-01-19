using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///（挂在main camera上）鼠标点击物体 显示信息
///</summary>
class MouseDown : MonoBehaviour
{
    /// <summary>属性面板预制体</summary>
    public GameObject characterDetailPrefab;
    /// <summary>UIcanvas</summary>
    public Canvas otherCanvas;
    /// <summary>预制体的克隆体</summary>
    private static GameObject a;
    /// <summary>预制体中的文字</summary>
    private Text[] texts=new Text[2];
    /// <summary>抽象角色</summary>
    [HideInInspector]
    public AbstractCharacter abschara;
    /// <summary>抽象角色身份</summary>
    [HideInInspector]
    public AbstractRole absRole;
    /// <summary>抽象形容词</summary>
    [HideInInspector]
    public AbstractAdjectives absAdj;
    /// <summary>抽象动词</summary>
    [HideInInspector]
    public AbstractVerbs absVerbs;
    /// <summary>抽象性格</summary>
    [HideInInspector]
    public AbstractTrait absTrait;
    /// <summary>判断当前是否有角色信息展示面板</summary>
    public bool isShow = false;
    /// <summary>音效</summary>
    public AudioSource audioSource;
    /// <summary>发射器</summary>
    public GameObject shoot;

    private void Update()
    {
        if (CharacterManager.instance.pause) return;
        if (isShow) return;
        MouseDownView();
    }  
    /// <summary>
    /// 全局鼠标射线
    /// </summary>
    private void MouseDownView()
    {
        
        if (Input.GetMouseButtonDown(1))//按下右键
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
            
            if(hit.collider!=null&&hit.collider.gameObject.tag=="character"&& !isShow)
            {
                //播放点击音效
                //audioSource.Play();

                //生成角色信息面板
                a = Instantiate(characterDetailPrefab, otherCanvas.transform);

                isShow = true;
                
                //获取点击角色的脚本信息
                abschara = hit.collider.GetComponent<AbstractCharacter>();

                a.GetComponentInChildren<CharacterDetail_t>().SetCharacter(hit.collider.gameObject);

                //if (hit.collider.GetComponent<AbstractAdjectives>())
                //{
                //    absAdj = hit.collider.GetComponent<AbstractAdjectives>();
                //}
                //else if (hit.collider.GetComponent<AbstractVerbs>())
                //{
                //    absVerbs = hit.collider.GetComponent<AbstractVerbs>();
                //}
        
                
              
            }

        }
    }

    public void CloseDetail()
    {
        isShow = false;
    }
}
