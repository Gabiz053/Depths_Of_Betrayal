// using UnityEngine;
// using Unity.Netcode;

// public class CharacterManager : NetworkBehaviour
// {
//     public GameObject[] characters; // Array of character prefabs
//     public GameObject[] monster;
//     public GameObject crystalPrefab; // Crystal prefab
//     public int numberOfCrystals; // Number of crystals to create

//     private int monsterIndex;

//     private void Start()
//     {
//         if (IsServer)
//         {
//             AssignCharacters();
//             CreateCrystals(); // Create crystals when the session initiates
//         }
//     }

//     private void AssignCharacters()
//     {
//         bool isMonster;
//         var playerIDs = NetworkManager.Singleton.ConnectedClientsList;
//         Shuffle(playerIDs);

//         for (int i = 0; i < playerIDs.Count; i++)
//         {
//             var playerID = playerIDs[i].ClientId;
//             var characterIndex = i;
//             isMonster = false;

//             if (i==0){
//                 monsterIndex = 1;
//                 MonsterCharacter monsterComponent = characterIndex.AddComponent<MonsterCharacter>();
//                 monsterComponent.isTransformed = false;
//                 isMonster = true;
                
//             }
//             AssignCharacterToPlayerServerRpc(playerID, characterIndex, isMonster);
//         }
//     }

//     private void CreateCrystals()
//     {
//         for (int i = 0; i < numberOfCrystals; i++)
//         {
//             //TODO: review
//             Vector3 randomPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)); // Example random position
//             GameObject crystal = Instantiate(crystalPrefab, randomPosition, Quaternion.identity);
//             crystal.GetComponent<NetworkObject>().Spawn();
//         }
//     }

//     [ServerRpc(RequireOwnership = false)]
//     private void AssignCharacterToPlayerServerRpc(ulong playerID, int characterIndex, bool isMonster)
//     {
//         GameObject playerCharacter = Instantiate(characters[characterIndex], Vector3.zero, Quaternion.identity);
//         playerCharacter.GetComponent<NetworkObject>().SpawnWithOwnership(playerID);

//         if (isMonster)
//         {
//             playerCharacter.GetComponent<Character>().EnableMonsterPowers();
//         }

//         AssignCharacterToPlayerClientRpc(playerID, characterIndex, isMonster);
//     }

//     [ClientRpc]
//     private void AssignCharacterToPlayerClientRpc(ulong playerID, int characterIndex, bool isMonster)
//     {
//         // Handle character setup on each client
//     }

//     private void Shuffle(List<NetworkClient> list)
//     {
//         for (int i = list.Count - 1; i > 0; i--)
//         {
//             int rnd = Random.Range(0, i + 1);
//             var temp = list[i];
//             list[i] = list[rnd];
//             list[rnd] = temp;
//         }
//     }
// }