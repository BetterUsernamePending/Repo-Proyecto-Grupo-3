using UnityEngine;
using Unity.Cinemachine;
using UnityEditor;

public class CameraBrainController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera vcam;
    [SerializeField] private CinemachineCamera vcam2;
    [SerializeField] private CinemachineCamera vcam3;
    [SerializeField] private CinemachineCamera vcam4;
    private TurnController turnController;
    public static int cameraAngle = 1;
    private void Start()
    {
        turnController = FindAnyObjectByType<TurnController>();
    }
    public void LookAtCurrent()
    {
        if (cameraAngle < 5)
            switch (cameraAngle)
            {
                case 1:
                    vcam.enabled = true;
                    vcam.LookAt = turnController.characterOrder[0].transform;
                    vcam.transform.position = new Vector3(turnController.characterOrder[0].transform.position.x + 10, turnController.characterOrder[0].transform.position.y + 10, turnController.characterOrder[0].transform.position.z + 10);
                    vcam2.enabled = false;
                    vcam3.enabled = false;
                    vcam4.enabled = false;
                    break;
                case 2:
                    vcam2.enabled = true;
                    vcam2.LookAt = turnController.characterOrder[0].transform;
                    vcam2.transform.position = new Vector3(turnController.characterOrder[0].transform.position.x - 10, turnController.characterOrder[0].transform.position.y + 10, turnController.characterOrder[0].transform.position.z + 10);
                    vcam.enabled = false;
                    vcam3.enabled = false;
                    vcam4.enabled = false;
                    break;
                case 3:
                    vcam3.enabled = true;
                    vcam3.LookAt = turnController.characterOrder[0].transform;
                    vcam3.transform.position = new Vector3(turnController.characterOrder[0].transform.position.x - 10, turnController.characterOrder[0].transform.position.y + 10, turnController.characterOrder[0].transform.position.z - 10);
                    vcam.enabled = false;
                    vcam2.enabled = false;
                    vcam4.enabled = false;
                    break;
                case 4:
                    vcam4.enabled = true;
                    vcam4.LookAt = turnController.characterOrder[0].transform;
                    vcam4.transform.position = new Vector3(turnController.characterOrder[0].transform.position.x + 10, turnController.characterOrder[0].transform.position.y + 10, turnController.characterOrder[0].transform.position.z - 10);
                    vcam.enabled = false;
                    vcam2.enabled = false;
                    vcam3.enabled = false;
                    break;
            }
        else
        {
            cameraAngle = 1;
            LookAtCurrent();
        }
    }
}
