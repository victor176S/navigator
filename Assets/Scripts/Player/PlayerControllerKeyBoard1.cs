using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerKeyBoard1 : MonoBehaviour
{

    private Rigidbody rb;

    [SerializeField] private bool agachado;

    [SerializeField] private float shiftSpeedModifier;

    [SerializeField] private bool enSuelo;

    [SerializeField] private GameObject camara;
    [SerializeField] private bool moviendose, moviendoseAdelante, moviendoseAtras, moviendoseIzq, moviendoseDer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //freno de movimiento

        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 10);

        if (!enSuelo)
        {
            
            rb.AddRelativeForce(9.81f * 4 * Time.fixedDeltaTime * Vector3.down, ForceMode.Acceleration);

        }
        
        Movimiento();

    }

    private void Movimiento()
    {

        if ((Input.GetKey(KeyCode.Space) && enSuelo) || (Input.GetKeyDown(KeyCode.Space) && enSuelo))
        {
            
                
            //no poner linear damping
            rb.AddRelativeForce(4 * Vector3.up, ForceMode.Impulse);

            

        }

        //de frente
        if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.W))
        {

            moviendose = true;

            moviendoseAdelante = true;

            rb.AddRelativeForce(0.2f * shiftSpeedModifier * Vector3.forward, ForceMode.VelocityChange); 
        }

        else
        {
            moviendose = false;

            moviendoseAdelante = false;
        }

        //Izq

        if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.A))
        {

            moviendose = true;

            moviendoseIzq = true;

            rb.AddRelativeForce(0.2f * shiftSpeedModifier * Vector3.left, ForceMode.VelocityChange); 
        }

        else
        {
            moviendose = false;

            moviendoseIzq = false;
        }

        // atras

        if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S))
        {

            moviendose = true;

            moviendoseAtras= true;

            rb.AddRelativeForce(0.2f * shiftSpeedModifier * Vector3.back, ForceMode.VelocityChange); 
        }

        else
        {
            moviendose = false;

            moviendoseAtras = false;
        }

        //derecha

        if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.D))
        {
            moviendose = true;

            moviendoseDer = true;

            rb.AddRelativeForce(0.2f * shiftSpeedModifier * Vector3.right, ForceMode.VelocityChange); 
        }

        else
        {
            moviendose = false;

            moviendoseDer = false;
        }

        

    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Suelo"))
        {
            enSuelo = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.transform.CompareTag("Suelo"))
        {
            enSuelo = false;
        }
    }

}
