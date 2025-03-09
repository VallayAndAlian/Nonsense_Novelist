using UnityEngine;
using UnityEngine.UI;
public class BattlePhaseUI_Rest : BattleUI
{
    protected Button startButton;
    public BattlePhaseUI_Rest(GameObject gameObject) : base(gameObject) 
    { 

    } 
    public override void Init() 
    {
        base.Init();
        
        if(startButton==null)
        {
            startButton=UIPanel.transform.Find("Panel/startBtn").GetComponent<Button>();
            startButton.onClick.AddListener(OnClickStartButton);
        }
    }
    public void OnClickStartButton()
    {
        Battle.BattlePhase.EnterNextStage();
    }
    
    public override void Update(float deltaTime) 
    {
        
    }

    public void RefreshSlider(float amount)
    {}

    
}