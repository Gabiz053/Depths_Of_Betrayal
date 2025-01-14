using Unity.Netcode;
using UnityEngine;

public class ShapeShifterController : NetworkBehaviour
{

    [SerializeField]
    private GameObject diverPrefab;   // Referencia a la skin del humano

    [SerializeField]
    public GameObject alternativePrefab; // Referencia al otro prefab


    private bool isMonster = false; // Estado inicial: forma humana

    void Start()
    {
        // Activar la skin inicial
        diverPrefab.SetActive(true);
        alternativePrefab.SetActive(false);
    }

    void Update()
    {
        HandleTransformation();
    }


    void HandleTransformation()
    {
        // Transformar al presionar la tecla T
        if (Input.GetKeyDown(KeyCode.T))
        {
            isMonster = !isMonster;

            // Alternar las skins
            diverPrefab.SetActive(!isMonster);
            alternativePrefab.SetActive(isMonster);
        }
    }
}


