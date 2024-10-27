using UnityEngine;

public class UnitViewBase : MonoBehaviour
{
    protected BattleUnit mRole = null;

    public bool IsCompatibleType(BattleUnit role)
    {
        return true;
    }

    public void Setup(BattleUnit role)
    {
        mRole = role;
        mRole.UnitView = this;
    }
}