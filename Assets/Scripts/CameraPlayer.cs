using UnityEngine;

public class CameraPlayer : MonoBehaviour
{

    [SerializeField] private Transform player;

    [SerializeField] float sensitivity;
    float xRotation;

    float yRotation;

    private void Update()

    {
        gameObject.transform.position = player.position;

        player.rotation = gameObject.transform.rotation;

        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        yRotation += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);             // to stop the player from looking above/below 90

        transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);

    }

    
}
