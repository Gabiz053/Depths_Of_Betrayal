using UnityEngine;
using System.Collections;
using Unity.Netcode;

public class MonsterCharacter : NetworkBehaviour
{

    [SerializeField]
    private const float DISTANCIA = 1.5f;


    [SerializeField]
    private NetworkVariable<int> Kills = new NetworkVariable<int>();

    public GameObject monsterPrefab;
    public GameObject characterPrefab;

    public bool isTransformed = false;

    
    
    public void Start(){

        monsterPrefab.SetActive(false);
    }
    
    public void Update(){

        if (IsClient && IsOwner)
        {
            CheckAttack();   
        }
    }

    public void CheckAttack()
    {

        int layerMask = LayerMask.GetMask("Player");

        RaycastHit hit = getRayCast(layerMask);

        if (hit.collider != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var playerhit = hit.transform.GetComponent<Character>();
                if (playerhit != null)
                {
                    playerhit.UpdatePlayerHealthServerRpc(-50, hit.transform.GetComponent<NetworkObject>().OwnerClientId);
                }
            }
        } 
    }

    public void Transform()
    {
        if (!isTransformed){

            monsterPrefab.SetActive(true);
            characterPrefab.SetActive(false);

            isTransformed = true;
            
        } else {

            monsterPrefab.SetActive(false);
            characterPrefab.SetActive(true);

            isTransformed = false;
        }
    }

    private RaycastHit getRayCast(int layerMask)
    {
        RaycastHit hit;

        // Desplazar el origen del raycast para que esté en el centro del personaje (usando el centro del collider)
        Vector3 rayOrigin = transform.position + Vector3.up * 1.0f;

        Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * DISTANCIA, Color.red);

        Physics.Raycast(rayOrigin, transform.TransformDirection(Vector3.forward), out hit, DISTANCIA, layerMask);

        return hit;
    }
}
