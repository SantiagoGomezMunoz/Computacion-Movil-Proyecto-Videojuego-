using UnityEngine;

public class EnemigoSeguidor : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad = 3.0f;
    public float rangoVision = 10f;
    public float rangoPerdida = 15f;

    private bool persiguiendo = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (objetivo == null) return;

        float distancia = Vector3.Distance(transform.position, objetivo.position);
        if (!persiguiendo && distancia <= rangoVision)
        {
            persiguiendo = true;
        }
        else if (persiguiendo && distancia >= rangoPerdida)
        {
            persiguiendo = false;
        }
    }

    void FixedUpdate()
    {
        if (persiguiendo && objetivo != null)
        {
            Vector3 direccion = (objetivo.position - transform.position).normalized;
            Vector3 nuevaPos = transform.position + direccion * velocidad * Time.fixedDeltaTime;
            rb.MovePosition(nuevaPos);
        }
    }

    public bool EstaPersiguiendo()
    {
        return persiguiendo;
    }

    public void RecibirDaño(int daño)
    {
        // Método vacío solo para evitar errores en pruebas
    }
}