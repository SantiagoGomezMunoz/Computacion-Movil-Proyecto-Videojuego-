using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ArmaDistanciaTest
{
    [UnityTest]
    public System.Collections.IEnumerator DisparoConMunicion_GeneraProyectil()
    {
        // Crear jugador ficticio con dirección
        GameObject jugador = new GameObject("Jugador");
        var movimiento = jugador.AddComponent<MovimientoPersonaje>();
        movimiento.ultimaDireccionMovimiento = Vector3.forward;

        // Crear arma
        GameObject armaObj = new GameObject("Arma");
        var arma = armaObj.AddComponent<Arma>();
        arma.municionActual = 5;

        // Crear punto de disparo
        GameObject punto = new GameObject("PuntoDisparo");
        arma.puntoDisparo = punto.transform;
        punto.transform.position = Vector3.zero;

        // Crear prefab de proyectil
        GameObject proyectilPrefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        proyectilPrefab.AddComponent<Rigidbody>();
        arma.proyectilPrefab = proyectilPrefab;

        // Esperar un frame para que Start se ejecute
        yield return null;

        arma.Usar();

        // Esperar otro frame para que el proyectil se instancie
        yield return null;

        var proyectilInstanciado = GameObject.FindFirstObjectByType<Rigidbody>();

        Assert.IsNotNull(proyectilInstanciado, "No se generó ningún proyectil.");
    }
}