using System.Collections.Generic;
using UnityEngine;

public delegate void TimerHandler();

class TimerNode
{
    public bool Valid => mRemoved || (mCallback != null && !mCallback.Target.Equals(null));
    
    public TimerHandler mCallback;
    public float mRepeatInterval; // 定时器触发的时间间隔;
    public float mDelay; // 第一次触发要隔多少时间;
    public int mRepeatTimes; // 你要触发的次数;
    public float mPassedTime; // 这个Timer过去的时间;
    public bool mRemoved; // 是否已经删除了
    public int mTimerId; // 标识这个timer的唯一Id号;
}

public class TimerManager
{
    private Dictionary<int, TimerNode> mTimers = null;//存放Timer对象

    private List<int> mRemoveTimers = null;//新增Timer缓存队列
    private List<TimerNode> mNewTimers = null;//删除Timer缓存队列

    private int mIncId = 1;//每个Timer的唯一标示

    //初始化Timer管理器
    public void Init()
    {
        mTimers = new Dictionary<int, TimerNode>();
        mNewTimers = new List<TimerNode>();
        mRemoveTimers = new List<int>();
        
        mIncId = 1;
    }

    /// <summary>
    /// 以秒为单位调用方法methodName，然后在每个repeatRate重复调用。
    /// </summary>
    /// <param name="callBack">回调函数</param>
    /// <param name="firstDelay">延迟调用</param>
    /// <param name="repeatInterval">时间间隔</param>
    /// <param name="repeatTimes">重复调用的次数 小于等于0表示无限触发</param>
    public int AddTimer(TimerHandler callBack, float firstDelay, float repeatInterval, int repeatTimes=0)
    {
        TimerNode timer = new TimerNode
        {
            mCallback = callBack,
            mRepeatTimes = repeatTimes,
            mRepeatInterval = repeatInterval,
            mDelay = firstDelay,
            mPassedTime = repeatInterval - firstDelay, // 延迟调用
            mRemoved = false,
            mTimerId = mIncId
        };
        mIncId++;
        
        mNewTimers.Add(timer); // 加到缓存队列里面
        return timer.mTimerId;
    }

    //移除Timers
    public void RemoveTimer(int timerId)
    {
        if (!mTimers.ContainsKey(timerId))
            return;
        
        TimerNode timer = mTimers[timerId];
        timer.mRemoved = true; // 先标记，不直接删除
    }

    //在Update里面调用
    public void Update(float deltaTime)
    {
        // 添加新的Timers
        for (int i = 0; i < mNewTimers.Count; i++)
        {
            mTimers.Add(mNewTimers[i].mTimerId, mNewTimers[i]);
        }
        mNewTimers.Clear();
        
        // 更新
        foreach (TimerNode timer in mTimers.Values)
        {
            if (!timer.Valid)
            {
                mRemoveTimers.Add(timer.mTimerId);
                continue;
            }
            
            timer.mPassedTime += deltaTime;
            if (timer.mPassedTime >= timer.mRepeatInterval)
            {
                // 做一次触发
                timer.mCallback();
                timer.mPassedTime -= timer.mRepeatInterval;

                if (timer.mRepeatTimes > 0)
                {
                    timer.mRepeatTimes--;
                    if (timer.mRepeatTimes == 0)
                    {
                        // 触发次数结束，将该删除的加入队列
                        timer.mRemoved = true;
                        mRemoveTimers.Add(timer.mTimerId);
                    }
                }
            }
        }
        
        // 清理掉要删除的Timer;
        foreach (var timerID in mRemoveTimers)
        {
            mTimers.Remove(timerID);
        }
        mRemoveTimers.Clear();
    }
}

