using UnityEngine;
using UnityEngine.SceneManagement;

public class IrAFinal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            SceneManager.LoadScene("EscenaFinal"); 
        }
    }
}