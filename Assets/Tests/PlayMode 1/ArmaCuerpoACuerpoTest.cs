using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ArmaCuerpoACuerpoTest
{
    [UnityTest]
    public System.Collections.IEnumerator AtaqueCuerpoACuerpo_HiereEnemigo()
    {
        // Crear el arma cuerpo a cuerpo
        GameObject armaGO = new GameObject("ArmaCuerpoACuerpo");
        var arma = armaGO.AddComponent<ArmaCuerpoACuerpo>();
        arma.rangoAtaque = 2f;
        arma.daño = 1;

        // Usamos una capa válida directamente (por ejemplo la 8)
        int capaDePrueba = 8;
        arma.capaEnemigos = 1 << capaDePrueba;

        // Crear el punto de ataque
        GameObject puntoAtaque = new GameObject("PuntoAtaque");
        arma.puntoAtaque = puntoAtaque.transform;
        puntoAtaque.transform.position = Vector3.zero;

        // Crear enemigo dentro del rango
        GameObject enemigo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        enemigo.layer = capaDePrueba;
        enemigo.transform.position = new Vector3(1f, 0f, 0f); // Dentro del rango

        // Agregar el script simulado con RecibirDaño
        var enemigoScript = enemigo.AddComponent<EnemigoSeguidorPrueba>();

        yield return null;

        // Usar el arma
        arma.Usar();

        // Verificar que recibió el daño
        Assert.AreEqual(1, enemigoScript.dañoRecibido, "El enemigo no recibió el daño esperado.");
    }

    // Clase simulada del enemigo con RecibirDaño
    public class EnemigoSeguidorPrueba : MonoBehaviour
    {
        public int dañoRecibido = 1;

        public void RecibirDaño(int cantidad)
        {
            dañoRecibido += cantidad;
        }
    }
}