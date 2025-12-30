using UnityEngine;

public class HideArea : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<PlayerControllerKeyBoard1>().agachado == true)
            {
                other.gameObject.GetComponent<PlayerControllerKeyBoard1>().escondido = true;
            }

            else
            {
                other.gameObject.GetComponent<PlayerControllerKeyBoard1>().escondido = false;
            }
            
        }
    }
}
