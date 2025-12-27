using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerKeyBoard1 : MonoBehaviour
{

    public static PlayerControllerKeyBoard1 instance;

    private Rigidbody rb;

    public bool agachado;

    [SerializeField] private float shiftSpeedModifier;

    [SerializeField] private bool enSuelo;
    [SerializeField] private bool moviendose, moviendoseAdelante, moviendoseAtras, moviendoseIzq, moviendoseDer;
    [SerializeField] private float rozamiento;

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
            
            rb.AddRelativeForce(9.81f * Time.fixedDeltaTime * Vector3.down, ForceMode.Acceleration);

        }

        ModsDeMovimiento();
        
        DeteccionMovimiento();

        Movimiento();

    }

    private void ModsDeMovimiento()
    {
        if (agachado)
        {
            rb.linearVelocity *= 0.92f;
        }

        else
        {
            shiftSpeedModifier = 1;
        }

        if (!enSuelo)
        {
            rb.linearVelocity *= 0.96f;
        }
        
        else
        {
            shiftSpeedModifier = 1;
        }
    }

    private void DeteccionMovimiento()
    {

        if ((Input.GetKey(KeyCode.Space) && enSuelo) || (Input.GetKeyDown(KeyCode.Space) && enSuelo))
        {
            
                
            //no poner linear damping
            rb.AddRelativeForce(1.5f * Vector3.up, ForceMode.Impulse);

            

        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            
            agachado = true;

        }

        else
        {
            agachado = false;
        }

        //de frente
        if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.W))
        {

            moviendose = true;

            moviendoseAdelante = true;

             
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

        }
            

        else
        {
            moviendose = false;

            moviendoseDer = false;
        }
    }

    private void Movimiento()
    {
        
        if (moviendoseAdelante)
        {
            
            rb.AddRelativeForce(20f * Vector3.forward);

        }

        if (moviendoseAtras)
        {
            
            rb.AddRelativeForce(20f * Vector3.back);

        }

        if (moviendoseDer)
        {
            
            rb.AddRelativeForce(20f * Vector3.right); 

        }

        if (moviendoseIzq)
        {
            
            rb.AddRelativeForce(20f * Vector3.left); 

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
