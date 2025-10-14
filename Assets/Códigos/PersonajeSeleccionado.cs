using UnityEngine;

public class PersonajeSeleccionado : MonoBehaviour
{
    public static PersonajeSeleccionado Instancia;

    public Sprite spriteSeleccionado; // Guardará el sprite del personaje elegido

    private void Awake()
    {
        // Aseguramos que no se destruya al cambiar de escena
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SeleccionarPersonaje(Sprite sprite)
    {
        spriteSeleccionado = sprite;
    }
}
