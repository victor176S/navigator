using UnityEngine;

public class HideDelete : MonoBehaviour
{
    private GameObject player;

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
        if (other.gameObject.CompareTag("Hide"))
        {
            other.gameObject.SetActive(false);

            PlayerControllerKeyBoard1.instance.escondido = false;
        }
    }
}
