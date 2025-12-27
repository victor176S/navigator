using UnityEngine;

public class CameraPlayer : MonoBehaviour
{

    [SerializeField] private GameObject player;

    private Transform playerTransform;

    [SerializeField] float sensitivity;
    float xRotation;

    float yRotation;

    void Start()
    {
        playerTransform = player.transform;
    }

    private void FixedUpdate()

    {

        FPSCameraLogic();
 
    }

    private void Update()
    {
        
    }

    private void FPSCameraLogic()
    {

        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        yRotation += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);             // to stop the player from looking above/below 90

        player.transform.localEulerAngles = new Vector3(0, yRotation, 0);

        transform.localEulerAngles = new Vector3(xRotation, 0, 0);

    }

    private void CameraShiftMovement()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            
        } 
    }
}
