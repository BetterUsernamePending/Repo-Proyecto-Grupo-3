using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] private List<Block> neighborBlocks = new List<Block>();
    public List<Block> Neighbors => neighborBlocks;
    public LayerMask m_Triggers;
    public Block Connection { get; private set; }
    public float G { get; private set; }
    public float H { get; private set; }
    public float F => G + H;
    public bool walkable = false;
    public int height;
    public Vector3 coord = Vector3.zero; 
    void Start()
    {
        coord = new Vector3(transform.position.x,transform.position.y,transform.position.z);

        m_Triggers = LayerMask.GetMask("Triggers");

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
                Debug.Log(hitColliders[0].gameObject.name);
            }
        }
    }

    public void SetG(float g) => G = g;
    public void SetH(float h) => H = h;
    public void SetConnection(Block block) => Connection = block;
    public bool isWalkable(int height)
    {
        //meter acá la comparación entre la variable de altura actual del personaje y la altura del bloque
        return walkable; //BORRAR ESTO
    }
    public float GetDistance(Block targetBlock)
    {
        return 1; //RESOLVER 
    }
  
    void Update()
    {
        
    }
}
