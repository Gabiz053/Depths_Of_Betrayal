using UnityEngine;
using Unity.Netcode;

public class MonsterCharacter : NetworkBehaviour
{

    [SerializeField]
    private const float DISTANCIA = 3.5f;


    [SerializeField]
    private NetworkVariable<int> Kills = new NetworkVariable<int>();


    public void Start()
    {

    }

    public void Update()
    {

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
                    playerhit.UpdatePlayerHealthServerRpc(-50);
                }
            }
        }
    }

    private RaycastHit getRayCast(int layerMask)
    {
        RaycastHit hit;


        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * DISTANCIA, Color.red);

        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, DISTANCIA, layerMask);

        return hit;
    }
}
