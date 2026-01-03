using UnityEngine;

public class Data : MonoBehaviour
{

    public static Data instance;

    public bool muertoCaida;

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
