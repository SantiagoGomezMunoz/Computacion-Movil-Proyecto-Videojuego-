using UnityEngine;
using System.Collections.Generic;

public class OcultarEntreCamaraYJugador : MonoBehaviour
{
    public Transform jugador;                 
    public LayerMask capaOcultable;          

    private List<Renderer> objetosOcultados = new List<Renderer>();

    void Update()
    {
        foreach (Renderer rend in objetosOcultados)
        {
            SetTransparente(rend, false);
        }
        objetosOcultados.Clear();

        // Lanzar rayo entre cámara y jugador
        Vector3 direccion = jugador.position - transform.position;
        Ray ray = new Ray(transform.position, direccion);
        float distancia = direccion.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(ray, distancia, capaOcultable);

        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
            {
                SetTransparente(rend, true);
                objetosOcultados.Add(rend);
            }
        }
    }

    void SetTransparente(Renderer rend, bool transparente)
    {
        foreach (Material mat in rend.materials)
        {
            Color color = mat.color;
            color.a = transparente ? 0.3f : 1f;
            mat.color = color;
            mat.SetFloat("_Mode", 3);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
        }
    }
}