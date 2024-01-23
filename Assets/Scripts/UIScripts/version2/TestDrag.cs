using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
///<summary>
///�����ק
///</summary>
class TestDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    /// <summary>ͼ��λ��</summary>
    private RectTransform rectTrans;
    /// <summary>����������λ��</summary>
    private Transform wordPanel;
    /// <summary>����������λ��</summary>
    private Transform testPanel;
    /// <summary>CanvasGroup���</summary>
    private CanvasGroup canvasGroup;
    public Transform originPos;

    private void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    /// <summary>
    /// ��ʼ��ק
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.5f;
    }
    /// <summary>
    /// ������ק
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        //�������λ��ƫ��
        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.name == "combatCanvas")
            {
                rectTrans.anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
        }
    }
    /// <summary>
    /// ��ק����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        //��δʹ�ã���ص����λ��
        if (rectTrans != null)
        {
            //FindGrid();
            this.transform.position = originPos.position;
        }
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit != null)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Character"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
        this.transform.position = originPos.position;
    }
}
