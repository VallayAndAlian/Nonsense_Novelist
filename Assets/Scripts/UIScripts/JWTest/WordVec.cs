
using UnityEngine;
/// <summary>
/// ���ڴ���С������
/// </summary>
public class WordVec : MonoBehaviour
{
    Rigidbody2D rigid;
    public float velocity=2f;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rigid.velocity.magnitude < velocity)
        {
            rigid.velocity = new Vector2(0, 0);
            
        }
    }
}
