using UnityEngine;
using System.Collections.Generic;

// genera los personajes en la escena
public class CharacterCreator : MonoBehaviour
{

  [SerializeField] public List<GameObject> characters;

  public void 
        CreateCharacter(Block clicked, int characterID, int player)
  {
    if (characterID != -1)
    {
      GameObject characterPrefab = characters[characterID];
      GameObject localChara = Instantiate(characterPrefab, new Vector3(clicked.transform.position.x, clicked.transform.position.y + 1.5f + clicked.height, clicked.transform.position.z), Quaternion.identity);
      localChara.GetComponent<CharacterController>().belongsToPlayer = player;
      Transform lookingAt = localChara.transform.Find("LookingAt").transform;
      if (player == 1)
        lookingAt.rotation = Quaternion.LookRotation(new Vector3(1, 0, 0), lookingAt.up);
      else
        lookingAt.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 0), lookingAt.up);
    }
  }

}
