using UnityEngine;

public class GeneralData : MonoBehaviour
{

    public static GeneralData instance;

    public float dificultad;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
