using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovimientoPersonaje : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public Joystick joystick; // opcional

    [Header("Audio")]
    public AudioSource pasosAudio;
    public AudioClip sonidoPasos;

    [Header("Debug")]
    public bool debugLogs = false;

    public Vector2 DireccionMovimiento { get; private set; }
    public Vector3 ultimaDireccionMovimiento { get; private set; } = Vector3.forward;

    private Rigidbody rb;
    private Vector3 direccionActual = Vector3.zero;

    // colección de normales de contacto vigentes
    private List<Vector3> contactoNormals = new List<Vector3>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        float jx = (joystick != null) ? joystick.Horizontal : 0f;
        float jz = (joystick != null) ? joystick.Vertical   : 0f;
        float kx = Input.GetAxisRaw("Horizontal");
        float kz = Input.GetAxisRaw("Vertical");

        float x = Mathf.Abs(jx) > 0.05f ? jx : kx;
        float z = Mathf.Abs(jz) > 0.05f ? jz : kz;

        direccionActual = new Vector3(z, 0f, -x); // tu mapeo para vista aérea
        if (direccionActual.magnitude > 1f) direccionActual.Normalize();

        DireccionMovimiento = new Vector2(direccionActual.x, direccionActual.z);

        bool seEstaMoviendo = direccionActual.magnitude > 0.1f;
        if (seEstaMoviendo)
        {
            ultimaDireccionMovimiento = direccionActual;
            Quaternion rotObj = Quaternion.LookRotation(direccionActual);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotObj, 0.2f);
        }

        // audio pasos
        if (pasosAudio != null && sonidoPasos != null)
        {
            if (seEstaMoviendo && !pasosAudio.isPlaying)
            {
                pasosAudio.clip = sonidoPasos;
                pasosAudio.loop = true;
                pasosAudio.Play();
            }
            else if (!seEstaMoviendo && pasosAudio.isPlaying)
            {
                pasosAudio.Stop();
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 movimientoDeseado = direccionActual;
        if (movimientoDeseado.magnitude <= 0.01f) return;

        // si estamos tocando colisiones, sacamos la normal promedio
        if (contactoNormals.Count > 0)
        {
            Vector3 avg = Vector3.zero;
            for (int i = 0; i < contactoNormals.Count; i++) avg += contactoNormals[i];
            avg /= contactoNormals.Count;
            avg.Normalize();

            // dot > 0 => la dirección deseada apunta hacia dentro de la pared? (hacia -avg)
            float dot = Vector3.Dot(movimientoDeseado.normalized, -avg);

            if (dot > 0.05f)
            {
                // Si se intenta moverse *contra* la pared, proyectamos para deslizar
                Vector3 proyectado = Vector3.ProjectOnPlane(movimientoDeseado, avg);
                // si la proyección es muy pequeña, permitimos tiny desacoplo para salir (prevent locking)
                if (proyectado.magnitude < 0.01f)
                {
                    // permitir retroceder si es necesario
                    if (Vector3.Dot(movimientoDeseado, avg) < 0f)
                    {
                        proyectado = movimientoDeseado; // movimiento que se aleja de la pared
                    }
                }
                movimientoDeseado = proyectado;
            }
        }

        Vector3 desplaz = movimientoDeseado * velocidad * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + desplaz);
    }

    private void OnCollisionEnter(Collision collision)
    {
        UpdateContactNormals(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        UpdateContactNormals(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        // al salir de un collider, borramos todos y dejaremos OnCollisionStay re-popular si sigue habiendo colisiones
        contactoNormals.Clear();
    }

    private void UpdateContactNormals(Collision collision)
    {
        contactoNormals.Clear();
        ContactPoint[] points = collision.contacts;
        for (int i = 0; i < points.Length; i++)
        {
            contactoNormals.Add(points[i].normal);
        }
    }
    
    public void RealizarAccionPrincipal()
    {
        // Lo que quieras que pase cuando NO hay NPC cerca.
        // // Ejemplo: atacar, talar árbol, usar herramienta, etc.
        Debug.Log("Realizando acción principal del jugador...");
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

