using UnityEngine;
using TMPro;

public class MostrarControles : MonoBehaviour
{
    public TextMeshProUGUI textoControles;
    private bool estaVisible = true;

    void Start()
    {
        if (textoControles != null)
            textoControles.gameObject.SetActive(true); // Mostrar al iniciar
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && textoControles != null)
        {
            estaVisible = !estaVisible;
            textoControles.gameObject.SetActive(estaVisible);
        }
    }
}