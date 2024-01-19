using UnityEngine;
using UnityEngine.UI;

///<summary>
///角色移动+摄像机移动(挂摄像机上
///</summary>
class CharacterTranslateAndCamera : MonoBehaviour
{
    /// <summary>当前关卡所有角色</summary>
    private GameObject[] characters1;
    /// <summary>移动的摄像机</summary>
    private Camera camera_;
    /// <summary>移动速度</summary>
    public float moveSpeed = 0.1f;
    /// <summary>暂停的点</summary>
    private GameObject[] targets;
    /// <summary>当前关卡序号</summary>
    public int guanQiaNum = 0;
    /// <summary>当前章节序号</summary>需要在inspector界面根据当前章节赋值
    public int chapterNum = 2;
    /// <summary>装饰品</summary>
    public GameObject[] zhuangShiPin;
    /// <summary>关卡图片</summary>
    public GameObject[] chapterName;
    

    private void Start()
    {
        camera_ = GameObject.Find("MainCamera").GetComponent<Camera>();
        targets = new GameObject[3];
        targets[1] = GameObject.Find("target2");
        targets[2] = GameObject.Find("target3");
        
    }
    private void FixedUpdate()
    {
        if (UIManager.nextQuanQia && characters1.Length != 0)
        {   
            //摄像机移动
            camera_.transform.Translate(Vector3.right*moveSpeed);
            //剩余角色移动
            foreach(var item in characters1)
            {
                item.transform.Translate(Vector3.right * moveSpeed);
            }
        }
        EndMove();
    }
    public void BeginMove()
    {
        AbstractCharacter[] tempCharacters = GameObject.Find("SelfCharacter").GetComponentsInChildren<AbstractCharacter>();
        characters1 = new GameObject[tempCharacters.Length];
        for(int i=0; i < tempCharacters.Length; i++)
        {
            characters1[i] = tempCharacters[i].gameObject;
        }
    }
    public void EndMove()
    {
        if (guanQiaNum<=1&&Vector3.Distance(camera_.transform.position, targets[guanQiaNum+1].transform.position) <= 0.5f)
        {
            camera_.transform.position = targets[guanQiaNum+1].transform.position;
            UIManager.nextQuanQia = false;
            //装饰品和关卡名图片切换
            zhuangShiPin[guanQiaNum + 1].SetActive(true);
            zhuangShiPin[guanQiaNum].SetActive(false);
            chapterName[guanQiaNum + 1].SetActive(true);
            chapterName[guanQiaNum].SetActive(false);

            guanQiaNum++;//这一行永远放在最后面
        }
    }
}
