using UnityEngine;
using UnityEngine.EventSystems;

public class UnitCandidateOperator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler,
    IDragHandler, IEndDragHandler
{
    [Header("控制物体")] 
    public GameObject mTarget;

    protected RectTransform mRectTransform;
    protected CanvasGroup mCanvasGroup;
    protected Canvas mCanvas;
    protected Vector2 mOriginalLocalPointerPosition;
    protected Vector2 mOriginalPanelPosition;

    protected int mSpawnKind = 0;
    protected BattleBase mBattle = null;

    public void Setup(int unitKind)
    {
        mSpawnKind = unitKind;

        mBattle = BattleRunner.Battle;
    }

    void Start()
    {
        if (mTarget == null)
        {
            mTarget = transform.gameObject;
        }

        mRectTransform = mTarget.GetComponent<RectTransform>();
        mCanvas = mTarget.GetComponentInParent<Canvas>();
        mCanvasGroup = mTarget.GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mOriginalPanelPosition = mRectTransform.anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mCanvas.transform as RectTransform, eventData.position,
            eventData.pressEventCamera, out mOriginalLocalPointerPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (mCanvas == null)
            return;

        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mCanvas.transform as RectTransform,
                eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            Vector2 offsetToOriginal = localPointerPosition - mOriginalLocalPointerPosition;
            mRectTransform.anchoredPosition = mOriginalPanelPosition + offsetToOriginal;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        mRectTransform.anchoredPosition = mOriginalPanelPosition;

        var unit = mBattle.Stage.SpawnUnit(mSpawnKind, UnitSlotType.BackSeat | UnitSlotType.FrontSeat, ClientUtils.GetMouseWorldPosition());
        if (unit != null)
        {
            gameObject.SetActive(false);
            mTarget.SetActive(false);
        }
    }
}
