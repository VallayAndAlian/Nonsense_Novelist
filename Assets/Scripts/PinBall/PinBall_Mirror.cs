using UnityEngine;

public class PinBall_Mirror : PinBall
{
    protected override void OnCollision()
    {
        // 不做任何特殊处理，继承默认反弹逻辑
    }
}