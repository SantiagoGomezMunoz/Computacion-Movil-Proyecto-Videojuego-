using UnityEngine;

public class ArmaEscopeta : MonoBehaviour, IArma, IApplyUpgrades
{
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public int municionActual = 15;
    public int municionMaxima = 15;
    public float velocidadProyectil = 30f;

    [Header("Escopeta (pellets)")]
    public int pelletsPorDisparo = 3;
    public float spreadGrados = 12f; 

    public AudioClip sonidoDisparo;
    public AudioSource audioSource;
    private MovimientoPersonaje movimientoJugador;

    void Start()
    {
        movimientoJugador = FindFirstObjectByType<MovimientoPersonaje>();

        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
        {
            var source = jugador.GetComponent<AudioSource>();
            if (source != null) audioSource = source;
        }

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        ApplyUpgrades();
    }

    public void ApplyUpgrades()
    {
        if (UpgradeManager.Instance != null)
            pelletsPorDisparo = UpgradeManager.Instance.GetShotgunPellets();
    }


    public void Usar()
    {
        if (municionActual <= 0 || proyectilPrefab == null || puntoDisparo == null) return;

        Vector3 direccionBase = Vector3.forward;
        if (movimientoJugador != null)
        {
            direccionBase = movimientoJugador.ultimaDireccionMovimiento;
        }

        if (direccionBase == Vector3.zero)
            direccionBase = puntoDisparo.forward;

        Vector3 dirNormalized = direccionBase.normalized;

        for (int i = 0; i < pelletsPorDisparo; i++)
        {
            // Spread en plano horizontal (Y axis) y vertical opcionalmente
            float yaw = Random.Range(-spreadGrados, spreadGrados);
            float pitch = Random.Range(-spreadGrados * 0.25f, spreadGrados * 0.25f); 
            Quaternion rot = Quaternion.LookRotation(dirNormalized) * Quaternion.Euler(pitch, yaw, 0f);
            Vector3 dirPellet = rot * Vector3.forward;

            GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.LookRotation(dirPellet));
            Rigidbody rb = proyectil.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = dirPellet * velocidadProyectil;
            }
        }

        // Consume 1 munición por disparo 
        municionActual = Mathf.Max(0, municionActual - 1);

        if (audioSource != null && sonidoDisparo != null)
            audioSource.PlayOneShot(sonidoDisparo);
    }
}