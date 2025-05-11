using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class DatosJugador
{
    public int vidaActual;
    public int nivel;
    public float xpActual;
    public float[] posicion = new float[3];
    public string[] objetosInventarioIDs = new string[3]; // Máximo 3 slots
    public int indiceCasillaSeleccionada;
    public int municionActual;

    public List<string> objetosRecogidos = new List<string>();
}