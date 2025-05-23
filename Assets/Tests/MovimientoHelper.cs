using NUnit.Framework;
using UnityEngine;

public class MovimientoHelperTests
{
    [Test]
    public void CalcularDireccion_DerechaCorrecta()
    {
        var resultado = MovimientoHelper.CalcularDireccion(-1f, 0f);
        Assert.AreEqual(new Vector3(0f, 0f, 1f), resultado);
    }
    
    [Test]
    public void CalcularDireccion_AbajoCorrecta()
    {
        var resultado = MovimientoHelper.CalcularDireccion(0f, -1f);
        Assert.AreEqual(new Vector3(-1f, 0f, 0f), resultado);
    }
    
    [Test]
    public void CalcularDireccion_ArribaCorrecta()
    {
        var resultado = MovimientoHelper.CalcularDireccion(0f, 1f);
        Assert.AreEqual(new Vector3(1f, 0f, 0f), resultado);
    }
    
    [Test]
    public void CalcularDireccion_DiagonalCorrecta()
    {
        var resultado = MovimientoHelper.CalcularDireccion(1f, 1f);
        var esperado = new Vector3(1f, 0f, -1f).normalized;
        Assert.AreEqual(esperado, resultado);
    }
    
    [Test]
    public void CalcularDireccion_SinMovimiento()
    {
        var resultado = MovimientoHelper.CalcularDireccion(0f, 0f);
        Assert.AreEqual(Vector3.zero, resultado);
    }
}