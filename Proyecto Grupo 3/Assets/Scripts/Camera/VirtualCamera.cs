using Unity.Cinemachine;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{

    private void Awake()
    {
        gameObject.AddComponent<CinemachineCamera>();
    }
}
