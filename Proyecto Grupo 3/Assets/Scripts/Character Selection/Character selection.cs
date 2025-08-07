using UnityEngine;
using UnityEngine.UIElements;

public class Characterselection : MonoBehaviour
{
    [SerializeField] private GameObject SorinPref;
    [SerializeField] private GameObject CimarronPref;
    [SerializeField] private GameObject RobPref;
    [SerializeField] private GameObject PigiusPref;
    //player elige sus unidades, se agregan a una lista de CharacterController(s) correspondientes a ese jugador
    private int charactersLeft = 4;
    private int currentPlayer = 1;
    private UIManager manager;
    public void Start()
    {
        manager = FindFirstObjectByType<UIManager>();
    }
    public void SelectedCharacter(int characterNumber)
    {
        switch (characterNumber)
        {
            case 0:
                Block.onBlockClicked += CreatePigius;
                break;
            case 1:
                Block.onBlockClicked += CreateSorin;
                break;
            case 2:
                Block.onBlockClicked += CreateRob;
                break;
            case 3:
                Block.onBlockClicked += CreateCimarron;
                break;
        }
    }
    public void CreateCimarron(Block clicked)
    {
        GameObject localChara = Instantiate(CimarronPref,new Vector3(clicked.transform.position.x, clicked.transform.position.y + 1.5f + clicked.height, clicked.transform.position.z), Quaternion.identity);
        localChara.GetComponent<CharacterController>().belongsToPlayer = currentPlayer;
        Block.onBlockClicked -= CreateCimarron;
        charactersLeft--;
        if(charactersLeft == 0)
        {
            currentPlayer++;
            if (currentPlayer == 3)
            {
                manager.DeactivateSelectorPanel();
            }
            else { manager.ChangeSelectingPlayer(); charactersLeft = 4; }
        }
    }
    public void CreateSorin(Block clicked)
    {
        GameObject localChara = Instantiate(SorinPref, new Vector3(clicked.transform.position.x, clicked.transform.position.y + 1.5f + clicked.height, clicked.transform.position.z), Quaternion.identity);
        localChara.GetComponent<CharacterController>().belongsToPlayer = currentPlayer;
        Block.onBlockClicked -= CreateSorin;
        charactersLeft--;
        if (charactersLeft == 0)
        {
            currentPlayer++;
            if (currentPlayer == 3)
            {
                manager.DeactivateSelectorPanel();
            }
            else { manager.ChangeSelectingPlayer(); charactersLeft = 4; }
        }
    }
    public void CreateRob(Block clicked)
    {
        GameObject localChara = Instantiate(RobPref, new Vector3(clicked.transform.position.x, clicked.transform.position.y + 1.5f + clicked.height, clicked.transform.position.z), Quaternion.identity);
        localChara.GetComponent<CharacterController>().belongsToPlayer = currentPlayer;
        Block.onBlockClicked -= CreateRob;
        charactersLeft--;
        if (charactersLeft == 0)
        {
            currentPlayer++;
            if (currentPlayer == 3)
            {
                manager.DeactivateSelectorPanel();
            }
            else { manager.ChangeSelectingPlayer(); charactersLeft = 4; }
        }
    }
    public void CreatePigius(Block clicked)
    {
        GameObject localChara = Instantiate(PigiusPref, new Vector3(clicked.transform.position.x, clicked.transform.position.y + 1.5f + clicked.height, clicked.transform.position.z), Quaternion.identity);
        localChara.GetComponent<CharacterController>().belongsToPlayer = currentPlayer;
        Block.onBlockClicked -= CreatePigius;
        charactersLeft--;
        if (charactersLeft == 0)
        {
            currentPlayer++;
            if (currentPlayer == 3)
            {
                manager.DeactivateSelectorPanel();
            }
            else { manager.ChangeSelectingPlayer(); charactersLeft = 4; }
        }
    }
    //se crean instancias de los personajes seleccionados en el mapa donde se juega

    //se pasa a la fase de posicionamiento en la escena donde se juega. En orden, los jugadores ubican sus unidades
}
