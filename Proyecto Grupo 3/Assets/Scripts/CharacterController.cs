using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    public int belongsToPlayer;
    public Block currentBlock; //bloque en el que est� parado el personaje
    public Block targetBlock;
    private LayerMask layerToFind;
    private UIManager uiManager;
    public bool isMoving = false;
    public bool isAlive = true;
    public List<AbilityClass> abilityList = new List<AbilityClass>();
    public Dictionary<string, int> origStats = new Dictionary<string, int>();
    public Dictionary<string, int> currentStats = new Dictionary<string, int>();
    public Sprite Portrait;
    public Sprite PortraitBorder;
    public string PortraitName;
    public int atk = 1;
    public int def = 1;
    public int hp = 1;
    public int mp = 1;
    public int movedist = 1;
    public int atkrange = 1;
    public int jump = 1;
    public int spd = 1;
    public int atkheight = 1;
    public Transform lookingAt;
    public Animator animator; // animator
    public GameObject TopView;

    private void Start()
    {
        layerToFind = LayerMask.GetMask("BottomLayer");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layerToFind))
        {
            currentBlock = hit.collider.gameObject.GetComponent<Block>();
            Debug.Log("character" + " " + this.name + " " + "standing in " + currentBlock.name);
        }
        uiManager = FindAnyObjectByType<UIManager>();
        switch (belongsToPlayer)
        {
            case 1:
                transform.GetChild(0).GetComponent<Renderer>().materials[0].SetColor("_OutlineColor", Color.blue);
                break;
            case 2:
                transform.GetChild(0).GetComponent<Renderer>().materials[0].SetColor("_OutlineColor", Color.red);
                break;
            case 3:
                transform.GetChild(0).GetComponent<Renderer>().materials[0].SetColor("_OutlineColor", Color.green);
                break;
            case 4:
                transform.GetChild(0).GetComponent<Renderer>().materials[0].SetColor("_OutlineColor", Color.yellow);
                break;
        }
        animator = GetComponentInChildren<Animator>(); // animaciones
        lookingAt =  transform.GetChild(2); // transform de objeto LookingAt
    }

    Vector3[] blockPositions;
    public void CharacterMove(List<Block> blockPath)
    {
        uiManager.DeactivateBattleUI();
        currentBlock.characterOnBlock = null;
        targetBlock = blockPath.Last();
        blockPositions = new Vector3[blockPath.Count];
        for (int i = 0; i < blockPath.Count; i++)
        {
            float ypos = blockPath[i].height + 1.5f;
            blockPositions[i] = new Vector3(blockPath[i].transform.position.x, ypos, blockPath[i].transform.position.z);
        }
        animator.SetBool("isMoving", true);
        transform.DOPath(blockPositions,1.5f).OnWaypointChange(OnWaypointChanged)
            .OnComplete(() =>
            {
                Reposition();
                animator.SetBool("isMoving", false); // animaciones
                uiManager.ActivateBattleUI();
            });
    }
    public void IsDead()
    {
        this.isAlive = false;
        this.gameObject.SetActive(false);
    }
    public void Reposition()
    {
        currentBlock.containsCharacter = false;
        currentBlock = targetBlock;
        currentBlock.characterOnBlock = this;
        targetBlock = null;
        Debug.Log(this.name + "new position is " + currentBlock.name);
    }

    public void LockBlock()
    {
        currentBlock.containsCharacter = true;
    }

    public void Update()
    {
        // parametros para el animator y flips en eje X de los sprites
        Vector3 camLook = Camera.main.transform.forward;
        Vector3 charDir = lookingAt.forward;
        camLook.y = 0;
        charDir.y = 0;

        float charCamAngle = Vector3.Angle(charDir, camLook);
        Vector3 cross = Vector3.Cross(charDir, camLook);

        if (cross.y < 0) charCamAngle = -charCamAngle;

        SpriteRenderer spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (charCamAngle < 90f && charCamAngle > 0f) // UL
        {
            animator.SetFloat("LookingBack", 1); // mirando hacia atras
            spriteRenderer.flipX = false;
        }
        else if (charCamAngle < 0f && charCamAngle > -90f) // UR
        {
            animator.SetFloat("LookingBack", 1);
            spriteRenderer.flipX = true;
        }
        else if (charCamAngle < 180f && charCamAngle > 90f) // DL
        {
            animator.SetFloat("LookingBack", 0); // mirando hacia adelante
            spriteRenderer.flipX = false;
        }
        else // DR
        {
            animator.SetFloat("LookingBack", 0);
            spriteRenderer.flipX = true;
        }
    }

    void OnWaypointChanged(int waypointIndex) // funcion para rotar personaje en cada giro que hace al moverse.
    {
        if (waypointIndex < blockPositions.Length - 1)
        {
            // Calculate direction from current waypoint to the next
            Vector3 currentWaypoint = blockPositions[waypointIndex];
            Vector3 nextWaypoint = blockPositions[waypointIndex + 1];
            Vector3 direction = (nextWaypoint - currentWaypoint).normalized;
            direction.y = 0;

            transform.GetChild(2).transform.rotation = Quaternion.LookRotation(direction, lookingAt.up); // rota objeto LookingAt
            //Debug.Log($"Direction at waypoint {waypointIndex}: {direction}");
        }
        else
        {
            //Debug.Log("Reached the last waypoint.");
        }
    }
}

