using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleUI : UIBase
{
    public BattleBase Battle {
        get => mBattle;
        set => mBattle = value;
    }
    protected BattleBase mBattle;
    public BattleUI(GameObject gameObject) : base(gameObject) 
    { 

    } 
    public override void Init() { }

    public override void Update(float deltaTime) { }

    public override void LateUpdate(float deltaTime) { }
}
