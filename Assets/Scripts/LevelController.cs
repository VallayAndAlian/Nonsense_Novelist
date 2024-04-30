using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetLevelTo(int level)
    {
        anim.SetInteger("index", 2);
    }

    #region ¶¯»­µ÷ÓÃ
    public void StartChangeLevel()
    {
        CharacterManager.instance.pause = true;
    }

    public void  EndChangeLevel()
    {
        CharacterManager.instance.pause = false;
    }
    #endregion
}
