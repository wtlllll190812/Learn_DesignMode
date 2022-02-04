using UnityEngine;
using DesignMode.Singleton;
using UnityEngine.Serialization;

public class GameManager :Singleton<GameManager>
{
    public int gameTest;

    public void SendMessage()
    {
        Debug.Log("sdsd");
    }
}
