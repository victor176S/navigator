using System.Collections;
using TMPro;
using UnityEngine;

public class EventosJugador : MonoBehaviour
{

    public GameObject puerta;

    public TextMeshProUGUI texto1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Entrada"))
        {
            StartCoroutine(MovimientoPuerta());
        }

        if (other.gameObject.CompareTag("Ventilacion1"))
        {
            texto1.gameObject.SetActive(false);
        }
    }

    IEnumerator MovimientoPuerta()
    {
        for(int i = 0; i < 20; i++)
        {
           puerta.transform.position -= new Vector3(0, 0.13f, 0);

           yield return new WaitForSeconds(0.02f);  
        }
    }
}
