using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{

    public Image boton1;

    public Image boton2;

    public Image boton3;

    public GameObject data;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        data = GameObject.Find("Data");

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(data.GetComponent<GeneralData>().dificultad);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Facil()
    {

        data.GetComponent<GeneralData>().dificultad = 1;

        boton1.color = Color.green;

        boton2.color = Color.white;

        boton3.color = Color.white;
    }

    public void Normal()
    {

        data.GetComponent<GeneralData>().dificultad = 1.2f;

        boton1.color = Color.white;

        boton2.color = Color.yellow;

        boton3.color = Color.white;
    }

    public void Dificil()
    {
        data.GetComponent<GeneralData>().dificultad = 1.4f;

        boton1.color = Color.white;

        boton2.color = Color.white;

        boton3.color = Color.red;
    }
}
