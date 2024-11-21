using UnityEngine;
using System.Collections;

public class MonsterCharacter : Character
{
    public GameObject monsterModel;
    public bool isTransformed = false;
    private int cooldown = 10; // 10 second cooldown for the transform ability    
    private bool canTransform = true;
    
    // TODO: crear constructor :)

    // Method to activate monster powers
    public void EnableMonsterPowers()
    {
        Debug.Log(characterName + " has monster powers!");
        // Enable specific abilities
    }

    // Transform ability unique to the monster
    public void ToggleTransform()
    {
        if (!canTransform) return;

        if (!isTransformed)
        {
            isTransformed = true;
            Health *= 2; // Example: Double health when transformed
            Speed *= 2; // Increase speed
            Debug.Log(characterName + " has transformed!");
            monsterModel.SetActive(false); // Hide the character model
        }
        else
        {
            isTransformed = false;
            Health /= 2; // Revert health
            Speed /= 2; // Revert speed
            Debug.Log(characterName + " has reverted back.");
            monsterModel.SetActive(true); // Show the character model
        }
        isTransformed = !isTransformed;
    }

    public void Attack(Character target)
    {
        if (target.TakeDamage()) {
            // Transform back if the target dies
            ToggleTransform();
            StartCoroutine(TransformCooldown());
        }
        //Reduce speed for a short time
        Speed /= 2;
        while (Speed < SPEED)
        {
            Speed += 0.1f;
        }
    }

    private IEnumerator TransformCooldown()
    {
        canTransform = false;
        yield return new WaitForSeconds(cooldown);
        canTransform = true;
    }

    // If the monster collides with another character, attack it
    private void OnCollisionEnter(Collision collision)
    {
        Character target = collision.gameObject.GetComponent<Character>();
        if (target != null && target != this)
        {
            Attack(target);
        }
    }

    // Monster cant take damage nor die
    public override bool TakeDamage()
    {
        Debug.Log(characterName + " is immune to damage!");
        return false;
    }

    protected override void Die()
    {
        Debug.Log(characterName + " cannot die!");
    }
}
