using UnityEngine;

#define MAX_HEALTH 2
#define HEALTH_REDUCTION 1
#define SPEED 5.0f
public class Character : MonoBehaviour
{
    public string characterName;
    public Inventory Inventory { get; set; }

    public int Health {get; set;} = MAX_HEALTH;
    public float Speed {get; set} = SPEED;

    // Method for basic movement, which all characters can use
    public virtual void Move(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
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

    private void OnCollisionEnter(Collision collision)
    {
        CollectableObjetc collectable = collision.gameObject.GetComponent<CollectableObjetc>();
        if (collectable != null)
        {
            Debug.Log("Press 'E' to collect the crystal.");
            StartCoroutine(WaitForCollectInput(collectable));
        }

        MonsterCharacter monster = collision.gameObject.GetComponent<MonsterCharacter>();
        if (monster != null)
        {
            Debug.Log("Press 'F' to attack the monster.");
            StartCoroutine(WaitForAttackInput(monster));
        }
    }

    private IEnumerator WaitForCollectInput(CollectableObjetc collectable)
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickObjetc(collectable);
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator WaitForAttackInput(MonsterCharacter monster)
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                monster.Attack(this);
                yield break;
            }
            yield return null;
        }
    }
}

