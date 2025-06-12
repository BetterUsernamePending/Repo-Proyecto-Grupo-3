using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

public class Block : MonoBehaviour
{
    [SerializeField] private List<Block> neighborBlocks = new List<Block>();
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material newMaterial;
    public static Action<Block> onBlockClicked;
    public List<Block> Neighbors => neighborBlocks;
    public LayerMask m_Triggers;
    public Block Connection { get; private set; }
    public float G { get; private set; }
    public float H { get; private set; }
    public float F => G + H;
    [SerializeField] public bool obstacle = false;
    [SerializeField] public int height;
    [SerializeField] private LayerMask Character;
    public CharacterController characterOnBlock;
    public Vector3 coord; //modificar ese "vector 3 zero" por una funcion que asigne los valores del vector 3 como su posicion en X,Y y Z (transform.position) 
    
    void Start()
    {
        if (characterOnBlock != null)
        characterOnBlock.currentBlock = this;

        baseMaterial = GetComponent<Renderer>().materials[0];

        coord = new Vector3(transform.position.x,transform.position.y,transform.position.z);

        m_Triggers = LayerMask.GetMask("BottomLayer");

        Collider[] hitColliders = new Collider[0];
        for (int i = 0; i<4; i++)
        {
            switch(i)
            {
                case 0:
                    hitColliders = Physics.OverlapBox(transform.position + Vector3.right, transform.localScale / 2, Quaternion.identity, m_Triggers);
                    break;
                case 1:
                    hitColliders = Physics.OverlapBox(transform.position + Vector3.left, transform.localScale / 2, Quaternion.identity, m_Triggers);
                    break;
                case 2:
                    hitColliders = Physics.OverlapBox(transform.position + Vector3.forward, transform.localScale / 2, Quaternion.identity, m_Triggers);
                    break;
                case 3:
                    hitColliders = Physics.OverlapBox(transform.position + Vector3.back, transform.localScale / 2, Quaternion.identity, m_Triggers);
                    break;
            }

            if (hitColliders.Length > 0)
            {
                neighborBlocks.Add(hitColliders[0].gameObject.GetComponent<Block>());
                //Debug.Log(hitColliders[0].gameObject.name);
            }
        }
    }

    public void SetG(float g) => G = g;
    public void SetH(float h) => H = h;
    public void SetConnection(Block block) => Connection = block;
    public bool isWalkable(int height, int jump)
    {
       return jump>height; //determina si el bloque es caminable o no
    }
    public float GetDistance(Block currentBlock, Block targetBlock)
    {
        var dist = new Vector2Int(Mathf.Abs((int)currentBlock.transform.position.x - (int)targetBlock.transform.position.x), Mathf.Abs((int)currentBlock.transform.position.z - (int)targetBlock.transform.position.z));

        var lowest = Mathf.Min(dist.x, dist.y);
        var highest = Mathf.Max(dist.x, dist.y);

        var horizontalMovesRequired = highest - lowest;

        return lowest+ horizontalMovesRequired;

    }

    private void OnMouseDown()
    {
        Debug.Log("click" + " " + gameObject.name);
        onBlockClicked?.Invoke(this);
    }

    public void TextureChange()
    {
        GetComponent<Renderer>().material = newMaterial;
    }

    public void TextureRevert()
    {
        GetComponent<Renderer>().material = baseMaterial;
    }

    /*public void DetectCharacter()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, Mathf.Infinity, Character))
        {
            characterOnBlock = hit.collider.gameObject.GetComponent<CharacterController>();
            Debug.Log(name);
        }
    }*/                   
}
