using UnityEngine;

public static class MovimientoHelper
{
    public static Vector3 CalcularDireccion(float inputX, float inputZ)
    {
        return new Vector3(inputZ, 0f, -inputX).normalized;
    }
}