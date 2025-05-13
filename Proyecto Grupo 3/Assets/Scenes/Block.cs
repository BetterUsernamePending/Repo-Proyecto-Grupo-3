using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] private List<Block> neighborBlocks = new List<Block>();
    public LayerMask m_Triggers;

    void Start()
    {
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

  
    void Update()
    {
        
    }
}
