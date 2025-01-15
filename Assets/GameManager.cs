using Mono.CSharp;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public Menu1 menu; // Pasarle desde el inspector el canvas_manager (carpeta UI)

    [SerializeField]
    private static NetworkVariable<int> deaths = new NetworkVariable<int>(0);

    [SerializeField]
    private static NetworkVariable<int> crystals = new NetworkVariable<int>(19);


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

    void Start()
    {
        deaths.OnValueChanged += checkDeaths;
        crystals.OnValueChanged += checkCrystals;
    }

    private void checkDeaths(int oldInt, int newInt)
    {
        if (newInt >= NetworkManager.Singleton.ConnectedClients.Count - 1)
        {
            MonsterWinServerRpc();
        }
    }

    private void checkCrystals(int oldInt, int newInt)
    {
        if (newInt >= 20)
        {
            DiverWinServerRpc();
        }
    }

    public static void notifyDie()
    {
        deaths.Value++;
    }

    public static void notifyCollect(int amount)
    {
        crystals.Value += amount;
    }

    [ServerRpc]
    public void DiverWinServerRpc()
    {
        //NetworkManager.Singleton.Shutdown();

        //PANTALLA MUESTRA GANAN DIVERS
        menu.MostrarMenuGameOverPlayerWins();
    }

    [ServerRpc]
    public void MonsterWinServerRpc()
    {
        //NetworkManager.Singleton.Shutdown();

        //PANTALLA MUESTRA GANA MONSTRUO
        menu.MostrarMenuGameOverMonsterWins();
    }
}
