using NUnit.Framework;
using UnityEngine;

public class GestorObjetivosTest
{
    [Test]
    public void MarcarObjetivoComoCumplido_ActualizaListaCorrectamente()
    {
        // Crear objeto de prueba
        var go = new GameObject();
        var gestor = go.AddComponent<GestorObjetivosBasico>();

        // Configurar objetivos
        gestor.objetivos.Add("Encontrar el hacha");
        gestor.objetivos.Add("Recoger el fusil");
        gestor.Start(); // Llenar la lista de objetivosCumplidos

        // Verificar inicialmente
        Assert.IsFalse(gestor.objetivosCumplidos[1]);

        // Marcar segundo objetivo como cumplido
        gestor.MarcarComoCumplido(1);

        // Verificar que se marcó correctamente
        Assert.IsTrue(gestor.objetivosCumplidos[1]);
    }

    [Test]
    public void CambiarTextoObjetivo_ActualizaTextoCorrectamente()
    {
        var go = new GameObject();
        var gestor = go.AddComponent<GestorObjetivosBasico>();

        gestor.objetivos.Add("Buscar fusil");
        gestor.Start();

        gestor.CambiarTextoObjetivo(0, "Buscar botiquín");

        Assert.AreEqual("Buscar botiquín", gestor.objetivos[0]);
    }
}