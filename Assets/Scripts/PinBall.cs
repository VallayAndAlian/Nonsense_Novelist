
using UnityEngine;
public enum ShootType
{
    None=0,
    Split=1,//分裂
    Activate=2,//激活
    spread=3,//传播
    Alpha=4//穿透
}

public abstract class PinBall : BattleObject
{
    public class Ball
    {
        public Transform transform;
        public Vector2 velocity;
        public LayerMask collisionLayer;
        public LineRenderer trajectoryLine;
        public WordTable.Data wordData;
    }

    public Ball mBall;

    public override void Update(float deltaSec)
    {
        if (!IsTickEnable) return; 

        mBall.transform.position += (Vector3)mBall.velocity * deltaSec;

        RaycastHit2D hit = Physics2D.Raycast(
            mBall.transform.position,
            mBall.velocity.normalized,
            mBall.velocity.magnitude * deltaSec,
            mBall.collisionLayer
        );

        if (hit.collider != null)
        {
            HandleCollision(hit);
        }
    }

    protected void HandleCollision(RaycastHit2D hit)
    {
        Vector2 normal = hit.normal;
        mBall.velocity = Vector2.Reflect(mBall.velocity, normal);
        PlayEffect(mBall.transform.position);
        OnCollision();
    }

    protected virtual void OnCollision() { }

    private void PlayEffect(Vector2 position)
    {

    }

    public void PredictTrajectory(Vector2 startPosition, Vector2 startVelocity, int maxSteps)
    {
        Vector2 currentPosition = startPosition;
        Vector2 currentVelocity = startVelocity;
        mBall.trajectoryLine.positionCount = maxSteps;

        for (int i = 0; i < maxSteps; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                currentPosition,
                currentVelocity.normalized,
                currentVelocity.magnitude * Time.deltaTime,
                mBall.collisionLayer
            );

            if (hit.collider != null)
            {
                mBall.trajectoryLine.SetPosition(i, hit.point);
                currentVelocity = Vector2.Reflect(currentVelocity, hit.normal);
                currentPosition = hit.point + currentVelocity * Time.deltaTime;
            }
            else
            {
                currentPosition += currentVelocity * Time.deltaTime;
                mBall.trajectoryLine.SetPosition(i, currentPosition);
            }
        }
    }
}
