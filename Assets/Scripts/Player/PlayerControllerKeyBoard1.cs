using System;
using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerKeyBoard1 : MonoBehaviour
{

    public static PlayerControllerKeyBoard1 instance;

    private Rigidbody rb;

    [SerializeField] private float rotacionSuelo;

    [SerializeField] private float boostEnRampas;
    
    public GameObject spawnPoint;

    public bool agachado;

    [SerializeField] private float shiftSpeedModifier;

    [SerializeField] private bool enSuelo;
    [SerializeField] private bool moviendose, moviendoseAdelante, moviendoseAtras, moviendoseIzq, moviendoseDer;
    [SerializeField] private float rozamiento;
    [SerializeField] private bool rampa;
    private float rampaMargen = 1f;
    private Vector3 rayCastFeetOffset;
    [SerializeField] private bool esPared;
    private float stepHeight = 10f;
    [SerializeField] private bool saltando;

    [SerializeField] private float gravedadCustom; //el default deberia ser 1
    private bool forzarAgachado;

    public bool escondido;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rayCastFeetOffset = new Vector3(0, transform.localScale.y +0.03f, 0);

        rb = gameObject.GetComponent<Rigidbody>();

        transform.position = spawnPoint.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //freno de movimiento

        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 10);

        if (!enSuelo)
        {
            
            rb.AddRelativeForce(9.81f * 146 * gravedadCustom * Time.fixedDeltaTime * Vector3.down, ForceMode.Acceleration);

        }


        ModsDeMovimiento();
        
        DeteccionMovimiento();

        Movimiento();



    }

    private void Update()
    {
        DeteccionRayCast();
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

        if (rotacionSuelo > 0 || rotacionSuelo < 0 && rampa)
        {
            boostEnRampas = MathF.Sqrt(Mathf.Sqrt(Mathf.Abs(rotacionSuelo))) * 1.5f;
        }
        else
        {
            
            boostEnRampas = 1;

        }
    }

    private void DeteccionMovimiento()
    {

        if ((Input.GetKey(KeyCode.Space) && enSuelo) || (Input.GetKeyDown(KeyCode.Space) && enSuelo))
        {
            //no poner linear damping
            rb.AddRelativeForce(1.5f * 24f * Vector3.up, ForceMode.Impulse);

            saltando = true;

        }

        //poner objetos que no sean planos para solucionar automaticamente 
        //el bug de ser más bajo sistema de agachado

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {

            transform.localScale = new Vector3(0.5f, 0.2f, 0.5f);
            
            agachado = true;
            

        }

        else
        {
            if (!forzarAgachado)
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                escondido = false;

                agachado = false;
            }

            if (forzarAgachado)
            {
                transform.localScale = new Vector3(0.5f, 0.2f, 0.5f);
            
                agachado = true;
            }
            
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !forzarAgachado)
        {
            transform.position += new Vector3(0, 0.5f, 0);
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
            
            rb.AddRelativeForce(20f * boostEnRampas * Vector3.forward);

        }

        if (moviendoseAtras)
        {
            
            rb.AddRelativeForce(20f * boostEnRampas * Vector3.back);

        }

        if (moviendoseDer)
        {
            
            rb.AddRelativeForce(20f * boostEnRampas * Vector3.right); 

        }

        if (moviendoseIzq)
        {
            
            rb.AddRelativeForce(20f * boostEnRampas * Vector3.left); 

        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Suelo"))
        {
            enSuelo = true;
            saltando = false;
        }
    }

    void OnTriggerStay(Collider other)
    {

        if (other.transform.CompareTag("Suelo"))
        {
            enSuelo = true;
        }

        rotacionSuelo = other.transform.eulerAngles.x + other.transform.eulerAngles.z;
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.transform.CompareTag("Suelo"))
        {
            enSuelo = false;
        }
    }
    private void DeteccionRayCast()
    {
        RaycastHit hitLower;
        RaycastHit HitMiddle;
        //RaycastHit hitHigher;
			if (Physics.Raycast(transform.localPosition - rayCastFeetOffset, transform.TransformDirection(Vector3.forward), out hitLower, 0.3f) ||
                Physics.Raycast(transform.localPosition - rayCastFeetOffset, transform.TransformDirection(Vector3.right), out hitLower, 0.3f) ||
                Physics.Raycast(transform.localPosition - rayCastFeetOffset, transform.TransformDirection(Vector3.back), out hitLower, 0.3f) ||
                Physics.Raycast(transform.localPosition - rayCastFeetOffset, transform.TransformDirection(Vector3.left), out hitLower, 0.3f))
			{
                
				if (hitLower.point.y <= stepHeight && !esPared && !rampa)
				{


					transform.position += new Vector3 (0, 0.05f, 0);
				}
                
            }

            if (Physics.Raycast(transform.position - rayCastFeetOffset, transform.TransformDirection(Vector3.down), out hitLower, 100f))
            {
                if (hitLower.point.y <= rampaMargen && hitLower.point.y > 0.02f && !saltando)
				{


					//transform.localPosition -= new Vector3 (0, 0.01f, 0);
				}
            }

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 0.35f) ||
                Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), 0.35f) ||
                Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), 0.35f) ||
                Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), 0.35f))
            {

                Debug.Log("ray if 1");
            
                esPared = true;

            }

            else
            {

                esPared = false;
            
            }

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out HitMiddle, 1f))
            {

                if(HitMiddle.transform.rotation.eulerAngles.x > 0 || HitMiddle.transform.rotation.eulerAngles.z > 0)
                {
                    rampa = true;
                }  
            }

            else
            {

                rampa = false;
            
            }

            if (Physics.Raycast(transform.position + new Vector3(0.2f, 0, 0), transform.TransformDirection(Vector3.up), 1.2f) ||
                Physics.Raycast(transform.position + new Vector3(-0.2f, 0, 0), transform.TransformDirection(Vector3.up), 1.2f) ||
                Physics.Raycast(transform.position + new Vector3(0, 0, 0.2f), transform.TransformDirection(Vector3.up), 1.2f) ||
                Physics.Raycast(transform.position + new Vector3(0, 0, -0.2f), transform.TransformDirection(Vector3.up), 1.2f))
            {

                Debug.Log("Forzar");

                forzarAgachado = true;
            }

            else
            {
                forzarAgachado = false;
            }
    }
}
