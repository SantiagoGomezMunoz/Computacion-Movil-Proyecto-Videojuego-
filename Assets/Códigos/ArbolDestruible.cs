using System.Collections;
using UnityEngine;

public class ArbolDestruible : MonoBehaviour
{
    public int vida = 3;
    public GameObject maderaPrefab;
    public Transform puntoSpawnMadera;
    public float tiempoRespawn = 10f;

    private Renderer[] renderers;
    private Collider[] colliders;

    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }

    public void RecibirDano(int cantidad)
    {
        vida -= cantidad;
        Debug.Log($"Árbol recibió {cantidad} de daño. Vida restante: {vida}");

        if (vida <= 0)
        {
            StartCoroutine(DestruirYRespawnear());
        }
    }

    private IEnumerator DestruirYRespawnear()
    {
        // Soltar madera
        if (maderaPrefab != null && puntoSpawnMadera != null)
        {
            GameObject madera = Instantiate(maderaPrefab, puntoSpawnMadera.position, Quaternion.identity);
            madera.SetActive(true); // Asegura que esté activa
        }

        // Ocultar árbol
        foreach (Renderer r in renderers) r.enabled = false;
        foreach (Collider c in colliders) c.enabled = false;

        Debug.Log("Árbol destruido. Esperando respawn...");

        // Esperar antes de reaparecer
        yield return new WaitForSeconds(tiempoRespawn);

        // Restaurar árbol
        vida = 3;
        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;

        foreach (Renderer r in renderers) r.enabled = true;
        foreach (Collider c in colliders) c.enabled = true;

        Debug.Log("Árbol reapareció.");
    }
}