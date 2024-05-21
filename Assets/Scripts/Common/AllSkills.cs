using System.Collections.Generic;
using UnityEngine;
using System;

///<summary>
///���м��ܾ�̬��
///</summary>
public static class AllSkills
{
    #region ����
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
    /// <summary>��¥�ν�ɫ����</summary>
    public static List<Type> hlmList_chara=new List<Type>();

    /// <summary>����԰���ʴ���</summary>
    public static List<Type> animalList_noun = new List<Type>();
    /// <summary>����԰���ݴʴ���</summary>
    public static List<Type> animalList_adj = new List<Type>();
    /// <summary>����԰���ʴ���</summary>
    public static List<Type> animalList_verb = new List<Type>();
    /// <summary>����԰ȫ������</summary>
    public static List<Type> animalList_all = new List<Type>();
    public static List<Type> animalList_chara = new List<Type>();

    /// <summary>���������ʴ���</summary>
    public static List<Type> humanList_noun = new List<Type>();
    /// <summary>���������ݴʴ���</summary>
    public static List<Type> humanList_adj = new List<Type>();
    /// <summary>�����˶��ʴ���</summary>
    public static List<Type> humanList_verb = new List<Type>();
    /// <summary>������ȫ������</summary>
    public static List<Type> humanList_all = new List<Type>();
    public static List<Type> humanList_chara = new List<Type>();

    /// <summary>ˮ���������ʴ���</summary>
    public static List<Type> crystalList_noun = new List<Type>();
    /// <summary>ˮ���������ݴʴ���</summary>
    public static List<Type> crystalList_adj = new List<Type>();
    /// <summary>ˮ���������ʴ���</summary>
    public static List<Type> crystalList_verb = new List<Type>();
    /// <summary>ˮ������ȫ������</summary>
    public static List<Type> crystalList_all = new List<Type>();
        public static List<Type> crystalList_chara = new List<Type>();

    /// <summary>ɯ�������ʴ���</summary>
    public static List<Type> shaLeMeiList_noun = new List<Type>();
    /// <summary>ɯ�������ݴʴ���</summary>
    public static List<Type> shaLeMeiList_adj = new List<Type>();
    /// <summary>ɯ�������ʴ���</summary>
    public static List<Type> shaLeMeiList_verb = new List<Type>();
    /// <summary>ɯ����ȫ������</summary>
    public static List<Type> shaLeMeiList_all = new List<Type>();
    public static List<Type> shaLeMeiList_chara=new List<Type>();

    /// <summary>���������ʴ���</summary>
    public static List<Type> aiJiShenHuaList_noun = new List<Type>();
    /// <summary>���������ݴʴ���</summary>
    public static List<Type> aiJiShenHuaList_adj = new List<Type>();
    /// <summary>�����񻰶��ʴ���</summary>
    public static List<Type> aiJiShenHuaList_verb = new List<Type>();
    /// <summary>������ȫ������</summary>
    public static List<Type> aiJiShenHuaList_all = new List<Type>();
    public static List<Type> aiJiShenHuaList_chara= new List<Type>();

    /// <summary>���в�ѧ���ʴ���</summary>
    public static List<Type> liuXingBXList_noun = new List<Type>();
    /// <summary>���в�ѧ���ݴʴ���</summary>
    public static List<Type> liuXingBXList_adj = new List<Type>();
    /// <summary>���в�ѧ���ʴ���</summary>
    public static List<Type> liuXingBXList_verb = new List<Type>();
    /// <summary>���в�ѧȫ������</summary>
    public static List<Type> liuXingBXList_all = new List<Type>();
    public static List<Type> liuXingBXList_chara=new List<Type>();

    /// <summary>���ϵ۹����ʴ���</summary>
    public static List<Type> maYiDiGuoList_noun = new List<Type>();
    /// <summary>���ϵ۹����ݴʴ���</summary>
    public static List<Type> maYiDiGuoList_adj = new List<Type>();
    /// <summary>���ϵ۹����ʴ���</summary>
    public static List<Type> maYiDiGuoList_verb = new List<Type>();
    /// <summary>���ϵ۹�ȫ������</summary>
    public static List<Type> maYiDiGuoList_all = new List<Type>();
    public static List <Type> maYiDiGuoList_chara = new List<Type>();
   
    /// <summary>ͨ�����ʴ���</summary>
    public static List<Type> commonList_noun = new List<Type>();
    /// <summary>ͨ�����ݴʴ���</summary>
    public static List<Type> commonList_adj = new List<Type>();
    /// <summary>ͨ�ö��ʴ���</summary>
    public static List<Type> commonList_verb = new List<Type>();
    /// <summary>ͨ��ȫ������</summary>
    public static List<Type> commonList_all = new List<Type>();
    public static List<Type> commonList_chara = new List<Type>();
   
    /// <summary>ս������ȫ������</summary>
    public static List<Type> combatList_all = new List<Type>();
#endregion

    /// <summary>6����ʼ����</summary>
    public static Type[] absWords = new Type[6];

    /// <summary>���Դ���</summary>
    public static List<Type> BadBuff = new List<Type>();
    public static List<Type> GoodBuff = new List<Type>();


    /// <summary>���Դ���</summary>
    public static List<Type> testList1 = new List<Type>();

    //�趨
    /// <summary>ƽӹ��</summary>
    public static List<Type> setting_PingYong = new List<Type>();
    /// <summary>��˼��</summary>
    public static List<Type> setting_QiaoSi = new List<Type>();
    /// <summary>��ſ�</summary>
    public static List<Type> setting_GuiCai = new List<Type>();
    /// <summary>���ؿ�</summary>
    public static List<Type> setting_DuTe = new List<Type>();
    /// <summary>��ɫ��ǩ��</summary>
    public static List<Type> setting_Chara = new List<Type>();

    //����ϡ�ж�
    public static List<Type> Rare_1 = new List<Type>();
    public static List<Type> Rare_2 = new List<Type>();
    public static List<Type> Rare_3 = new List<Type>();
    public static List<Type> Rare_4 = new List<Type>();
    /// <summary>
    /// ��̬���캯��
    /// </summary>
    static AllSkills()
    {
        #region ����
        //��Ӷ��ʴ���
        list_verb.AddRange(new Type[] {   typeof(WritePoem) , typeof(BuryFlower),typeof(WanShua),typeof(ShaYu), typeof(FangFuShu),
            typeof(ShenPan),typeof(QiChongShaDance),typeof(Kiss),typeof(TongPinGongZhen),typeof(TuLingCeShi),
             typeof(GunShoot), typeof(MianYiZengQiang),typeof(ChanLuan),typeof(XuanZhan),typeof(HeartBroken),typeof(Shuai),
            typeof(ToBigger), typeof(BaoZa),typeof(XuanHua)
         //δ����ͨ����,typeof(WenYiChuanBo) , 
          });
        
        //������ݴʴ���W
        list_adj.AddRange(new Type[] {  typeof(ChaFanWuXin),typeof(ShenYouHuanJing),typeof(KeBan),typeof(HunHe),
            typeof(YongJi),  typeof(YouAnQuanGan), typeof(BuXiu),  typeof(FengChan),  typeof(BeiPanDe),typeof(XinShenJiDang),
            typeof(LuanLun),typeof(HunQianMengYing),typeof(QingXi),typeof(JuCaiDe),typeof(FuNengLiangDe),
            typeof(HeWuRan), typeof(RenZao) , typeof(XiaYuDe),typeof(KeSou),/*typeof(ShenHuanFeiYan)*/typeof(GuoMin),
            typeof(GeLi),typeof(NanYiXiaoMieDe),typeof(SheHuiHua), typeof(HunFei),typeof(HaoZhan),typeof(FengLi),
            typeof(QuicklyGrowing),typeof(LuoYingBinFen) ,
             typeof(CuZhuang),typeof(JianRuPanShi), typeof(ZhongDu),typeof(YeSheng),
            //δ����ͨ����
         }) ;
                                                 
        //������ʴ���
        list_noun.AddRange(new Type[] {  typeof(ChaBei),typeof(LengXiangWan),typeof(TongLingBaoyu),
            typeof(ShiWuFengRong),typeof(SheQunFengRong),typeof(RiLunGuaZhui),typeof(herusizhiyan),typeof(XianZhiHead),typeof(LiWu),
            typeof(BaiShuijing) ,typeof(ZiShuiJIng),typeof(HuYanShi) ,typeof(MeiGuiShiYing) ,typeof(Nexus_6Arm),
             typeof(BeiZhiRuDeJiYi),typeof(VolumeProduction) ,typeof(JiShengChong),typeof(EXingZhongLiu),typeof(WaiGuGe), 
            typeof(DuXian), typeof(FuTouAxe),  typeof(HouZiDian),typeof(QiGuaiShiXiang),typeof(BoLiGuaZhui),
             //δ����ͨ����typeof(SheQunFengRong),
        
            //ȱ�ٵ��Ѳ���
        });
        //ȫ��
        list_all.AddRange(list_verb);
        list_all.AddRange(list_adj);
        list_all.AddRange(list_noun);

        #region
        //����¥�Ρ���Ӷ��ʴ���
        hlmList_verb.AddRange(new Type[] { typeof(BuryFlower), typeof(WritePoem)});
        //����¥�Ρ�������ݴʴ���
        hlmList_adj.AddRange(new Type[] { typeof(ChaFanWuXin), typeof(ShenYouHuanJing)});
        //����¥�Ρ�������ʴ���
        hlmList_noun.AddRange(new Type[] { typeof(ChaBei), typeof(LengXiangWan),typeof(TongLingBaoyu) });
        //����¥�Ρ���ӽ�ɫ����
        hlmList_chara.AddRange(new Type[] { typeof(LinDaiYu), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng), typeof(WangXiFeng) });
        //����¥�Ρ�ȫ������
        hlmList_all.AddRange(hlmList_chara);
        hlmList_all.AddRange(hlmList_noun);
        hlmList_all.AddRange(hlmList_verb);
        hlmList_all.AddRange(hlmList_adj);

        //����԰��Ӷ��ʴ���
        animalList_verb.AddRange(new Type[] { typeof(WanShua), typeof(ShaYu) });
        //����԰������ݴʴ���
        animalList_adj.AddRange(new Type[] { typeof(KeBan), typeof(YouAnQuanGan) ,typeof(HunHe),typeof(YongJi)});
        //����԰������ʴ���
        animalList_noun.AddRange(new Type[] { typeof(ShiWuFengRong), typeof(SheQunFengRong) });
        //����԰��ӽ�ɫ����
        animalList_chara.AddRange(new Type[] { typeof(SiYangYuan)/*, typeof(CS_BenJieShiDui), typeof(CS_YiZhiWeiShiQi), typeof(CS_HunYangLong)*/ });
        //����԰ȫ������
        animalList_all.AddRange(animalList_chara);
        animalList_all.AddRange(animalList_noun);
        animalList_all.AddRange(animalList_verb);
        animalList_all.AddRange(animalList_adj);

        //��������Ӷ��ʴ���
        aiJiShenHuaList_verb.AddRange(new Type[] { typeof(FangFuShu), typeof(ShenPan) });
        //������������ݴʴ���
        aiJiShenHuaList_adj.AddRange(new Type[] { typeof(FengChan), typeof(BuXiu), typeof(BeiPanDe)});
        //������������ʴ���
        aiJiShenHuaList_noun.AddRange(new Type[] { typeof(RiLunGuaZhui), typeof(herusizhiyan), });

        aiJiShenHuaList_chara.AddRange(new Type[] { typeof(MuNaiYi),typeof(Anubis)});
        //������ȫ������
        aiJiShenHuaList_all.AddRange(aiJiShenHuaList_verb);
        aiJiShenHuaList_all.AddRange(aiJiShenHuaList_adj);
        aiJiShenHuaList_all.AddRange(aiJiShenHuaList_noun);


        //ɯ������Ӷ��ʴ���
        shaLeMeiList_verb.AddRange(new Type[] { typeof(QiChongShaDance), typeof(Kiss) });
        //ɯ����������ݴʴ���
        shaLeMeiList_adj.AddRange(new Type[] { typeof(XinShenJiDang), typeof(LuanLun), typeof(HunQianMengYing), });
        //ɯ����������ʴ���
        shaLeMeiList_noun.AddRange(new Type[] { typeof(XianZhiHead),typeof(LiWu) });

        shaLeMeiList_chara.AddRange(new Type[] { typeof(ShaLeMei),typeof(ShiLian)});
        //ɯ����ȫ������
        shaLeMeiList_all.AddRange(shaLeMeiList_verb);
        shaLeMeiList_all.AddRange(shaLeMeiList_adj);
        shaLeMeiList_all.AddRange(shaLeMeiList_noun);


        //ˮ��������Ӷ��ʴ���
        crystalList_verb.AddRange(new Type[] { typeof(TongPinGongZhen) });
        //ˮ������������ݴʴ���
        crystalList_adj.AddRange(new Type[] { typeof(QingXi),typeof(FuNengLiangDe),typeof(JuCaiDe) });
        //ˮ������������ʴ���
        crystalList_noun.AddRange(new Type[] { typeof(BaiShuijing), typeof(ZiShuiJIng), typeof(HuYanShi), typeof(MeiGuiShiYing) });
        //ˮ������ȫ������
        crystalList_all.AddRange(crystalList_verb);
        crystalList_all.AddRange(crystalList_adj);
        crystalList_all.AddRange(crystalList_noun);

        //��������Ӷ��ʴ���
        humanList_verb.AddRange(new Type[] { typeof(TuLingCeShi), typeof(GunShoot) });
        //������������ݴʴ���
        humanList_adj.AddRange(new Type[] { typeof(HeWuRan), typeof(RenZao),typeof(XiaYuDe) });
        //������������ʴ���
        humanList_noun.AddRange(new Type[] { typeof(Nexus_6Arm), typeof(VolumeProduction), typeof(BeiZhiRuDeJiYi) });


        humanList_chara.AddRange(new Type[] { typeof(DiKaDe) ,typeof(LongDuanGongSi),typeof(SaiBoFengZi),typeof(FangSheXingWeiCheng),typeof(Boss_Huaiyizhuyi) });
        //������ȫ������
        humanList_all.AddRange(humanList_verb);
        humanList_all.AddRange(humanList_adj);
        humanList_all.AddRange(humanList_noun);

     
        //���в�ѧ��Ӷ��ʴ���
        liuXingBXList_verb.AddRange(new Type[] { /*typeof(WenYiChuanBo),*/typeof(MianYiZengQiang),});
        //���в�ѧ������ݴʴ���
        liuXingBXList_adj.AddRange(new Type[] { /*typeof(ShenHuanFeiYan),*/typeof(GuoMin),typeof(GeLi),typeof(NanYiXiaoMieDe)});
        //���в�ѧ������ʴ���
        liuXingBXList_noun.AddRange(new Type[] { typeof(JiShengChong),typeof(EXingZhongLiu),});
        //���в�ѧȫ������
        liuXingBXList_all.AddRange(liuXingBXList_verb);
        liuXingBXList_all.AddRange(liuXingBXList_adj);
        liuXingBXList_all.AddRange(liuXingBXList_noun);

        //���ϵ۹���Ӷ��ʴ���
        maYiDiGuoList_verb.AddRange(new Type[] {typeof(ChanLuan),typeof(XuanZhan) });
        //���ϵ۹�������ݴʴ���
        maYiDiGuoList_adj.AddRange(new Type[] {typeof(HunFei),typeof(HaoZhan),typeof(SheHuiHua)});
        //���ϵ۹�������ʴ���
        maYiDiGuoList_noun.AddRange(new Type[] { typeof(WaiGuGe),typeof(DuXian),});

        maYiDiGuoList_chara.AddRange(new Type[] { typeof(BeiLuoJi) });
        //���ϵ۹�ȫ������
        maYiDiGuoList_all.AddRange(maYiDiGuoList_verb);
        maYiDiGuoList_all.AddRange(maYiDiGuoList_adj);
        maYiDiGuoList_all.AddRange(maYiDiGuoList_noun);
    
        //ͨ����Ӷ��ʴ���
        commonList_verb.AddRange(new Type[] { typeof(Shuai),typeof(HeartBroken),typeof(ToBigger),typeof(BaoZa),typeof(XuanHua)});
        //ͨ��������ݴʴ���
        commonList_adj.AddRange(new Type[] {typeof(FengLi), typeof(QuicklyGrowing), typeof(LuoYingBinFen),typeof(CuZhuang),
            typeof(JianRuPanShi),typeof(ZhongDu),typeof(YeSheng)});
        //ͨ��������ʴ���
        commonList_noun.AddRange(new Type[] { typeof(FuTouAxe),typeof(HouZiDian),typeof(QiGuaiShiXiang),typeof(BoLiGuaZhui)});

        commonList_chara.AddRange(new Type[] { typeof(Rat),typeof(PianJian),typeof(JingCha) });
        //ͨ��ȫ������
        commonList_all.AddRange(commonList_verb);
        commonList_all.AddRange(commonList_adj);
        commonList_all.AddRange(commonList_noun);


        /// <summary>ս������ȫ������</summary>
        combatList_all.AddRange(shaLeMeiList_all);

        //ϡ�ж�1�Ĵ���
        Rare_1.AddRange(new Type[] {typeof(ChaBei), typeof(LiWu), typeof(BaiShuijing), typeof(ZiShuiJIng), typeof(BeiZhiRuDeJiYi),
        typeof(VolumeProduction), typeof(EXingZhongLiu), typeof(WaiGuGe), typeof(FuTouAxe), typeof(HouZiDian), typeof(QiGuaiShiXiang),
        typeof(BoLiGuaZhui), 
        //
        typeof(FuShi), typeof(MianYiZengQiang), typeof(ChanLuan), typeof(HeartBroken), typeof(Shuai), typeof(ToBigger),
            typeof(BaoZa), typeof(XuanHua),
        //
        typeof(ChaFanWuXin), typeof(KeBan), typeof(YongJi), typeof(YouAnQuanGan), typeof(FengChan), typeof(BeiPanDe), typeof(HunQianMengYing), 
            typeof(RenZao), typeof(XiaYuDe), typeof(GeLi), typeof(HaoZhan), typeof(FengLi), typeof(QuicklyGrowing), typeof(LuoYingBinFen),
            typeof(CuZhuang), typeof(JianRuPanShi), typeof(YeSheng )
        });

        Rare_2.AddRange(new Type[] {typeof(LengXiangWan), typeof(SheQunFengRong), typeof(HuYanShi), typeof(MeiGuiShiYing), 
            typeof(JiShengChong),
        //
        typeof(ShaYu), typeof(FangFuShu), typeof(Kiss), typeof(GunShoot),
        //
        typeof(ShenYouHuanJing), typeof(HunHe), typeof(BuXiu), typeof(XinShenJiDang), typeof(LuanLun), typeof(QingXi), 
            typeof(JuCaiDe), typeof(FuNengLiangDe), typeof(HeWuRan), typeof(KeSou), typeof(GuoMin), typeof(HunFei), typeof(ZhongDu)
        });

        Rare_3.AddRange(new Type[] {typeof(ShiWuFengRong), typeof(RiLunGuaZhui), typeof(herusizhiyan), typeof(XianZhiHead), typeof(DuXian),
        //
        typeof(XuanZhan), /*typeof(WenYiChuanBo),*/ typeof(TongPinGongZhen), typeof(WanShua),
        //
        typeof(NanYiXiaoMieDe), typeof(SheHuiHua)});

        Rare_4.AddRange(new Type[] { typeof(TongLingBaoyu), typeof(BuryFlower), typeof(ShenPan), typeof(QiChongShaDance),typeof(TuLingCeShi)});


        //�ֶ����Ӻû�����
        BadBuff.AddRange(new Type[] { typeof(DianDao),typeof(FuShi ),typeof(Toxic),typeof(Ill),typeof(XuRuo),/*typeof(PoJia),*/
            typeof(MuNe),typeof(YiLuan),typeof(HanLen),/*typeof(ChiHuan),typeof(LengMo),*/typeof(QingMi),typeof(Dizzy),typeof(Upset)});
        GoodBuff.AddRange(new Type[] { typeof(QiWu), typeof(ReLife),typeof(ChaoFeng),typeof(HuaBan),
            typeof(GongZhen),typeof(GaiZao),typeof(ChongLuan),typeof(HuoRe),
        typeof(ShiQing),typeof(HeShan),typeof(RuiLi),typeof(JianShi),/*typeof(JiSu),*/typeof(ZaiSheng),typeof(KangFen),});
     #endregion

        ///<summary>���Դ���1</summary>
        testList1.AddRange(new Type[] {  typeof(SheQunFengRong),typeof(ChanLuan),typeof(ShiWuFengRong) });

        #endregion

        //�趨
        setting_PingYong.AddRange(new Type[] { typeof(HaiTangChunShui), typeof(JuLingQiShu), typeof(ShuoShuShuoShu) , typeof(ShengSiZaiWo ), typeof(JingXinHeHu) 
        ,typeof(DongWuZhuanJia),typeof(XiXiXiangTong),typeof(MiaoYuLianZhu),typeof(YiShiErNiao),typeof(QianXianMingLiao),typeof(YiEChuanE),typeof(ShiDaLiChen),typeof(ChaoPinChongJi)
        ,typeof(YiYiDaiLao),typeof(JiuHouYuXing),typeof(YinJiuLeShen),typeof(CaiSIQuanYong),typeof(ZangHuaJuHun),typeof(LuoHuaShiJie),typeof(ShengCunJiQiao),typeof(PiMaoZengHou)
        ,typeof(DongWuZhuanJia),typeof(XiuYangShengXi ),typeof(AiZhuanJiuJue),typeof(ZhuRenMingLing),typeof(BingRuGaoHuang),typeof(LuoJingXiaShi),typeof(ZhenFuPingWen),typeof(FuFuDeZheng)
        ,typeof(JianDuanYiLiao),typeof(QiangZhiFanLan),typeof(QiaoGuShiSui),typeof(ShouShuZhunBei),typeof(BingFaZheng),typeof(MianYiXiTong),typeof(YiQunZhuQiang),typeof(YiMingDiMing)
        ,});
        setting_QiaoSi.AddRange(new Type[] {typeof(JinYuManTang),typeof(KeJuanZaShui),typeof(SuoDuoMaZhiNv),typeof(YaoHaiZhenCe),typeof(JiNiTaiMei) ,
        typeof(XueShangJiaShuang),typeof(MouDingHouDong),typeof(NengLiangHuanChong),typeof(FengChiDianChe),typeof(XianShouHengChang),typeof(JinJiJiuYuan),typeof(YuanQuGuiHua),typeof(ShiPuYouHua)
        ,typeof(TongShengGOngSi),typeof(YiChanZhuanRang),typeof(WuZhiAiQuan),typeof(QingMiZhiWu),typeof(KuMuFengChun),typeof(QuanBoDuan),typeof(HuaXueJiLe),typeof(XuNiShengMingLi),typeof(ManXingYanZheng)
        ,typeof(YinKeXiaoYing),typeof(DiXiXinHao)});
        setting_GuiCai.AddRange(new Type[] { typeof(ChengShengZhuiJi),typeof(MeiFeiSeWu), typeof(DongWuLeYuan), typeof(CanHeYuSheng), typeof(DongWuLeYuan), typeof(QunQingJIFen), typeof(AiYiFanLan)
        , typeof(CiShengBo), typeof(ZhiHuiHeXin), typeof(FangXueLiaoFa), typeof(ChongFenLiYong)});
        setting_DuTe.AddRange(new Type[] { typeof(RenXingShangCun), typeof(ShenJingWenLuan) });

        setting_Chara.AddRange(new Type[] { typeof(ShuoShuShuoShu), typeof(HaiTangChunShui), typeof(CanHeYuSheng), typeof(JinYuManTang), typeof(DongWuZhuanJia)
        , typeof(ShiPuYouHua), typeof(JuLingQiShu), typeof(ShengSiZaiWo), typeof(SuoDuoMaZhiNv), typeof(LuoJingXiaShi), typeof(QiaoGuShiSui), typeof(KeJuanZaShui)
        , typeof(YaoHaiZhenCe), typeof(JingXinHeHu), typeof(JiNiTaiMei)});
    }

    /// <summary>
    /// ������汾�������������(��������+����+���ݴ�)
    /// </summary>
    /// <returns></returns>
    public static Type CreateSkillWord()
    {
        #region
        

        #endregion

        //�����ã�ָ��ĳ�ֿ��ơ�

        int _number = UnityEngine.Random.Range(0, testList1.Count);

        return testList1[_number];



        ////��ʽ

        //int number = UnityEngine.Random.Range(0, GameMgr.instance.GetNowList().Count);
        //while (testList3.Contains(GameMgr.instance.GetNowList()[number]))
        //{
        //    number = UnityEngine.Random.Range(0, GameMgr.instance.GetNowList().Count);
        //}

        //return GameMgr.instance.GetNowList()[number];
    }
    public static Type AllAdjWords(int i)
    {
        if (i > list_adj.Count) return list_adj[0];
        return list_adj[i];
    }
    public static Type AllNounWords(int i)
    {
        if (i > list_noun.Count) return list_noun[0];
        return list_noun[i];
    }
    public static Type AllVerbWords(int i)
    {
        if (i > list_verb.Count) return list_verb[0];
        return list_verb[i];
    }
    
    public static Type RandomPY()
    {
        int _number = UnityEngine.Random.Range(0, setting_PingYong.Count);

        return setting_PingYong[_number];
    }
    public static Type RandomQS()
    {
        int _number = UnityEngine.Random.Range(0, setting_QiaoSi.Count);

        return setting_QiaoSi[_number];
    }
    public static Type RandomGC()
    {
        int _number = UnityEngine.Random.Range(0, setting_GuiCai.Count);

        return setting_GuiCai[_number];
    }
    public static Type RandomDT()
    {
        int _number = UnityEngine.Random.Range(0, setting_DuTe.Count);

        return setting_DuTe[_number];
    }
    
    public static Type RandomChara()
    {
        int _number = UnityEngine.Random.Range(0, setting_Chara.Count);

        return setting_Chara[_number];
    }

}
