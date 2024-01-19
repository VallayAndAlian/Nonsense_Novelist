using System.Collections.Generic;
using UnityEngine;
using System;

///<summary>
///���м��ܾ�̬��
///</summary>
public static class AllSkills
{
    /// <summary>ȫ�����ʴ���</summary>
    public static List<Type> list_noun = new List<Type>();
    /// <summary>ȫ�����ݴʴ���</summary>
    public static List<Type> list_adj= new List<Type>();
    /// <summary>ȫ�����ʴ���</summary>
    public static List<Type> list_verb = new List<Type>();
    /// <summary>ȫ������</summary>
    public static List<Type> list_all = new List<Type>();

    /// <summary>��¥�����ʴ���</summary>
    public static List<Type> hlmList_noun = new List<Type>();
    /// <summary>��¥�����ݴʴ���</summary>
    public static List<Type> hlmList_adj = new List<Type>();
    /// <summary>��¥�ζ��ʴ���</summary>
    public static List<Type> hlmList_verb = new List<Type>();
    /// <summary>��¥��ȫ������</summary>
    public static List<Type> hlmList_all = new List<Type>();

    /// <summary>����԰���ʴ���</summary>
    public static List<Type> animalList_noun = new List<Type>();
    /// <summary>����԰���ݴʴ���</summary>
    public static List<Type> animalList_adj = new List<Type>();
    /// <summary>����԰���ʴ���</summary>
    public static List<Type> animalList_verb = new List<Type>();
    /// <summary>����԰ȫ������</summary>
    public static List<Type> animalList_all = new List<Type>();

    /// <summary>���������ʴ���</summary>
    public static List<Type> humanList_noun = new List<Type>();
    /// <summary>���������ݴʴ���</summary>
    public static List<Type> humanList_adj = new List<Type>();
    /// <summary>�����˶��ʴ���</summary>
    public static List<Type> humanList_verb = new List<Type>();
    /// <summary>������ȫ������</summary>
    public static List<Type> humanList_all = new List<Type>();

    /// <summary>ˮ���������ʴ���</summary>
    public static List<Type> crystalList_noun = new List<Type>();
    /// <summary>ˮ���������ݴʴ���</summary>
    public static List<Type> crystalList_adj = new List<Type>();
    /// <summary>ˮ���������ʴ���</summary>
    public static List<Type> crystalList_verb = new List<Type>();
    /// <summary>ˮ������ȫ������</summary>
    public static List<Type> crystalList_all = new List<Type>();

    /// <summary>ɯ�������ʴ���</summary>
    public static List<Type> shaLeMeiList_noun = new List<Type>();
    /// <summary>ɯ�������ݴʴ���</summary>
    public static List<Type> shaLeMeiList_adj = new List<Type>();
    /// <summary>ɯ�������ʴ���</summary>
    public static List<Type> shaLeMeiList_verb = new List<Type>();
    /// <summary>ɯ����ȫ������</summary>
    public static List<Type> shaLeMeiList_all = new List<Type>();
    
    /// <summary>���������ʴ���</summary>
    public static List<Type> aiJiShenHuaList_noun = new List<Type>();
    /// <summary>���������ݴʴ���</summary>
    public static List<Type> aiJiShenHuaList_adj = new List<Type>();
    /// <summary>�����񻰶��ʴ���</summary>
    public static List<Type> aiJiShenHuaList_verb = new List<Type>();
    /// <summary>������ȫ������</summary>
    public static List<Type> aiJiShenHuaList_all = new List<Type>();
   
    /// <summary>���в�ѧ���ʴ���</summary>
    public static List<Type> liuXingBXList_noun = new List<Type>();
    /// <summary>���в�ѧ���ݴʴ���</summary>
    public static List<Type> liuXingBXList_adj = new List<Type>();
    /// <summary>���в�ѧ���ʴ���</summary>
    public static List<Type> liuXingBXList_verb = new List<Type>();
    /// <summary>���в�ѧȫ������</summary>
    public static List<Type> liuXingBXList_all = new List<Type>();
   
    /// <summary>���ϵ۹����ʴ���</summary>
    public static List<Type> maYiDiGuoList_noun = new List<Type>();
    /// <summary>���ϵ۹����ݴʴ���</summary>
    public static List<Type> maYiDiGuoList_adj = new List<Type>();
    /// <summary>���ϵ۹����ʴ���</summary>
    public static List<Type> maYiDiGuoList_verb = new List<Type>();
    /// <summary>���ϵ۹�ȫ������</summary>
    public static List<Type> maYiDiGuoList_all = new List<Type>();
   
    /// <summary>ͨ�����ʴ���</summary>
    public static List<Type> commonList_noun = new List<Type>();
    /// <summary>ͨ�����ݴʴ���</summary>
    public static List<Type> commonList_adj = new List<Type>();
    /// <summary>ͨ�ö��ʴ���</summary>
    public static List<Type> commonList_verb = new List<Type>();
    /// <summary>ͨ��ȫ������</summary>
    public static List<Type> commonList_all = new List<Type>();
   
    /// <summary>ս������ȫ������</summary>
    public static List<Type> combatList_all = new List<Type>();


    /// <summary>6����ʼ����</summary>
    public static Type[] absWords = new Type[6];



    /// <summary>���Դ���</summary>
    public static List<Type> testList1 = new List<Type>();
    public static List<Type> testList2 = new List<Type>();
    public static List<Type> testList3 = new List<Type>();
    /// <summary>
    /// ��̬���캯��
    /// </summary>
    static AllSkills()
    {       
        //��Ӷ��ʴ���
        list_verb.AddRange(new Type[] { typeof(BuryFlower),  typeof(WritePoem) , typeof(HeartBroken),
           typeof(CHOOHShoot_x) ,typeof(Shuai),typeof(QiChongShaDance),typeof(TongPinGongZhen),
        typeof(BaoZa), typeof(FangFuShu), typeof(GunShoot), typeof(Kiss),typeof(MianYiZengQiang),
        typeof(ShaYu), typeof(ToBigger), typeof(TuLingCeShi), typeof(WanShua), typeof(WenYiChuanBo)});
        //������ݴʴ���
        list_adj.AddRange(new Type[] {  typeof(BuXiu), typeof(ChaFanWuXin), typeof(CuZhuang), typeof(HunFei),
           typeof(FengChan),typeof(FengLi) ,typeof(GuoMin),typeof(HaoZhan) ,typeof(HeWuRan),typeof(HunQianMengYing),
        typeof(JianRuPanShi),typeof(KeBan) ,typeof(LuanLun),typeof(LuoYingBinFen) ,typeof(QingXi),typeof(QuicklyGrowing),
        typeof(RenZao),typeof(SanFaFeiLuoMeng_x) ,typeof(ShenHuanFeiYan),typeof(ShenYouHuanJing) ,typeof(XinShenJiDang),
            typeof(YouAnQuanGan),typeof(ZhongDu),});
        //������ʴ���
        list_noun.AddRange(new Type[] { typeof(WaiGuGe), typeof(JiShengChong), typeof(LengXiangWan), typeof(Nexus_6Arm),
            typeof(ZiShuiJIng) ,typeof(RiLunGuaZhui),typeof(BenJieShiDui),typeof(DuXian),
            typeof(ChaBei),typeof(HuYanShi) ,typeof(BaiShuijing) ,typeof(FuTouAxe),typeof(BoLiGuaZhui),
            typeof(herusizhiyan),typeof(MeiGuiShiYing) ,typeof(BeiZhiRuDeJiYi),typeof(PaintBrush_x),typeof(QiGuaiShiXiang),
            typeof(HouZiDian),typeof(VolumeProduction) ,typeof(XianZhiHead),typeof(YiZhiWeiShiQi) });
        //ȫ��
        list_all.AddRange(list_verb);
        list_all.AddRange(list_adj);
        list_all.AddRange(list_noun);
        

        //����¥�Ρ���Ӷ��ʴ���
        hlmList_verb.AddRange(new Type[] { typeof(BuryFlower), typeof(WritePoem)});
        //����¥�Ρ�������ݴʴ���
        hlmList_adj.AddRange(new Type[] { typeof(ChaFanWuXin), typeof(ShenYouHuanJing)});
        //����¥�Ρ�������ʴ���
        hlmList_noun.AddRange(new Type[] { typeof(ChaBei), typeof(LengXiangWan) });
        //����¥�Ρ�ȫ������
        hlmList_all.AddRange(hlmList_verb);
        hlmList_all.AddRange(hlmList_adj);
        hlmList_all.AddRange(hlmList_noun);

        //����԰��Ӷ��ʴ���
        animalList_verb.AddRange(new Type[] { typeof(WanShua), typeof(ShaYu) });
        //����԰������ݴʴ���
        animalList_adj.AddRange(new Type[] { typeof(KeBan), typeof(YouAnQuanGan) });
        //����԰������ʴ���
        animalList_noun.AddRange(new Type[] { typeof(BenJieShiDui), typeof(YiZhiWeiShiQi) });
        //����԰ȫ������
        animalList_all.AddRange(animalList_verb);
        animalList_all.AddRange(animalList_adj);
        animalList_all.AddRange(animalList_noun);

        //��������Ӷ��ʴ���
        humanList_verb.AddRange(new Type[] { typeof(TuLingCeShi), typeof(GunShoot) });
        //������������ݴʴ���
        humanList_adj.AddRange(new Type[] { typeof(HeWuRan), typeof(RenZao) });
        //������������ʴ���
        humanList_noun.AddRange(new Type[] { typeof(Nexus_6Arm), typeof(VolumeProduction), typeof(BeiZhiRuDeJiYi) });
        //������ȫ������
        humanList_all.AddRange(humanList_verb);
        humanList_all.AddRange(humanList_adj);
        humanList_all.AddRange(humanList_noun);

        //ˮ��������Ӷ��ʴ���
        crystalList_verb.AddRange(new Type[] { typeof(TongPinGongZhen)});
        //ˮ������������ݴʴ���
        crystalList_adj.AddRange(new Type[] { typeof(QingXi)});
        //ˮ������������ʴ���
        crystalList_noun.AddRange(new Type[] { typeof(BaiShuijing), typeof(ZiShuiJIng), typeof(HuYanShi), typeof(MeiGuiShiYing) });
        //ˮ������ȫ������
        crystalList_all.AddRange(crystalList_verb);
        crystalList_all.AddRange(crystalList_adj);
        crystalList_all.AddRange(crystalList_noun);

        //ɯ������Ӷ��ʴ���
        shaLeMeiList_verb.AddRange(new Type[] { typeof(QiChongShaDance),typeof(Kiss)});
        //ɯ����������ݴʴ���
        shaLeMeiList_adj.AddRange(new Type[] { typeof(XinShenJiDang),typeof(LuanLun),typeof(HunQianMengYing),});
        //ɯ����������ʴ���
        shaLeMeiList_noun.AddRange(new Type[] { typeof(XianZhiHead)});
        //ɯ����ȫ������
        shaLeMeiList_all.AddRange(shaLeMeiList_verb);
        shaLeMeiList_all.AddRange(shaLeMeiList_adj);
        shaLeMeiList_all.AddRange(shaLeMeiList_noun);

        //��������Ӷ��ʴ���
        aiJiShenHuaList_verb.AddRange(new Type[] { typeof(FangFuShu)});
        //������������ݴʴ���
        aiJiShenHuaList_adj.AddRange(new Type[] { typeof(FengChan),typeof(BuXiu),});
        //������������ʴ���
        aiJiShenHuaList_noun.AddRange(new Type[] { typeof(RiLunGuaZhui),typeof(herusizhiyan),});
        //������ȫ������
        aiJiShenHuaList_all.AddRange(aiJiShenHuaList_verb);
        aiJiShenHuaList_all.AddRange(aiJiShenHuaList_adj);
        aiJiShenHuaList_all.AddRange(aiJiShenHuaList_noun);

        //���в�ѧ��Ӷ��ʴ���
        liuXingBXList_verb.AddRange(new Type[] { typeof(WenYiChuanBo),typeof(MianYiZengQiang),});
        //���в�ѧ������ݴʴ���
        liuXingBXList_adj.AddRange(new Type[] { typeof(ShenHuanFeiYan),typeof(GuoMin),});
        //���в�ѧ������ʴ���
        liuXingBXList_noun.AddRange(new Type[] { typeof(JiShengChong),typeof(EXingZhongLiu),});
        //���в�ѧȫ������
        liuXingBXList_all.AddRange(liuXingBXList_verb);
        liuXingBXList_all.AddRange(liuXingBXList_adj);
        liuXingBXList_all.AddRange(liuXingBXList_noun);

        //���ϵ۹���Ӷ��ʴ���
        maYiDiGuoList_verb.AddRange(new Type[] { });
        //���ϵ۹�������ݴʴ���
        maYiDiGuoList_adj.AddRange(new Type[] {typeof(HunFei),typeof(HaoZhan),});
        //���ϵ۹�������ʴ���
        maYiDiGuoList_noun.AddRange(new Type[] { typeof(WaiGuGe),typeof(DuXian),});
        //���ϵ۹�ȫ������
        maYiDiGuoList_all.AddRange(maYiDiGuoList_verb);
        maYiDiGuoList_all.AddRange(maYiDiGuoList_adj);
        maYiDiGuoList_all.AddRange(maYiDiGuoList_noun);

        //ͨ����Ӷ��ʴ���
        commonList_verb.AddRange(new Type[] { typeof(Shuai),typeof(HeartBroken),typeof(ToBigger),typeof(BaoZa),});
        //ͨ��������ݴʴ���
        commonList_adj.AddRange(new Type[] {typeof(FengLi), typeof(QuicklyGrowing), typeof(LuoYingBinFen),typeof(CuZhuang),
            typeof(JianRuPanShi),typeof(ZhongDu),});
        //ͨ��������ʴ���
        commonList_noun.AddRange(new Type[] { typeof(FuTouAxe),typeof(HouZiDian),typeof(QiGuaiShiXiang),typeof(BoLiGuaZhui),});
        //ͨ��ȫ������
        commonList_all.AddRange(commonList_verb);
        commonList_all.AddRange(commonList_adj);
        commonList_all.AddRange(commonList_noun);


        /// <summary>ս������ȫ������</summary>
        combatList_all.AddRange(shaLeMeiList_all);


        ///<summary>���Դ���1</summary>
        testList1.AddRange(new Type[] { /*typeof(DuXian) ,typeof(BenJieShiDui)*/typeof(FengChan) });

        ///<summary>����ͨ������2</summary>///
        testList2.AddRange(new Type[] { /*noun*/typeof(BaiShuijing) ,typeof(BeiZhiRuDeJiYi) ,typeof(BenJieShiDui), typeof(BoLiGuaZhui),typeof(ChaBei),
        typeof(EXingZhongLiu),typeof(FuTouAxe),typeof(herusizhiyan),typeof(HouZiDian),typeof(HuYanShi),typeof(MeiGuiShiYing),typeof(Nexus_6Arm),
        typeof(QiGuaiShiXiang),typeof(VolumeProduction),typeof(WaiGuGe),typeof(XianZhiHead),typeof(ZiShuiJIng),
        /*adj*/typeof(ChaFanWuXin),typeof(CuZhuang),typeof(FengChan),typeof(FengLi),typeof(HaoZhan),typeof(HeWuRan),typeof(HunFei),
        typeof(JianRuPanShi),typeof(KeBan),typeof(LuoYingBinFen),typeof(QingXi),typeof(QuicklyGrowing),typeof(RenZao),typeof(ShenHuanFeiYan),
        typeof(ShenYouHuanJing),typeof(XinShenJiDang),typeof(ZhongDu),
        /*verb*/});


        ///<summary>������Ĵ���3</summary>///
        testList3.AddRange(new Type[] {
            /*���Թ������*/ typeof(DuXian), typeof(JiShengChong) ,
       typeof(CHOOHShoot_x),
      /*���Ը���״̬�����*/
           typeof(HunQianMengYing),typeof(LuanLun),
        /*verb*/typeof(BaoZa),typeof(BuryFlower),typeof(Kiss),typeof(QiChongShaDance),typeof(TuLingCeShi),typeof(WenYiChuanBo)});
    }

    /// <summary>
    /// ������汾�������������(��������+����+���ݴ�)
    /// </summary>
    /// <returns></returns>
    public static Type CreateSkillWord()
    {
        //int number = UnityEngine.Random.Range(0, list_all.Count);


        //while (testList3.Contains(list_all[number]))
        //{
        //    number = UnityEngine.Random.Range(0, list_all.Count);
        //}

        //return list_all[number];



        ////�����ã�ָ��ĳ�ֿ��ơ�

        //int _number = UnityEngine.Random.Range(0, testList1.Count);

        //return testList1[_number];

        //int _number = UnityEngine.Random.Range(0, list_all.Count);
        //while (testList3.Contains(list_all[_number]))
        //{
        //    _number = UnityEngine.Random.Range(0, list_all.Count);
        //}
        //return list_all[_number];

        ////��ʽ

        int number = UnityEngine.Random.Range(0, GameMgr.instance.GetCombatStartList().Count);
        while (testList3.Contains(GameMgr.instance.GetCombatStartList()[number]))
        {
            number = UnityEngine.Random.Range(0, GameMgr.instance.GetCombatStartList().Count);
        }

        return GameMgr.instance.GetCombatStartList()[number];
    }

}
