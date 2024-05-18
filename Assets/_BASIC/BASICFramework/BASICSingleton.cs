using UnityEditor;
using UnityEngine;

public class BASICSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (Application.isPlaying)
                Destroy(this.gameObject);
        }
    }

}

#if UNITY_EDITOR
public class BASICEditorSingleton<T> : EditorWindow where T : EditorWindow
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GetWindow<T>();
            }

            return _instance;
        }
    }

    private void OnEnable()
    {
        if (_instance != null && _instance != this)
        {
            Close();
        }
        else
        {
            _instance = this as T;
        }
    }
}
#endif 

public class BASICNODSingleton<T> where T : new()
{
    public static T _instance;

    public static T Instance
    {
        get
        {
            _instance ??= new T();
            return _instance;
        }
    }

    public BASICNODSingleton() { }
}