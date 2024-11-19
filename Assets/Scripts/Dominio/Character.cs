using UnityEngine;



public class Character : MonoBehaviour
{
    public const int MAX_HEALTH = 2;
    public const int  HEALTH_REDUCTION = 1;
    public const float  SPEED = 5.0f;

    public string characterName;
    public Inventory Inventory { get; set; }

    public int Health { get; set; } = MAX_HEALTH;
    public float Speed { get; set; } = SPEED;

    // Method for basic movement, which all characters can use
    public virtual void Move(Vector3 direction)
    {
        transform.Translate(direction * Speed * Time.deltaTime);
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

