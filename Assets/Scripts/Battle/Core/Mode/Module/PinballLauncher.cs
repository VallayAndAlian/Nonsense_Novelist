using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;



public class PinBallLauncher : BattleModule
{
    public Transform launchPoint;
    private float currentChargeTime = 0f;
    private float chargeStartTime = 0f;
    private bool isCharging = false;
    List<Vector3> pathPoints = new List<Vector3>();


    public PinBall nowBall;
    
    public LineRenderer trajectoryLine;

    /// <summary>
    /// 发射小球
    /// </summary>
    public void LaunchPinBall()
    {
        CreateNewBallObj();
   
    }


    void CreateNewBallObj()
    {           
        if (launchPoint == null )
        {
            launchPoint=GameObject.Find("bulletroot").transform;
        }   
        PoolMgr.GetInstance().GetSOObj(prefabSOType.BattleObj,0,(obj)=>
        {
            obj.transform.position=launchPoint.position;
            obj.transform.rotation=Quaternion.identity;
        
            nowBall.mBall.transform=obj.transform;
            SetNowBallVel();
            nowBall.ShootOut(); 
            nowBall=null;
            Battle.mCardDeckManager.UseCurrentCard();
        CreateNewBall();
        });
        
    }

    void CreateNewBall()
    {
        if (launchPoint == null )
        {
            launchPoint=GameObject.Find("bulletroot").transform;
        }  
        WordTable.Data data=Battle.mCardDeckManager.GetCurrentCard();
        PinBall pinball=Battle.mObjectFactory.CreatePinBall(data);
        GameObject temp=new GameObject();
        temp.transform.position=launchPoint.position;
        pinball.mBall = new PinBall.Ball
        {
            transform = temp.transform,
            velocity = Vector3.zero,
            radius = BattleConfig.mData.word.wordBallRadius,
            friction = BattleConfig.mData.word.wordBallFriction,
            energyLoss = BattleConfig.mData.word.wordBallCollisionLoss,
            hasShoot=false
        };
        nowBall=pinball;
    }

    void SetNowBallVel()
    {
        if(nowBall==null)
            return; 
        nowBall.mBall.velocity=nowBall.mBall.preVelocity=CalculateLaunchVelocity();
    }

    private Vector2 CalculateLaunchVelocity()
    {
        float speed = Mathf.Lerp
        (BattleConfig.mData.word.wordBallMinSpeed, BattleConfig.mData.word.wordBallMaxSpeed, 
        currentChargeTime / BattleConfig.mData.word.wordBallChargingMaxTime);

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0; 
        Vector2 direction = (mouseWorldPosition - launchPoint.position).normalized;
        Debug.Log(direction);
        return direction * speed;
    }
    public override void LateUpdate(float deltaSec)
    { 

    }
    public override void Update(float deltaSec)
    {
        if (Input.GetMouseButton(0))
        {
            if (!isCharging)
            {
                CreateNewBall();
                isCharging = true;
                chargeStartTime = 0;
            }
            chargeStartTime+=deltaSec;
            currentChargeTime = Mathf.Min(chargeStartTime, BattleConfig.mData.word.wordBallChargingMaxTime);
            SetNowBallVel();
            ShowTrajectory();  
            
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isCharging)
            {        
                LaunchPinBall();
                currentChargeTime = 0f;
                isCharging = false;
             
            }
        }
    }

    public void ShowTrajectory()
    {
         if (launchPoint == null )
        {
            launchPoint=GameObject.Find("bulletroot").transform;
        } 
       var preInfo=nowBall.mPreInfo;


        pathPoints .Clear();
        pathPoints.Add(launchPoint.position); // 起点
        for(int i=0;i<preInfo.colPos.Count;i++)
        {

             pathPoints.Add(preInfo.colPos[i]); 
        }
        if(nowBall.mBall.preTransform!=null)
            pathPoints.Add(nowBall.mBall.preTransform.position);   // 射线的延伸终点

        if(trajectoryLine==null)
            launchPoint.gameObject.TryGetComponent<LineRenderer>(out trajectoryLine);
        trajectoryLine.startWidth=BattleConfig.mData.word.wordBallRadius;
        trajectoryLine.endWidth=BattleConfig.mData.word.wordBallRadius;
        trajectoryLine.positionCount = Mathf.Min(pathPoints.Count,BattleConfig.mData.word.maxPreColTimes);
        for (int i = 0; i < Mathf.Min(pathPoints.Count,BattleConfig.mData.word.maxPreColTimes); i++)
        {
            trajectoryLine.SetPosition(i, pathPoints[i]);
        }
        
    }

    public override void Init()
    {
        base.Init();
        
    }
    public override void Dispose()
    {
        base.Dispose();
    }
}
