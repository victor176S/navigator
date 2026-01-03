using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextoDerrota : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var data = GameObject.Find("Data");

        if (data.GetComponent<Data>().muertoCaida == true && data.GetComponent<Data>() != null)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "Perdiste porque te caiste desde muy alto, prueba otra vez"; 
        }

        if(data.GetComponent<Data>().muertoCaida == false && data.GetComponent<Data>() != null)
        {
           gameObject.GetComponent<TextMeshProUGUI>().text = "Perdiste porque te atraparon, prueba de nuevo"; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
