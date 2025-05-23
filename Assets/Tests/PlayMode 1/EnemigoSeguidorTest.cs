using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnemigoSeguidorTests
{
    GameObject enemigoGO;
    GameObject jugadorGO;
    EnemigoSeguidor enemigo;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        jugadorGO = new GameObject("Jugador");
        enemigoGO = GameObject.Instantiate(new GameObject("Enemigo"));
        enemigo = enemigoGO.AddComponent<EnemigoSeguidor>();

        // Configurar Rigidbody necesario
        enemigoGO.AddComponent<Rigidbody>();
        enemigo.objetivo = jugadorGO.transform;

        yield return null;
    }

    [UnityTest]
    public IEnumerator PersigueJugador_CuandoEntraEnRangoVision()
    {
        enemigo.rangoVision = 5f;
        jugadorGO.transform.position = enemigoGO.transform.position + Vector3.forward * 3f;

        yield return new WaitForSeconds(0.1f);

        Assert.IsTrue(GetPrivateBool(enemigo, "persiguiendo"));
    }

    [UnityTest]
    public IEnumerator DejaDePerseguir_CuandoJugadorSaleDelRangoPerdida()
    {
        enemigo.rangoVision = 5f;
        enemigo.rangoPerdida = 10f;

        jugadorGO.transform.position = enemigoGO.transform.position + Vector3.forward * 3f;
        yield return new WaitForSeconds(0.1f);

        // Forzamos a entrar en modo persecución
        jugadorGO.transform.position = enemigoGO.transform.position + Vector3.forward * 12f;
        yield return new WaitForSeconds(0.1f);

        Assert.IsFalse(GetPrivateBool(enemigo, "persiguiendo"));
    }

    private bool GetPrivateBool(object obj, string fieldName)
    {
        var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (bool)field.GetValue(obj);
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.Destroy(enemigoGO);
        GameObject.Destroy(jugadorGO);
    }
}