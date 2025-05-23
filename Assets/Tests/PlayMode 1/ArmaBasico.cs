using UnityEngine;

public class Arma : MonoBehaviour, IArma
{
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public int municionActual = 10;
    public float velocidadProyectil = 20f;

    private MovimientoPersonaje movimientoJugador;

    void Start()
    {
        movimientoJugador = FindFirstObjectByType<MovimientoPersonaje>();
    }

    public void Usar()
    {
        if (municionActual <= 0 || proyectilPrefab == null || puntoDisparo == null) return;

        Vector3 direccion = movimientoJugador != null && movimientoJugador.ultimaDireccionMovimiento != Vector3.zero
            ? movimientoJugador.ultimaDireccionMovimiento
            : puntoDisparo.forward;

        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.LookRotation(direccion));
        Rigidbody rb = proyectil.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direccion * velocidadProyectil;
        }

        municionActual--;
    }
}