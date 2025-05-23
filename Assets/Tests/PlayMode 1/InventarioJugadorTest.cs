using NUnit.Framework;
using UnityEngine;

public class InventarioBasicoTests
{
    private GameObject jugadorGO;
    private InventarioBasico inventario;

    [SetUp]
    public void SetUp()
    {
        jugadorGO = new GameObject("Jugador");
        inventario = jugadorGO.AddComponent<InventarioBasico>();
    }

    [Test]
    public void AgregarObjeto_DeberiaAgregarCorrectamente()
    {
        GameObject objeto = new GameObject("Objeto");
        bool resultado = inventario.AgregarObjeto(objeto);
        Assert.IsTrue(resultado);
        Assert.IsNotNull(inventario.objetos[0]);
    }

    [Test]
    public void AgregarObjeto_InventarioLleno_DeberiaFallar()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = new GameObject("Obj" + i);
            inventario.AgregarObjeto(obj);
        }

        GameObject extra = new GameObject("Extra");
        bool resultado = inventario.AgregarObjeto(extra);

        Assert.IsFalse(resultado);
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(jugadorGO);
    }
}