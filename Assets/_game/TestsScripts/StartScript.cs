using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
public class StartScript : NetworkBehaviour
{
    private bool clicked = false;
    public GameObject suelo;
    private NetworkVariable<bool> gameStarted = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        gameStarted.OnValueChanged += (bool previousValue, bool newValue) =>
        {
            if (newValue)
            {
                Debug.Log("Game started");
                suelo.SetActive(false);
                //Menu1 llamar a StartGame()
                
            }
        };
    }

    private void Update()
    {
        if (!IsOwner || !IsSpawned) return;

        if (clicked) {
            gameStarted.Value = true;
        }
    }

    public void Click()
    {
        clicked = true;
    }
}
