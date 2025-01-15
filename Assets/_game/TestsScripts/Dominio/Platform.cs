using Unity.Netcode;
using UnityEngine;

public class Platform : NetworkBehaviour
{

    [SerializeField]
    private NetworkVariable<int> CrystalCount = new NetworkVariable<int>();


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

    [ServerRpc(RequireOwnership = false)]
    public void UpdateCrystalCountServerRpc(int deposited)
    {
        CrystalCount.Value = +deposited;
        GameManager.notifyCollect(deposited);
    }


}
