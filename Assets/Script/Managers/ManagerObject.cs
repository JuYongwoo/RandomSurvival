using UnityEngine;

public class ManagerObject : MonoBehaviour
{
    static public ManagerObject instance;

    static public AudioManager am = new AudioManager();
    static public InputManager input = new InputManager();
    static public PoolManager pool = new PoolManager();
    static public ResourceManager resource = new ResourceManager();
    static public StatObject playerStatObj = new StatObject();

    private void Awake()
    {
        //if (instance != null && instance != this)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        instance = this;
        //DontDestroyOnLoad(gameObject);
        playerStatObj.OnAwake();
        am.onAwake();
        input.OnAwake();
    }
    void Start()
    {
        Screen.SetResolution(1600, 900, false);
        pool.Init();

    }

    void Update()
    {
        input.OnUpdate();
    }


    public static void Clear()
    {
        pool.Clear();
    }
}
