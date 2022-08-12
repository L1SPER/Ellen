using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private static CanvasManager instance;
    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject gameMenu;
    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
