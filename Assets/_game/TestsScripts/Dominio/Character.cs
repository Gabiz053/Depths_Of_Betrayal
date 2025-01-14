using System.Collections.Generic;
using Mono.CSharp;
using Unity.Netcode;
using UnityEngine;



public class Character : NetworkBehaviour
{
    
    [SerializeField]
    private const float DISTANCIA = 1.5f;

    [SerializeField]
    private NetworkVariable<int> PlayerHealth = new NetworkVariable<int>(100);

    [SerializeField]
    private List<Crystal> Crystals = new List<Crystal>();

    [SerializeField]
    private GameObject TextInteractCollectable;

    [SerializeField]
    private GameObject TextInteractPlatform;
    

    GameObject collectableSelect = null;

    void Start(){

        //desactiva el texto de interaccion
        TextInteractCollectable.SetActive(false);
        TextInteractPlatform.SetActive(false);

        PlayerHealth.OnValueChanged += checkHealth;

    }

    private void checkHealth(int oldHealth, int newHealth)
    {
        if (newHealth <= 0 )
        {
            //INDICAR AL JUEGO QUE ME HE MUERTO
            GameManager.notifyDie();

            //INDICAR A LOS CRISTALES QUE VUELVAN A SU SITIO
            foreach (Crystal c in Crystals)
            {
                c.comeback();
            }

            //TRANSFORMARME EN TORTUGA
        }
    }
    

    void Update()
    {
        if (IsClient && IsOwner)
        {
            CheckCollectable();
            CheckPlatform();
        }
        
    }

    private void CheckCollectable()
    {

        int layerMask = LayerMask.GetMask("Collectable");

        RaycastHit hit = getRayCast(layerMask);

        if (hit.collider != null)
        {
            deselect();
            selectedObject(hit.transform);

            if (Input.GetKeyDown(KeyCode.E))
            {
                var collectable = hit.collider.transform.GetComponent<CollectableObjetc>();
                collectable.pickedUp(this);
                AudioManager.instance.playSFX(AudioManager.instance.crystal);
            }
        } else {deselect();} 
    }

    private void CheckPlatform()
    {
        int layerMask = LayerMask.GetMask("Platform");

        RaycastHit hit = getRayCast(layerMask);

        if (hit.collider != null)
        {
            TextInteractPlatform.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && Crystals.Count > 0)
            {
                var platform = Platform.instance;
                platform.UpdateCrystalCountServerRpc(Crystals.Count);
                Crystals.Clear();
            }


        } else {TextInteractPlatform.SetActive(false);}
    }

    [ServerRpc(RequireOwnership = false)]
    public void UpdatePlayerHealthServerRpc(int amount)
    {
        
        if (PlayerHealth.Value > 0 && PlayerHealth.Value <= 100)
        {
            PlayerHealth.Value += amount;
        }
        
    }

    public void addCrystal(Crystal c){
        Crystals.Add(c);
    }

    void selectedObject(Transform transform){
        transform.GetComponent<MeshRenderer>().material.color = Color.green;
        TextInteractCollectable.SetActive(true);
        collectableSelect = transform.gameObject;
    }

    void deselect()
    {
        if (collectableSelect){
            collectableSelect.GetComponent<Renderer>().material.color = Color.magenta;
            collectableSelect = null;
            TextInteractCollectable.SetActive(false);
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

