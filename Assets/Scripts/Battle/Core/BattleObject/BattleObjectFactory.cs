using System.Collections.Generic;
using UnityEngine;

public enum BattleObjectType
{
    none = 0,
    BattleUnit = 1,
    PinBall = 2,
    Wall = 3,
}

public enum BattleUnitType
{
    Character = 0,
    Servant = 1,
    Monster = 2,
}

public class BattleObjectFactory : BattleModule
{
    private static Dictionary<BattleObjectType, System.Type> mBattleObjectClassMap =
        new Dictionary<BattleObjectType, System.Type>()
        {
            { BattleObjectType.BattleUnit, typeof(BattleUnit) },
            { BattleObjectType.PinBall, typeof(PinBall) },
            { BattleObjectType.Wall, typeof(WallObject) },
        };

    #region pinball

    public PinBall CreatePinBall(WordTable.Data data)
    {
        if (data == null)
            return null;
        
        PinBall ball = null;
        switch (data.mShootType)
        {
            case ShootType.None:
                ball = new PinBall_none();
                break;
            case ShootType.Split:
                ball = new PinBall_Split();
                break;
            case ShootType.Alpha:
                ball = new PinBall_Alpha();
                break;
            case ShootType.spread:
                ball = new PinBall_Spread();
                break;
            case ShootType.Activate:
                ball = new PinBall_Activate();
                break;
            case ShootType.Small:
                ball = new PinBall_Small();
                break;
            case ShootType.Big:
                ball = new PinBall_Big();
                break;
            case ShootType.Add:
                ball = new PinBall_Add();
                break;
            case ShootType.Mirror:
                ball = new PinBall_Mirror();
                break;
            case ShootType.Copy:
                ball = new PinBall_Copy();
                break;
            case ShootType.Dead:
                ball = new PinBall_Dead();
                break;
            case ShootType.Expect:
                ball = new PinBall_Expect();
                break;
            case ShootType.ReTrigger:
                ball = new PinBall_ReTrigger();
                break;
            case ShootType.SameChara:
                ball = new PinBall_SameChara();
                break;
            case ShootType.Servants:
                ball = new PinBall_Servants();
                break;
            case ShootType.Start:
                ball = new PinBall_Start();
                break;
            
            default:
                break;
        }
        
        if (ball == null)
            return null;
        
        ball.mBall = new PinBall.Ball
        {
            mWordData = data,
            mVelocity = Vector3.zero,
            mRadius = BattleConfig.mData.word.wordBallRadius,
            mFriction = BattleConfig.mData.word.wordBallFriction,
            mEnergyLoss = BattleConfig.mData.word.wordBallCollisionLoss
        };
        
        Battle.ObjectManager.RegisterObject(ball);
        return ball;
    }
    
    #endregion


    #region Wall

    public WallObject CreateWall<T>(Collider2D collider) where T : WallObject, new()
    {
        T wall = new T();
        Battle.ObjectManager.RegisterWall<T>(wall, collider);
        return wall;
    }

    #endregion


    public BattleUnit CreateBattleUnit(UnitInstance instance, UnitPlacement placement = new UnitPlacement())
    {
        var unitData = BattleUnitTable.Find(instance.mKind);
        if (unitData == null)
        {
            Debug.LogError($"unit_{instance.mKind} not found data");
            return null;
        }

        if (unitData.mForbidden)
        {
            Debug.LogError($"unit_{instance.mKind} is forbidden");
            return null;
        }

        var asset = AssetManager.Load<BattleUnitSO>("SO/BattleUnit", unitData.mAsset);
        if (asset == null)
        {
            Debug.LogError("unit_{instance.mKind} not found asset");
            return null;
        }

        var obj = Object.Instantiate(asset.prefab);
        if (obj == null)
        {
            Debug.LogError("unit_{instance.mKind} no prefab");
            return null;
        }

        var pawnTransform = obj.transform.Find("Pawn");
        if (pawnTransform == null)
        {
            Debug.LogError("unit_{instance.mKind} no pawn component");
            return null;
        }

        var unit = new BattleUnit(unitData, instance);

        var slot = Battle.Stage.GetSlot(placement.mSlotIndex);
        if (slot != null)
        {
            slot.OccupiedBy(unit);
        }

        Battle.ObjectManager.RegisterUnit(unit);

        // bind view
        var unitView = pawnTransform.GetComponent<UnitViewBase>();
        if (unitView == null)
            unitView = pawnTransform.gameObject.AddComponent<UnitViewBase>();

        unitView.Setup(unit, asset);

        if (unitData.mInitType != BattleUnitType.Servant)
        {
            var infoUI = Battle.BattleUI.Add(new BattleUnitSelfUI(unit));
            Battle.BattleUI.ShowPanel(infoUI);
        }

        EventManager.Invoke(EventEnum.UnitSpawn, unit);

        return unit;
    }


    public static void StartEmit(EmitMeta meta)
    {
        // create projectile
        var projData = EmitTable.Find(meta.mProjKind);
        if (projData == null)
        {
            Debug.LogError($"can not find proj obj data, kind {meta.mProjKind}!");
            meta.OnHitTarget();
            return;
        }

        var projAsset = AssetManager.Load<EmitSO>("SO/Emit", projData.mAsset);
        if (projAsset == null)
        {
            Debug.LogError($"can not load proj asset, kind {meta.mProjKind}!");
            meta.OnHitTarget();
            return;
        }

        var projObj = Object.Instantiate(projAsset.projObject);
        if (projObj == null)
        {
            Debug.LogError($"can not create proj obj, kind {meta.mProjKind}!");
            meta.OnHitTarget();
            return;
        }

        var proj = projObj.GetComponent<NnProjectile>();
        if (proj == null)
        {
            proj = projObj.AddComponent<NnProjectile>();
        }

        meta.mData = projData;
        proj.Setup(meta, projAsset);

        proj.Emit();
    }
}