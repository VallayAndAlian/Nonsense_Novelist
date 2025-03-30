
using System.Collections.Generic;
using ImGuiNET;
using UnityEngine;

public class DebugSpawnUnit : BattleDebugModule
{
    public override string ModuleName => "单位生成器";
    public override string ToolTip => "在规定区域内生成新单位, 按C建生成";
    
    protected int mPickIdx = 0;

    protected Dictionary<int, string> mItems = new Dictionary<int, string>();

    public override void OnRegistered()
    {
        foreach (var it in BattleUnitTable.DataList)
        {
            mItems.Add(it.Key, $"{it.Key}_{it.Value.mName}");
        }
    }

    public override void OnDrawImGui(BattleDebugContext context)
    {
        ImGuiFunc.Combo("UnitList", mItems, ref mPickIdx, (int key, string value) => value);
        
        if (mPickIdx > 0 && Input.GetKeyDown(KeyCode.C))
        {
            var foundSlot = context.mBattle.Stage.FindClosestSlot(UnitSlotType.BackSeat | UnitSlotType.FrontSeat,
                ClientUtils.GetMouseWorldPosition(), 5.0f);

            if (foundSlot != null)
            {
                UnitInstance instance = new UnitInstance()
                {
                    mKind = mPickIdx,
                    mCamp = foundSlot.SpawnCamp,
                };

                UnitPlacement placement = new UnitPlacement()
                {
                    mSlotIndex = foundSlot.ID,
                };
                
                context.mBattle.ObjectFactory.CreateBattleUnit(instance, placement);
            }
        }
    }
}