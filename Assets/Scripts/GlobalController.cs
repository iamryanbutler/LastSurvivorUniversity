using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    public static GlobalController CurrentInstance;

    public SceneLoader SceneLoader;
    public PlayerInfoStorage PlayerInfoStorage = new PlayerInfoStorage();
    public TypeWriterQueue TypeWriterQueue = new TypeWriterQueue();

    void Awake()
    {
        if (CurrentInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            CurrentInstance = this;
        }
        else if (CurrentInstance != this)
            Destroy(gameObject);
    }
}
