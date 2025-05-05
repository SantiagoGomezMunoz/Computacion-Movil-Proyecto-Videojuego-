using UnityEngine;
using System.Collections.Generic;

public class MovimientoPersonaje : MonoBehaviour
{
    public float velocidad = 5f;
    private Rigidbody rb;

    private bool direccionBloqueada = false;
    private Vector3 normalColision = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        Vector3 direccion = new Vector3(inputZ, 0f, -inputX).normalized;

        // Si hay colisión, comprobamos si el jugador quiere empujar contra ella
        if (direccionBloqueada)
        {
            float empuje = Vector3.Dot(direccion, -normalColision);
            if (empuje > 0.5f)
            {
                // Está empujando hacia la colisión → bloquear movimiento
                return;
            }
            else
            {
                // Se está moviendo en otra dirección → desbloquear
                direccionBloqueada = false;
                normalColision = Vector3.zero;
            }
        }

        rb.MovePosition(rb.position + direccion * velocidad * Time.deltaTime);
    }

    /*void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Casa"))
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                Vector3 normal = contact.normal;
                float empuje = Vector3.Dot(ultimaDireccion(), -normal);

                if (empuje > 0.5f)
                {
                    direccionBloqueada = true;
                    normalColision = normal;
                    return;
                }
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Casa"))
        {
            direccionBloqueada = false;
            normalColision = Vector3.zero;
        }
    }

    Vector3 ultimaDireccion()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        return new Vector3(inputZ, 0f, -inputX).normalized;
    }*/
}