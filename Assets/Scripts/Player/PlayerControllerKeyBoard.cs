using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerKeyBoard : MonoBehaviour
{

    private Rigidbody rb;

    [SerializeField] private bool agachado;

    [SerializeField] private float shiftSpeedModifier;

    [SerializeField] private bool enSuelo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {



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
            
            if (enSuelo)
            {
                
                rb.AddRelativeForce(10 * Vector3.up, ForceMode.Impulse);

            }

        }

        //de frente
        if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.W))
        {

            

            rb.AddRelativeForce(0.3f * shiftSpeedModifier * Vector3.forward, ForceMode.VelocityChange); 
        }

        //Izq

        if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.A))
        {



            rb.AddRelativeForce(0.3f * shiftSpeedModifier * Vector3.left, ForceMode.VelocityChange); 
        }

        // atras

        if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S))
        {

            

            rb.AddRelativeForce(0.3f * shiftSpeedModifier * Vector3.back, ForceMode.VelocityChange); 
        }

        //derecha

        if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.D))
        {

            

            rb.AddRelativeForce(shiftSpeedModifier * 0.3f  * Vector3.right, ForceMode.VelocityChange); 
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
