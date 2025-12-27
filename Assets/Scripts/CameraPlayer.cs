using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraPlayer : MonoBehaviour
{

    [SerializeField] private GameObject player;

    private Transform playerTransform;

    [SerializeField] float sensitivity;
    float xRotation;

    float yRotation;
    private bool seAgachó = false;

    int i = 0;

    void Start()
    {
        playerTransform = player.transform;
    }

    private void FixedUpdate()

    {

        FPSCameraLogic();
        StartCoroutine(CameraShiftMovement());

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

    private IEnumerator CameraShiftMovement()
    {

        if (player.GetComponent<PlayerControllerKeyBoard1>().agachado && transform.localPosition.y > -0.3)
        {   
            transform.position += new Vector3 (0, -0.1f, 0);
        }

        if (!player.GetComponent<PlayerControllerKeyBoard1>().agachado && transform.localPosition.y < 0)
        {
           transform.position += new Vector3 (0, +0.1f, 0);
        }

        yield return new WaitForSeconds(0.01f);
        
    }
}
