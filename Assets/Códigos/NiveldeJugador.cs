using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    public Slider xpBar;
    public TMP_Text nivelTexto;

    public int nivel = 1;
    public float xpActual = 0;
    public float xpNecesaria = 100;

    void Start()
    {
        ActualizarUI();
    }

    void Update()
    {
        // Solo para prueba: ganar XP al presionar tecla
        if (Input.GetKeyDown(KeyCode.X))
        {
            GanarExperiencia(25);
        }
    }

    public void GanarExperiencia(float cantidad)
    {
        xpActual += cantidad;
        if (xpActual >= xpNecesaria)
        {
            xpActual -= xpNecesaria;
            SubirNivel();
        }
        ActualizarUI();
    }

    void SubirNivel()
    {
        nivel++;
        xpNecesaria += 25; // Aumenta dificultad
    }

    void ActualizarUI()
    {
        xpBar.maxValue = xpNecesaria;
        xpBar.value = xpActual;
        nivelTexto.text = "Nivel " + nivel;
    }
}
