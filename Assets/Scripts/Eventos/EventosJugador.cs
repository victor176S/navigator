using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventosJugador : MonoBehaviour
{

    public bool tieneDiamante;

    public GameObject puerta;

    public TextMeshProUGUI texto1;

    public TextMeshProUGUI texto2;

    public TextMeshProUGUI texto3;

    public TextMeshProUGUI texto4;

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
        
        if (other.gameObject.CompareTag("Sala 2"))
        {
            StartCoroutine(TextoSala2(other.gameObject));
        }

        if (other.gameObject.CompareTag("Sala 3"))
        {
            StartCoroutine(TextoSala3());
        }

        if (other.gameObject.CompareTag("WinTrigger"))
        {
            SceneManager.LoadScene(2);
        }

        if (other.gameObject.CompareTag("Diamante"))
        {
            tieneDiamante = true;

            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Edificio"))
        {
            StartCoroutine(TextoSala4(other.gameObject));
        }

    }

    IEnumerator MovimientoPuerta()
    {
        for(int i = 0; i < 20; i++)
        {
           puerta.transform.position -= new Vector3(0, 0.063f, 0);

           yield return new WaitForSeconds(0.02f);  
        }
    }

    IEnumerator TextoSala2(GameObject other)
    {
       texto2.gameObject.SetActive(true);

       yield return new WaitForSeconds(8f);

       texto2.gameObject.SetActive(false);

       other.gameObject.SetActive(false);
    }

    IEnumerator TextoSala3()
    {
       texto3.gameObject.SetActive(true);

       yield return new WaitForSeconds(8f);

       texto3.gameObject.SetActive(false);
    }

    IEnumerator TextoSala4(GameObject other)
    {
       texto4.gameObject.SetActive(true);

       yield return new WaitForSeconds(8f);

       texto4.gameObject.SetActive(false);

       other.gameObject.SetActive(false);
    }
}
