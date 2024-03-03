using UnityEngine;
/// <summary>
/// 24.1.24�߻�debug���ܲ���boss
/// </summary>
public class BossCreate : MonoBehaviour
{
    public GameObject huaiYiZhuYi;
    public void CreateTestBoss()
    {
        GameObject boss = Instantiate(huaiYiZhuYi);
        boss.transform.SetParent(GameObject.Find("Circle5.5").transform);
        boss.transform.localPosition = Vector3.zero;


        //���ɵ���
        boss.GetComponentInChildren<AI.MyState0>().enabled = true;
        boss.GetComponent<AbstractCharacter>().enabled = true;
        boss.gameObject.AddComponent(typeof(AfterStart));
        Destroy(boss.GetComponent<CharacterMouseDrag>());

        //����һ�����Ŀ�꣬ʹ����빥��״̬
        IAttackRange attackRange = new SingleSelector();

        //��һ��Խ����
        AbstractCharacter[] a = attackRange.CaculateRange(100, boss.GetComponent<AbstractCharacter>().situation, NeedCampEnum.all,false);
        boss.GetComponentInChildren<AI.MyState0>().aim.Add(a[0]);
    }
    public void ClosePanel()
    {
        this.transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1;

    }
}
