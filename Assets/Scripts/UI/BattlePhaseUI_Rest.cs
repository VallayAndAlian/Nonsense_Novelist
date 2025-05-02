using UnityEngine;
using UnityEngine.UI;

public class BattlePhaseUI_Rest : BattleUI
{
    protected Button startButton;
    public Transform mCharaPos = null;
    public GameObject mUnitCandiTemp = null;

    public BattlePhaseUI_Rest()
    {

    }

    protected override void CreateUIPanel()
    {
        mUIPanel = AssetManager.Create<GameObject>("UI/Battle/phase_rest");
        mUnitCandiTemp = AssetManager.Load<GameObject>("UI/Battle/UnitCandidate");
    }

    public override void Init()
    {
        base.Init();

        if (startButton == null)
        {
            startButton = UIPanel.transform.Find("Panel/startBtn").GetComponent<Button>();
            startButton.onClick.AddListener(OnClickStartButton);
        }

        if (mCharaPos == null)
        {
            mCharaPos = GameObject.Find("Panel/charaPos").transform;
        }

        ShuffleCharacter(4);
    }

    public void OnClickStartButton()
    {
        Battle.BattlePhase.EnterNextStage();
    }

    public override void Update(float deltaTime)
    {

    }

    public void RefreshSlider(float amount)
    {
    }

    public void ShuffleCharacter(int count)
    {
        ClearCandidates();
        
        var unitList =  Battle.UnitDeck.ShuffleUnit(count);

        for (int i = 0; i < unitList.Count; i++)
        {
            PutToSlot(unitList[i], i);
        }
    }

    public void CreateCharacter(int kind)
    {
        ClearCandidates();
        
        PutToSlot(kind, 1);
    }

    public void PutToSlot(int kind, int i)
    {
        var data = BattleUnitTable.Find(kind);
        if (data == null)
            return;
        
        var asset = AssetManager.Load<BattleUnitSO>("SO/BattleUnit", data.mAsset);

        GameObject candiObj = null;
        GameObject temp = asset.uiPrefab;
        if (asset.uiPrefab != null)
        {
            candiObj = Object.Instantiate(asset.uiPrefab, mCharaPos.GetChild(i), false);
        }
        else
        {
            candiObj = Object.Instantiate(mUnitCandiTemp, mCharaPos.GetChild(i), false);
            candiObj.transform.Find("Mask").GetComponent<Image>().sprite = asset.sprite;
            candiObj.transform.Find("Icon").GetComponent<Image>().sprite = asset.sprite;
        }
        
        candiObj.GetComponent<UnitCandidateOperator>().Setup(kind);
    }

    public void ClearCandidates()
    {
        for (int i = 0; i < mCharaPos.childCount; i++)
        {
            var slot = mCharaPos.GetChild(i);
            for (int j = slot.childCount - 1; j >= 0; j--)
            {
                Object.Destroy(slot.GetChild(j).gameObject);
            }
        }

    }
}