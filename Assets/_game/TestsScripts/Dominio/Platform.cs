using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Platform : NetworkBehaviour
{

    [SerializeField]
    private NetworkVariable<int> CrystalCount = new NetworkVariable<int>();

    //Singleton
    public static Platform instance;

    //Obtiene el Singleton
    private void Awake()
    {
        if (instance != this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }


    void Start()
    {
        CrystalCount.OnValueChanged += Count;
    }

    private void Count(int oldList, int newList)
    {
        if (newList >= 20)
        {
            GameManager.instance.DiverWinServerRpc();
        }
    }

    [ServerRpc]
    public void UpdateCrystalCountServerRpc(int deposited)
    {
        CrystalCount.Value =+ deposited;
    }

    
}
