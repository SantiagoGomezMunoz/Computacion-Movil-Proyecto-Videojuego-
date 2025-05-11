using UnityEngine;
using System.Collections.Generic;

public class MovimientoPersonaje : MonoBehaviour
{
    public float velocidad = 5f;
    private Rigidbody rb;

    private bool direccionBloqueada = false;
    private Vector3 normalColision = Vector3.zero;
    public Vector3 ultimaDireccionMovimiento { get; private set; } = Vector3.forward;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        Vector3 direccion = new Vector3(inputZ, 0f, -inputX).normalized;
    
        if (direccion.magnitude > 0.1f)
        {
            ultimaDireccionMovimiento = direccion;
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, 0.2f);
        }


        // Si hay colisión, se mira si el jugador va en contra de ella
        if (direccionBloqueada)
        {
            float dot = Vector3.Dot(direccion, -normalColision);
            if (dot > 0.5f)
            {
                // Si está tocando la colisión, detiene el movimiento
                return;
            }
            else
            {
            direccionBloqueada = false;
            normalColision = Vector3.zero;

                // Si NO está tocando la colisión, sigue el movimiento
                direccionBloqueada = false;
                normalColision = Vector3.zero;
            }
        }
            
        rb.MovePosition(rb.position + direccion * velocidad * Time.deltaTime);
    }
      
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
//}

