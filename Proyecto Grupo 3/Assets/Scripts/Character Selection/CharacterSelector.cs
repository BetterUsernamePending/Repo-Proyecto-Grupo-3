using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Timeline;

public class CharacterSelector : MonoBehaviour
{
    private int selectedCharacterID = -1;
    private CharacterCreator charCreator;

    [SerializeField] private CinemachineCamera vcam;
    [SerializeField] private GameObject midMapTarget;
    [SerializeField] private AudioSource m_CharacterSelectedSFX;
    //player elige sus unidades, se agregan a una lista de CharacterController(s) correspondientes a ese jugador
    private int charactersLeft = 4;
    private int currentPlayer = 1;
    private int maxPlayers = 2;

    public void Start()
    {
        charCreator = FindFirstObjectByType<CharacterCreator>();
    }

    public void OnCharacterSelected(int characterID)
    {
        if (selectedCharacterID == -1)
        {
            selectedCharacterID = characterID;
            Block.onBlockClicked += OnCharacterPlaced;
        }
        else selectedCharacterID = characterID;
        //Debug.Log("seleccionaste " + characterID);
    }

    public void OnCharacterPlaced(Block clicked)
    {
        clicked.DetectCharacter();
        if (!clicked.containsCharacter && !clicked.obstacle)
        {
            Block.onBlockClicked -= OnCharacterPlaced;
            charCreator.CreateCharacter(clicked, selectedCharacterID, currentPlayer);
            selectedCharacterID = -1;
            charactersLeft--;
            m_CharacterSelectedSFX.Play();
            if (charactersLeft == 0)
            {
                currentPlayer++;
                vcam.transform.position = new Vector3(26, 6, 13);
                vcam.transform.LookAt(midMapTarget.transform);
                if (currentPlayer > maxPlayers)
                {
                    UIManager.instance.DeactivateSelectorPanel();
                }
                else { UIManager.instance.ChangeSelectingPlayer(); charactersLeft = 4; }
            }
        }
    }
}
