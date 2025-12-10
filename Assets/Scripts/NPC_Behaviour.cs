using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.iOS;

public class NPC_Behaviour : MonoBehaviour
{

    [SerializeField] private Vector3 destination;
    [SerializeField] private Vector3 max, min;
    [SerializeField] private GameObject player;
    public bool modoRandom;
    public bool modoClick;
    public void Start()
    {
        if (modoRandom)
        {
            destination = RandomDestination();
            GetComponent<NavMeshAgent>().SetDestination(destination);
        }

        StartCoroutine(Follow());
    }

    void Update()
    {
        if (modoClick)
        {
            if (Input.GetButtonDown("Fire1"))
            {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

                if(Physics.Raycast(ray, out hit, 1000))
                {
                    GetComponent<NavMeshAgent>().SetDestination(hit.point);
                }
            }
           // movimiento con click izq (desde la pestaña game)
        
        }
        

        if (modoRandom)
        {
            if (Vector3.Distance(transform.position, destination) < 0.8f)
            {
                destination = RandomDestination();
             GetComponent<NavMeshAgent>().SetDestination(destination);
            }
        }
        

    }

    private Vector3 RandomDestination()
    {
        return new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));
    }

    #region Always Detect

    IEnumerator Follow()
    {
        while (true)
        {
            destination = player.transform.position;
            GetComponent<NavMeshAgent>().SetDestination(destination);
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(1);
        }
        
    }

    #endregion

}


