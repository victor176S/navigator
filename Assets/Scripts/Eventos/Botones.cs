using System.Collections;
using UnityEngine;

public class Botones : MonoBehaviour
{
    [SerializeField] GameObject puerta;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AbrirPuertaWrap()
    {
        StartCoroutine(AbrirPuerta());
    }

    public IEnumerator AbrirPuerta()
    {
        for(int i = 0; i < 20; i++)
        {
           puerta.transform.position += new Vector3(0, 0.17f, 0);

           yield return new WaitForSeconds(0.02f);  
        }
    }
}
