using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Charecter Selection/Character")]
public class CharacterComplete : ScriptableObject
{

    [SerializeField] public GameObject diverPrefab = default;
    //[SerializeField] public GameObject alternativePrefab = default;

    public GameObject DiverPrefab => diverPrefab;
    //public GameObject AlternativePrefab => alternativePrefab;
}
