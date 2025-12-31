using System.Collections;
using TMPro;
using UnityEngine;

public class EventosMapa : MonoBehaviour
{

    public TextMeshProUGUI textoAparecer;

    public TextMeshProUGUI textoAparecer1;

    public TextMeshProUGUI textoDesaparecer;
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
        if (other.gameObject.CompareTag("Puerta"))
        {
            Debug.Log("Puerta trigger");

            StartCoroutine(TextoPuerta());
        }
    }

    IEnumerator TextoPuerta()
    {
            textoAparecer.gameObject.SetActive(true);

            textoAparecer1.gameObject.SetActive(true);

            textoDesaparecer.gameObject.SetActive(false);

            yield return new WaitForSeconds(8f);

            textoAparecer1.gameObject.SetActive(false);
    }
}
