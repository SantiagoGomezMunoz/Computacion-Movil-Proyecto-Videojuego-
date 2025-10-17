using UnityEngine;

[System.Serializable]
public class DatosPersonaje
{
    public Sprite[] spritesAbajo;
    public Sprite[] spritesArriba;
    public Sprite[] spritesIzquierda;
    public Sprite[] spritesDerecha;
}

public class PersonajeSeleccionado : MonoBehaviour
{
    public static PersonajeSeleccionado Instancia;

    public DatosPersonaje personajeDatos; // Aquí guardamos los sprites del personaje elegido

    private void Awake()
    {
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

    public void SeleccionarPersonaje(DatosPersonaje nuevoDatos)
    {
        personajeDatos = nuevoDatos;
    }
}
