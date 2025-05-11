using UnityEngine;

public class ArmaCuerpoACuerpo : MonoBehaviour, IArma
{
    public float rangoAtaque = 2f;
    public int daño = 1;
    public LayerMask capaEnemigos; 

    public Transform puntoAtaque; // Punto desde donde se genera el golpe

    public float tiempoEntreAtaques = 0.5f;
    private float tiempoUltimoAtaque = 0f;
    private InventarioJugador inventario;

    void Start()
    {
        inventario = FindFirstObjectByType<InventarioJugador>();
    }

    void Update()
    {
        if (inventario == null || (UnityEngine.Object)inventario.armaEquipada != this)
        return;

        if (Input.GetMouseButtonDown(0) && Time.time >= tiempoUltimoAtaque)
        {
            Usar();
            tiempoUltimoAtaque = Time.time + tiempoEntreAtaques;
        }
    }

    public void Usar()
    {
        Collider[] enemigosGolpeados = Physics.OverlapSphere(puntoAtaque.position, rangoAtaque, capaEnemigos);

        if (enemigosGolpeados.Length == 0)
        {
            Debug.Log("Ataque realizado, pero no se golpeó a ningún enemigo.");
        }

        foreach (Collider enemigo in enemigosGolpeados)
        {
            EnemigoSeguidor enemigoScript = enemigo.GetComponent<EnemigoSeguidor>();
            if (enemigoScript != null)
            {
                enemigoScript.RecibirDaño(daño);
                Debug.Log($"Golpeaste a: {enemigo.name} y le hiciste {daño} de daño.");
            }

            BarricadaDestructible barricada = enemigo.GetComponent<BarricadaDestructible>();
            if (barricada != null)
            {
                barricada.RecibirGolpe("Hacha"); 
                Debug.Log("Golpeaste una barricada con el hacha.");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (puntoAtaque != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
        }
    }
}