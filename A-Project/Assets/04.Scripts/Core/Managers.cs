using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);  //Scene �� ����ǵ� �ı� ���� �ʰ� 
            s_instance = go.GetComponent<Managers>();
        }
    }

    private void Awake()
    {
        Event.Init();
        Sound.Init();
    }

    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    ObjectManager _object = new ObjectManager();
    GameManager _game = new GameManager();
    DataManager _data = new DataManager();
    UIManager _ui = new UIManager();
    SoundManager _sound = new SoundManager();
    EventManager _event = new EventManager();

    public static PoolManager Pool { get { return Instance?._pool; } }
    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static ObjectManager Object { get { return Instance?._object; } }
    public static GameManager Game { get { return Instance?._game; } }
    public static DataManager Data { get { return Instance?._data; } }
    public static EventManager Event { get { return Instance?._event; } }
    public static UIManager UI { get { return Instance?._ui; } }
    public static SoundManager Sound { get { return Instance?._sound; } }
    public static SceneManager Scene { get { return SceneManager.Instance; } }
    public static CoroutineManager CO { get { return CoroutineManager.Instance; } }
    public static TickManager Tick { get { return TickManager.Instance; } }
}
