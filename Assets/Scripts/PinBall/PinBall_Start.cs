public class PinBall_Start : PinBall
{
        public PinBall_Start(WordTable.Data data)
    {
        mBall.wordData=data;
    }
    protected override void OnCollision()
    {
        // 起兴的效果：在获得本词条和一局战斗开始时触发效果。
        TriggerStartEffect();
    }

    private void TriggerStartEffect()
    {
        // 实现开局触发的机制。
       
    }
}