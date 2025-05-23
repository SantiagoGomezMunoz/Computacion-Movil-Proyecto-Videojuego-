using NUnit.Framework;
using UnityEngine;

public class BarricadaDestructibleTest
{
    [Test]
    public void RecibirGolpesConHacha_DestruyeBarricada()
    {
        var go = new GameObject();
        var barricada = go.AddComponent<BarricadaDestructibleBasica>();
        barricada.golpesParaDestruir = 3;

        barricada.RecibirGolpe("Hacha");
        barricada.RecibirGolpe("Hacha");
        barricada.RecibirGolpe("Hacha");

        Assert.IsTrue(barricada.destruida);
    }

    [Test]
    public void RecibirGolpeConArmaIncorrecta_NoHaceNada()
    {
        var go = new GameObject();
        var barricada = go.AddComponent<BarricadaDestructibleBasica>();
        barricada.golpesParaDestruir = 2;

        barricada.RecibirGolpe("Fusil");

        Assert.IsFalse(barricada.destruida);
    }
}