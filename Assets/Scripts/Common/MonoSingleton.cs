/// <summary>
/// Generic Mono singleton.
/// </summary>
using UnityEngine;


/// <summary>
/// ����ģʽ�ĳ�����(Ҫ��֤�ܼ̳У�����˽�й��캯������P488 03-09 4:00�����15:00��
/// </summary>
/// <typeparam name="T">����̳�MonoBehavior������Լ��ΪMonoSingleton����</typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    //�����Ѷ��徲̬˽���ֶΣ����಻��д
    private static T m_Instance = null;

    public static T instance
    {
        get
        {
            //����Ϸ������Ӧ
            if (m_Instance == null)
            {//3.��ƽ׶Σ�����ű�û���������ϣ�Awake�õ�null������Ϊ�˽ű�����ģʽ���ӳ�������Ψһʵ��
                m_Instance = FindObjectOfType(typeof(T)) as T;
                if (m_Instance == null)
                {//4.������Ҳ�Ҳ���������newһ���С�Singleton of...���Ķ��󣬺͹������ϵ� �ű�Ψһʵ��
                    m_Instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                    m_Instance.Init();
                
                
                }

            }
            return m_Instance;
        }
    }
    //1.unity��Ŀ�ص㣺д�ű����������ϣ���Ŀ����ʱ��ϵͳ�����ǰѽű���ʵ������
    /// <summary>
    /// 2.��������ʱ��Awake�����ӳ������ҵ� �ű���Ψһʵ������¼��m_instance�У�
    /// </summary>
    virtual public void Awake()
    {

        if (m_Instance == null)
        {
            m_Instance = this as T;
        }
        else
        {
            if(m_Instance!=this)
            { Destroy(this); }
        }
       // DontDestroyOnLoad(m_Instance.gameObject);
    }

    public virtual void Init() {  }
    //Ӧ�ó����˳����������

    virtual public void OnApplicationQuit()
    {
        m_Instance = null;
    }
}
