using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebugUi : MonoBehaviour
{
    bool oriGamePause = false;
    Text text1;Text text2;
    private void Awake()
    {
       
        Time.timeScale = 0;

        text1 = transform.Find("Time1").GetComponent<Text>();
        text2 = transform.Find("Time2").GetComponent<Text>();
        text1.text = GameMgr.instance.time1.ToString();
        text2.text = GameMgr.instance.time2.ToString();
    }

    #region 外部点击事件
    public void Exit()
    {
        Time.timeScale = 0.99F;
        Destroy(this.gameObject);
       
    }
    #endregion
}
