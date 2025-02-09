using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ƹ�����
/// ����:1.�¼����� 2.����Monoģ��
/// </summary>
public class InputManager : SingletonBase<InputManager>
{
    //�ڹ��캯�������ȫ��Mono����
    public InputManager()
    {
        MonoManager.GetInstance().AddUpdateListener(MyUpdate);
    }
    private bool isOpen = true;

    /// <summary>
    /// �Ƿ���������
    /// </summary>
    public void StartOrEndCheck(bool isOpen)
    {
        this.isOpen = isOpen;
    }

    /// <summary>
    /// ��ⰴ�����º�̧���¼�
    /// �ַ� "keyDown" �� "keyUp" �¼�
    /// </summary>
    /// <param name="key"></param>
    private void CheckKeyCode(KeyCode key)
    {
        //�¼����ķַ������¼�
        if (Input.GetKeyDown(key))
            EventCenter.GetInstance().EventTrigger<KeyCode>("keyDown", key);

        //�¼����ķַ�̧���¼�
        if (Input.GetKeyUp(key))
            EventCenter.GetInstance().EventTrigger<KeyCode>("keyUp", key);
    }
    
    //�����������֡����
    void MyUpdate()
    {
        //δ����������
        if (!isOpen)
            return;

        /*WASD�����ĵķַ�,���ఴ���ַ�Ҳ����*/
        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.D);
    }

}
