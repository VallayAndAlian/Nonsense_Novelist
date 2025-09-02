using System.Collections.Generic;
using ImGuiNET;
using UnityEngine;

public class DebugEffect : BattleDebugModule
{
    public override string ModuleName => "Buff查看器";
    public override string ToolTip => "";

    protected int mPickIdx = 0;
    
    protected Dictionary<int, string> mItems = new Dictionary<int, string>();

    public override void OnRegistered()
    {
        foreach (var it in BattleEffectTable.DataList)
        {
            mItems.Add(it.Key, $"{it.Key}_{it.Value.mName}");
        }
    }

    public override void OnDrawImGui(BattleDebugContext context)
    {
        ImGuiFunc.Combo("Effect列表", mItems, ref mPickIdx, (int key, string value) => value);
        ImGui.Spacing();
        
        var unit = context.mPickedUnit;
        if (unit.IsValid())
        {
            if (mPickIdx > 0 && ImGui.Button("添加Effect"))
            {
                EffectAgent.ApplyEffectToTarget(unit, new BattleEffectApplier(mPickIdx, null));
            }
        }
        
        DrawUnitEffect(context);
    }

    protected void DrawUnitEffect(BattleDebugContext context)
    {
        if (context.mPickedUnit == null)
            return;

        var effectAgent = context.mPickedUnit.EffectAgent;
        
        ImGui.Columns(4);
        ImGui.Separator();
        ImGui.Text("操作");ImGui.NextColumn();
        ImGui.Text("类型");ImGui.NextColumn();
        ImGui.Text("持续时间");ImGui.NextColumn();
        ImGui.Text("堆叠数量");ImGui.NextColumn();
        ImGui.Separator();

        int Count = 1;
        
        foreach (var effect in effectAgent.Effects)
        {
            if (effect.mExpired)
                continue;
            
            ImGui.PushID(ModuleImGuiID + Count++);
            
            if (ImGui.Button("移除"))
            {
                effect.MarkInvalid();
            }
            ImGui.NextColumn();
            
            ImGui.Text($"{effect.mType.ToString()}");ImGui.NextColumn();

            if (effect.mDefine.mDurationRule == EffectDurationRule.HasDuration)
            {
                ImGui.Text((effect.mExpiredTime - context.mBattle.Now).ToString("F2"));
            }
            else
            {
                ImGui.Text("Script");
            }
            
            ImGui.NextColumn();
            ImGui.Text($"{effect.mStackCount}");ImGui.NextColumn();
            
            ImGui.PopID();
        }
        
        ImGui.Separator();
        ImGui.Columns();
    }
}