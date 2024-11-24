using UnityEngine;
using System.Collections;

public class MonsterCharacter : Character
{
    public GameObject monsterPrefab;
    public GameObject characterPrefab;

    public bool isTransformed = false;
    
    public override void Start(){
        base.Start();

        monsterPrefab.SetActive(false);
    }
    
    public override void Update(){
        base.Update();

        if (Input.GetKeyDown(KeyCode.T)) {
            Transform();
        }
    }

    // Transform ability unique to the monster
    public void Transform()
    {
        if (!isTransformed){

            monsterPrefab.SetActive(true);
            characterPrefab.SetActive(false);

            isTransformed = true;
            
        } else {

            monsterPrefab.SetActive(false);
            characterPrefab.SetActive(true);

            isTransformed = false;
        }
    }

    public void Attack(Character target)
    {
        
    }

    /*
    private IEnumerator TransformCooldown()
    {
        canTransform = false;
        yield return new WaitForSeconds(cooldown);
        canTransform = true;
    }
    */
}
