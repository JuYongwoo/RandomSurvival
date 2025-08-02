using UnityEngine;

public class ManagerObject : MonoBehaviour
{
    static public ManagerObject instance;

    static public AudioManager am = new AudioManager();
    static public InputManager input = new InputManager();
    static public PoolManager pool = new PoolManager();
    static public ResourceManager resource = new ResourceManager();
    static public PlayerStatObject playerStatObj = new PlayerStatObject();

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        playerStatObj.OnAwake();
        am.onAwake();
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
        input.Clear();
        pool.Clear();
    }
}
