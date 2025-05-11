using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vida = 7; // Cantidad de disparos que puede recibir

    public void RecibirDaño(int cantidad)
    {
        vida -= cantidad;

        if (vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log($"{gameObject.name} fue derrotado");
        Destroy(gameObject); // Elimina al enemigo de la escena
    }
} 
