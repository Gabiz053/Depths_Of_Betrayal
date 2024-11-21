using UnityEngine;



public class Character : MonoBehaviour
{
    public const int MAX_HEALTH = 2;
    public const int  HEALTH_REDUCTION = 1;
    public const float  SPEED = 5.0f;

    public string characterName;
    public Inventory Inventory { get; set; } = new Inventory();

    public int Health { get; set; } = MAX_HEALTH;
    public float Speed { get; set; } = SPEED;

    public float distancia = 1.5f;

    //Objeto que hace referencia al Texto de Interaccion
    public GameObject TextDetect;
    //Layer para objetos recolectables
    public LayerMask collectableLayer;

    GameObject ultimoReconocido = null;


    void Start(){

         // Encuentra el Canvas de Press E
        TextDetect = GameObject.Find("ObjetoTextoPressE");
        //Encuentra la Layer Collectable
        collectableLayer = LayerMask.GetMask("Collectable");

        //desactiva el texto de interaccion
        TextDetect.SetActive(false);
    }
    

    void Update() {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distancia, Color.red);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, collectableLayer)) {
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


    // Method to take damage, can be overridden if needed
    public virtual bool TakeDamage()
    {
        bool dead = false;
        Health -= HEALTH_REDUCTION;
        if (Health <= 0)
        {
            Die();
            dead = true;
        }
        return dead;
    }

    // Basic death function
    protected virtual void Die()
    {
        Debug.Log(characterName + " has died.");
        // Becomes invisible
        gameObject.SetActive(false);
    }

    // Crystal is collected
    public void pickObjetc(CollectableObjetc o)
    {
        Debug.Log(characterName + " has collected a crystal!");
        o.pickedUp(this);
    }

    // Desposit all crystals form the inventory to the platform
    public void DepositCrystals(Platform platform)
    {
        Debug.Log(characterName + " has deposited all crystals!");
        platform.pickUp(Inventory.Cristales);
        Inventory.Cristales.Clear();
    }
}

