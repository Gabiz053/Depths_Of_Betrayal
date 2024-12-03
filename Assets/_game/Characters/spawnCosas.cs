using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class spawnCosas : NetworkBehaviour
{

    [SerializeField] private Transform spawnedObjectPrefab;
    private Transform spawnedObjectTransform;

    // Update is called once per frame
    private void Update()
    {
        // SOLO SE PUEDE EJECUTAR DESDE EL SERVER
        // si se quiere ejecutar desde el cliente, se debe hacer con los serverSRP esos
        if (!IsOwner || !IsSpawned) return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            spawnedObjectTransform = Instantiate(spawnedObjectPrefab);
            spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Destroy(spawnedObjectTransform.gameObject);
        }
    }
}
