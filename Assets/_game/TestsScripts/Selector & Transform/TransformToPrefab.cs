using System.Linq;
using System.Transactions;
using Unity.Netcode;
using UnityEngine;

public class TransformToPrefab : NetworkBehaviour
{
    public GameObject[] prefabs;  // Referencia al prefab

    void Start()
    {
        // Verifica si el prefab está asignado
        int randomIndex = Random.Range(0, prefabs.Count());

        
        // Instancia el prefab en la posición del objeto vacío
        Instantiate(prefabs[randomIndex], transform.position, transform.rotation);
        prefabs[randomIndex].SetActive(true);

        // Destruye el objeto vacío después de crear el prefab
        Destroy(gameObject);
    }
}