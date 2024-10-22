using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolConfigData :MonoSingleton<PoolConfigData>
{

    public static int CHARA1_CHARAID_MAX = 100;//1-100=½ÇÉ«
    public static int CHARA2_MONSTERID_MAX = 200;//101-200=¹ÖÎï
    public static int CHARA3_BOSSID_MAX = 300;//201-300=boss
    public static int CHARA4_SERVERID_MAX = 400;//301-400=Ëæ´Ó
    public PoolConfig so;
    override public void Awake()
    {
        base.Awake();

        so = ResMgr.GetInstance().Load<PoolConfig>("PoolConfig");


    }


}
