using Unity.Netcode;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager instance;

    //obtiene Singleton
    private void Awake()
    {
        if (instance != this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    [ServerRpc]
    public void DiverWinServerRpc()
    {
        //END GAME
        
    }

    [ServerRpc]
    public void MonsterWinServerRpc()
    {
        //END GAME

    }
}
