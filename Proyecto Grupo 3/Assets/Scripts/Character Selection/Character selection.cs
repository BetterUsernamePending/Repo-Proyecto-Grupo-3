using UnityEngine;
using UnityEngine.UIElements;

public class Characterselection : MonoBehaviour
{
    [SerializeField] GameObject SorinPref;
    [SerializeField] GameObject CimarronPref;
    [SerializeField] GameObject RobPref;
    [SerializeField] GameObject PigiusPref;
    //player elige sus unidades, se agregan a una lista de CharacterController(s) correspondientes a ese jugador
    public void CreateCimarron(Vector3 position)
    {
        Instantiate(CimarronPref,new Vector3(position.x,position.y + 1.5f,position.z),Quaternion.identity);
    }
    public void CreateSorin(Vector3 position)
    {
        Instantiate(SorinPref, new Vector3(position.x, position.y + 1.5f, position.z), Quaternion.identity);
    }
    public void CreateRob(Vector3 position)
    {
        Instantiate(RobPref, new Vector3(position.x, position.y + 1.5f, position.z), Quaternion.identity);
    }
    public void CreatePigius(Vector3 position)
    {
        Instantiate(PigiusPref, new Vector3(position.x, position.y + 1.5f, position.z), Quaternion.identity);
    }
    //se crean instancias de los personajes seleccionados en el mapa donde se juega

    //se pasa a la fase de posicionamiento en la escena donde se juega. En orden, los jugadores ubican sus unidades
}
