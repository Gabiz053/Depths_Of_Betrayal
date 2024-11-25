using UnityEngine;



public class Character : MonoBehaviour
{
    public const int MAX_HEALTH = 2;
    public const int  HEALTH_REDUCTION = 1;
    public string characterName;
    public Inventory Inventory { get; set; } = new Inventory();

    public int Health { get; set; } = MAX_HEALTH;
    public float distancia = 1.5f;

    //Objeto que hace referencia al Texto de Interaccion
    public GameObject TextDetect;
    //Layer para objetos recolectables
    private LayerMask collectableLayer;

    GameObject ultimoReconocido = null;


    public virtual void Start(){

         // Encuentra el Canvas de Press E
        TextDetect = GameObject.Find("ObjetoTextoPressE");
        //Encuentra la Layer Collectable
        collectableLayer = LayerMask.GetMask("Collectable");

        //desactiva el texto de interaccion
        TextDetect.SetActive(false);
    }
    

    public virtual void Update()
    {
        RaycastHit hit;

        // Desplazar el origen del raycast para que esté en el centro del personaje (usando el centro del collider)
        Vector3 rayOrigin = transform.position + Vector3.up * 1.0f; // Ajusta la altura según el tamaño del personaje

        // Dibuja el raycast para depuración desde el centro del personaje
        Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.forward) * distancia, Color.red);

        // Realizar el Raycast desde el centro del personaje (ajustado hacia arriba)
        if (Physics.Raycast(rayOrigin, transform.TransformDirection(Vector3.forward), out hit, distancia, collectableLayer)) {
            deselect();
            selectedObject(hit.transform);

            if (Input.GetKeyDown(KeyCode.E)) {
                hit.collider.transform.GetComponent<CollectableObjetc>().pickedUp(this);
            }
        } else {
            deselect();
        }
    }



    void selectedObject(Transform transform){
        transform.GetComponent<MeshRenderer>().material.color = Color.green;
        TextDetect.SetActive(true);
        ultimoReconocido = transform.gameObject;
    }

    void deselect(){
        if (ultimoReconocido){
            ultimoReconocido.GetComponent<Renderer>().material.color = Color.magenta;
            ultimoReconocido = null;
            TextDetect.SetActive(false);
        }
    }
}

