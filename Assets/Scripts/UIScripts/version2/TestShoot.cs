using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class TestShoot : MonoBehaviour
{
    /// <summary>����λ��</summary>
    private GameObject gang;
    /// <summary>����</summary>
    public GameObject bullet;
    /// <summary>����</summary>
    private GameObject bulletInstance;
    /// <summary>���������ĸ�����</summary>
    private Transform afterShootTF;
    /// <summary>��ǰ����</summary>
    [SerializeField]
    private float crtForce = 0;
    /// <summary>��С��</summary>
    public float minForce = 0;
    /// <summary>�����</summary>
    public float maxForce = 200;
    /// <summary>�����ٶ�</summary>

    public float forceSpeed = 80;
    /// <summary>������ֵ</summary>
    public float forceAmount = 1;
    /// <summary>���޷���</summary>
    private bool fired = false;
    /// <summary>����Slider</summary>
    private Slider aimSlider;
    /// <summary>�洢WordCollisionShoot�Ĵ�������</summary>
    public static AbstractWord0 abs;
    /// <summary>�ֶ���������Ϣ�� </summary>
    private WordInformation information;
    private Type clickWord;
    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "CombatTest")
        {
            gang = GameObject.Find("shooter").transform.GetChild(0).gameObject;
            information = GameObject.Find("combatCanvas").GetComponentInChildren<WordInformation>();
            afterShootTF = GameObject.Find("AfterShootTF").transform;
            aimSlider = GameObject.Find("combatCanvas").transform.GetChild(2).GetComponent<Slider>();

        }
    }
    private void Update()
    {        
            if (CreateOneCharacter.isTwoSides)
            {
                aimSlider.value = 0; // ����slider��ֵ

                if (crtForce >= maxForce && !fired)// ���������ֵ
                {
                    crtForce = maxForce;
                }

                if (Input.GetButtonDown("Fire2"))
                {
                    crtForce = minForce; // �������Ĵ�С
                    fired = false; // ���ÿ���״̬Ϊδ����                    
                }
                else if (Input.GetButton("Fire2") && !fired)// һֱ����
                {
                    crtForce += forceSpeed * Time.deltaTime; // ����
                    aimSlider.value = crtForce / maxForce; // ����slider��ֵ
                }
                else if (Input.GetButtonUp("Fire2") && !fired)
                {
                    ShootWordBullet();
                }
            }        
    }
    public void ClickWord()
    {
        var buttonSelf = EventSystem.current.currentSelectedGameObject;
        clickWord = buttonSelf.GetComponent<AbstractWord0>().GetType();
    }
    /// <summary>
    /// ��һ������С��׼��[���԰汾��]
    /// </summary>
    public void NextWordReady()
    {
        bulletInstance = Instantiate(bullet);

        //Ԥ�������
        bulletInstance.transform.SetParent(gang.transform);
        bulletInstance.transform.localPosition = Vector3.zero;
        bulletInstance.transform.localEulerAngles = Vector3.zero;
        bulletInstance.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
        var buttonSelf = EventSystem.current.currentSelectedGameObject;
        abs = GameObject.Find("WordCollisionShoot").GetComponent<WordCollisionShoot>().absWord = bulletInstance.AddComponent(buttonSelf.GetComponent<AbstractWord0>().GetType()) as AbstractWord0;
        //if (list != null) list.Clear();
        //list.Add(buttonSelf.GetComponent<AbstractWord0>().GetType());
        foreach (var _col in (bulletInstance.GetComponentsInChildren<WordCollisionShoot>()))
            _col.absWord = abs;
        information.ChangeInformation(abs);

    }
    public void ReadyWordBullet()
    {
        bulletInstance = Instantiate(bullet);

        //Ԥ�������
        bulletInstance.transform.SetParent(gang.transform);
        bulletInstance.transform.localPosition = Vector3.zero;
        bulletInstance.transform.localEulerAngles = Vector3.zero;
        bulletInstance.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
        //��С�����Ӵ�������
        abs = GameObject.Find("WordCollisionShoot").GetComponent<WordCollisionShoot>().absWord = bulletInstance.AddComponent(AllSkills.CreateSkillWord()) as AbstractWord0;
        foreach (var _col in (bulletInstance.GetComponentsInChildren<WordCollisionShoot>()))
            _col.absWord = abs;
        information.ChangeInformation(abs);

    }
    /// <summary>
    /// ��������ʵ��
    /// </summary>
    void ShootWordBullet()
    {
        fired = true; // ���ÿ���״̬Ϊ�ѿ���
        //���������һ����ʼ����
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletInstance.transform.up * crtForce * forceAmount);
        bulletInstance.transform.SetParent(afterShootTF);
        bulletInstance.GetComponent<Collider2D>().isTrigger = false;
        bulletInstance.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        ReadyWordBullet();
        //NextWordReady();
        DestroyWordBullet();
    }
    private void DestroyWordBullet()
    {
        if (afterShootTF.childCount > 10)
        {
            Destroy(afterShootTF.GetChild(0).gameObject);
        }
        else if (gang.transform.childCount > 1)
        {
            Destroy(gang.transform.GetChild(0).gameObject);
        }
    }
}
