using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    CinemachineCamera vcam;
    TurnController turnController;
    private void Start()
    {
        turnController = FindAnyObjectByType<TurnController>();
    }
    public void LookAtCurrent()
    {
        vcam.LookAt = turnController.characterOrder[0].transform;
    }
}
