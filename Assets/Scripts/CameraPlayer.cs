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
        
        gameObject.transform.position = playerTransform.position;

        Vector3 eulerRotation = new Vector3(playerTransform.eulerAngles.x, transform.eulerAngles.y, playerTransform.eulerAngles.z);

        player.transform.rotation = Quaternion.Euler(eulerRotation);

        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        yRotation += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);             // to stop the player from looking above/below 90

        transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);

    }

    private void CameraShiftMovement()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            
        } 
    }
}
