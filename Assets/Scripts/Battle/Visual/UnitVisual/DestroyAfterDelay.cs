using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [Tooltip("����ǰ���ӳ�ʱ�䣨�룩")]
    public float delay=2f;
    void Start()
    {
        if (gameObject != null)
        {
            Destroy(gameObject, delay);
        }
    }
}