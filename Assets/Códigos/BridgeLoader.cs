using UnityEngine;
using UnityEngine.SceneManagement;

public class BridgeLoader : MonoBehaviour
{
    [SerializeField] private string sceneName = "Puente"; // Nombre de la escena/prefab del puente
    private bool isLoaded = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLoaded)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName);
            isLoaded = false;
        }
    }
}