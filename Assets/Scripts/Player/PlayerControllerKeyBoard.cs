using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerKeyBoard : MonoBehaviour
{

    private Rigidbody rb;

    [SerializeField] private bool agachado;

    [SerializeField] private float shiftSpeedModifier;

    [SerializeField] private bool enSuelo;
    [SerializeField] private bool moviendose, moviendoseAdelante, moviendoseAtras, moviendoseIzq, moviendoseDer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!enSuelo)
        {
            
            rb.AddRelativeForce(9.81f * 4 * Time.fixedDeltaTime * Vector3.down, ForceMode.Acceleration);

        }


        if (agachado)
        {
            shiftSpeedModifier = 0.3f;
        }
        else
        {
            shiftSpeedModifier = 1f;
        }
        
        Movimiento();

    }

    private void FrenoMovimiento()
    {
        if(moviendose == false)
        {
            if (rb.linearVelocity.z > 0 || rb.linearVelocity.x > 0)
            {
                rb.linearDamping = 10;
            }
        }

        else
        {
            rb.linearDamping = 0;
        }
    }

    private void Movimiento()
    {

        //agacharse

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {

            if (!agachado)
            {
                StartCoroutine(MoverCamara("shift"));

            }

            agachado = true;
            
        }

        else
        {
            if (agachado)
            {
                StartCoroutine(MoverCamara("back"));
            }

            agachado = false;
        }



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

    IEnumerator MoverCamara(string movimiento)
        {

            if (movimiento == "shift")
            {

                Debug.Log("shift");

                for(int i = 0; i < 100; i++)
                {
                    gameObject.transform.GetChild(1).localPosition += new Vector3 (0,-0.002f,0);

                    yield return new WaitForSeconds(0.002f);

                }

            }

            if (movimiento == "back")
            {

                Debug.Log("back");

                for(int i = 0; i < 100; i++)
                {
                    gameObject.transform.GetChild(1).localPosition += new Vector3 (0,0.002f,0);

                    yield return new WaitForSeconds(0.002f);
                }

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
