using UnityEngine;

public class BattleDebugContext
{
    public UImGui.UImGui mImGuiObj = null;
    public BattleBase mBattle = null;
}

public class BattleDebugModule : MonoBehaviour
{
    public virtual string ModuleName => "Null";

    public virtual string ToolTip => "";
    
    public virtual void OnRegistered() {}
    
    public virtual void OnDrawImGui(BattleDebugContext context) {}
    
    public virtual void Tick(float deltaTime) {}
}