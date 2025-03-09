
using ImGuiNET;
using UnityEngine;
using System;

public class DebugUnitInspector : BattleDebugModule
{
    public override string ModuleName => "单位检视器";
    public override string ToolTip => "查看Unit的信息";
    
    public override void OnDrawImGui(BattleDebugContext context)
    {
        foreach (var unit in context.mBattle.ObjectManager.Units.Values)
        {
            if (ImGui.TreeNode($"{unit.Data.mName}_{unit.ID}"))
            {
                DrawUnit(unit);
                ImGui.TreePop();
            }
        }
    }

    public static void DrawUnit(BattleUnit unit)
    {
        ImGui.Text($"单位Kind: {unit.UnitInstance.mKind}");
        
        ImGui.Text($"单位血量:");
        ImGui.SameLine();
        ImGui.ProgressBar(unit.HpPercent, new Vector2(150, 20));
        ImGui.SameLine();
        ImGui.Text($"{unit.Hp}/{unit.MaxHp}");

        if (ImGui.Button("杀死单位"))
        {
            unit.Die(null);
        }

        if (ImGui.BeginTabBar("单位信息"))
        {
            DrawAIInfo(unit);
            DrawUnitAttributes(unit);
            
            ImGui.EndTabBar();
        }
        
    }

    public static void DrawAIInfo(BattleUnit unit)
    {
        if (ImGui.BeginTabItem("AI"))
        {
            ImGui.Text($"状态：{unit.AIAgent.State.ToString()}");
            ImGui.EndTabItem();
        }
    }

    public static void DrawUnitAttributes(BattleUnit unit)
    {
        if (ImGui.BeginTabItem("属性"))
        {
            ImGui.Columns(2);
            ImGui.Text("属性名");ImGui.NextColumn();
            ImGui.Text("属性值");ImGui.NextColumn();

            foreach (AttributeType attyType in Enum.GetValues(typeof(AttributeType)))
            {
                if (attyType == AttributeType.None)
                    continue;

                ImGui.Text(attyType.ToString());
                ImGui.NextColumn();
                ImGui.Text($"{unit.GetAttributeValue(attyType)}");
                ImGui.NextColumn();

            }
            
            ImGui.Columns();
            
            ImGui.EndTabItem();
        }
    }
}