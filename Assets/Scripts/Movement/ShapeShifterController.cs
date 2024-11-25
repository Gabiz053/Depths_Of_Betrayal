using UnityEngine;

public class ShapeShifterController : MonoBehaviour
{
    public GameObject humanSkin;   // Referencia a la skin del humano
    public GameObject monsterSkin; // Referencia a la skin del monstruo

    public float speed;       // Velocidad de movimiento

    private CharacterController characterController; // Controlador de personaje
    private bool isMonster = false; // Estado inicial: forma humana

    void Start()
    {
        // Obtener el componente CharacterController
        characterController = GetComponent<CharacterController>();

        // Activar la skin inicial
        humanSkin.SetActive(true);
        monsterSkin.SetActive(false);
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
            humanSkin.SetActive(!isMonster);
            monsterSkin.SetActive(isMonster);

            // Ajustar las propiedades del CharacterController
            if (isMonster)
            {
                characterController.height = 3.0f;  // Tamaño para monstruo
                characterController.center = new Vector3(0, 1.5f, 0); // Centrado para monstruo
                speed = 3f; // Reducir la velocidad del monstruo
            }
            else
            {
                characterController.height = 2.0f;  // Tamaño para humano
                characterController.center = new Vector3(0, 1.0f, 0); // Centrado para humano
                speed = 5f; // Velocidad normal para humano
            }
        }
    }
}


