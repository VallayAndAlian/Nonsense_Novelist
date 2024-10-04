using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestUI : MonoBehaviour
{
    public void ClickEnterNextStage()
    {
        GameMgr.instance.EnterTheStage(GameMgr.instance.stageCount + 1);
        GameMgr.instance.restUI.gameObject.SetActive(false);
    }
}
