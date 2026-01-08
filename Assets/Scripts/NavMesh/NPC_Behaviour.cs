using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Behaviour : MonoBehaviour
{
    [SerializeField] private Vector3 destination;
    [Tooltip("Si no se le asigna nada, el movimiento será independiente")]
    [SerializeField] private GameObject player;

    [SerializeField] private int childrenIndex;
    [SerializeField] private Transform path;
    [SerializeField] private bool isNPC;
    [SerializeField] private float playerDetectionDistance;
    [SerializeField] private bool playerDetected;

    [SerializeField] private float delayPatroll;

    [SerializeField] private float contador;

    private float tiempoMargen;

    private Coroutine runningPatroll;
    private bool ciego;

    public void Start()
    {
        
        tiempoMargen = GameObject.Find("Data").gameObject.GetComponent<GeneralData>().dificultad;
        
        
        if (isNPC)
        {
            runningPatroll = StartCoroutine("Patroll");
            //StartCoroutine("DistanceDetection");
           
        }

        

    }

    void Update()
    {


        if (playerDetected)
        {

        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

        }

        else
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }

        if (player.GetComponent<PlayerControllerKeyBoard1>().escondido == true)
        {
            playerDetected = false;

            contador = 0;

            StopCoroutine("Follow");

            if(runningPatroll == null)
            {
                runningPatroll = StartCoroutine("Patroll");
            }
        }


    }

    

    

    // #region y #endregion te permite hacer codigo desplegable sin que sean funciones
    //el nombre de la region se pone despues de #region

    #region Always Detect

    IEnumerator Follow()
    {

        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        
            while (true)
            {
                destination = player.transform.position;
                GetComponent<NavMeshAgent>().SetDestination(destination);
                yield return new WaitForEndOfFrame();
                yield return new WaitForSeconds(1);
            }
        

    }

    #endregion

    #region Patroll Movement

    IEnumerator Patroll()
    {

        destination = path.GetChild(childrenIndex).position;

        if(destination == path.GetChild(childrenIndex).position)
        {
            Debug.Log("ha llegado");
        }
        GetComponent<NavMeshAgent>().SetDestination(destination);

        while(true)
        {

            Debug.Log("while patroll");

            Debug.Log("Posicion " + transform.position + "; Destino: " + destination);

            if (Vector3.Distance(transform.position, destination) < 0.5f)
            {

                Debug.Log("if patroll");
                Debug.Log(childrenIndex);

                childrenIndex++;
                childrenIndex = childrenIndex % path.childCount;

                destination = path.GetChild(childrenIndex).position;
                GetComponent<NavMeshAgent>().SetDestination(destination);

               yield return new WaitForEndOfFrame();
                
            }

            yield return new WaitForSeconds(delayPatroll);

        }
    }
    #endregion

    #region 

    IEnumerator DistanceDetection()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < playerDetectionDistance)
            {
                if (runningPatroll != null)
                {
                    StopCoroutine("Patroll");
                    runningPatroll = null;
                }
                    playerDetected = true;
                    destination = player.transform.position;
                    GetComponent<NavMeshAgent>().SetDestination(destination);
                
            }
                
            else 
            {
                playerDetected = false;
                if(runningPatroll == null)
                {
                    StartCoroutine("Patroll");
                }
                
            }

            yield return new WaitForSeconds(1);
        }
    }

    #endregion

    #region Collider Detection

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (runningPatroll != null)
            {

                StopCoroutine("Patroll");
                runningPatroll = null;

            }

            playerDetected = true;

        }
    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
        
            contador += Time.deltaTime / 2;

            if (contador > 2f / tiempoMargen)
            {
                StartCoroutine("Follow");
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine("Follow");
            playerDetected = false;
            
            if(runningPatroll == null)
            {
                runningPatroll = StartCoroutine("Patroll");
            }
        }

        contador = 0;
        
    }

    #endregion

}


