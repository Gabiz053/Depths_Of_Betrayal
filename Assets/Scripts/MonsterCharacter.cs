using UnityEngine;
using System.Collections;

#define DEAD 1

public class MonsterCharacter : Character
{
    public GameObject monsterModel;
    public bool isTransformed = false;
    private int cooldown = 10; // 10 second cooldown for the transform ability    
    private bool canTransform = true;

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
            health *= 2; // Example: Double health when transformed
            speed *= 2; // Increase speed
            Debug.Log(characterName + " has transformed!");
            monsterModel.SetActive(false); // Hide the character model
        }
        else
        {
            isTransformed = false;
            health /= 2; // Revert health
            speed /= 2; // Revert speed
            Debug.Log(characterName + " has reverted back.");
            monsterModel.SetActive(true); // Show the character model
        }
        isTransformed = !isTransformed;
    }

    public void Attack(Character target)
    {
        if (target.TakeDamage() == DEAD) {
            // Transform back if the target dies
            ToggleTransform();
            StartCoroutine(TransformCooldown());
        }
        //Reduce speed for a short time
        speed /= 2;
        while (speed < SPEED)
        {
            speed += 0.1f;
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
    public override void TakeDamage()
    {
        Debug.Log(characterName + " is immune to damage!");
    }

    protected override void Die()
    {
        Debug.Log(characterName + " cannot die!");
    }
}
