using UnityEngine;

public class TopDownCamaraSeguimiento2 : MonoBehaviour
{
    public Transform target; // El cilindro que la camara debe seguir
    public float followDistance = 20f;  // Distancia fija en el eje Y
    public float smoothSpeed = 0.125f;  // Velocidad suavizada

    private Vector3 offset;  // Desplazamiento de la cámara respecto al cilindro

    void Start()
    {
        float yRotationDegrees = 90f; // Grados de inclinación sobre el eje Y
        Quaternion rotation = Quaternion.Euler(10f, yRotationDegrees, 0);
        offset = rotation * new Vector3(0, followDistance + 5f, -followDistance); // Arriba y hacia atrás, rotado

        // Inclinación vertical 
        transform.rotation = Quaternion.Euler(30f, yRotationDegrees, 0); // X = inclinación hacia abajo, Y = ángulo lateral
    }

    void LateUpdate()
    {
         if (target == null)
    {
        Debug.LogWarning("La cámara no tiene asignado un 'target'");
        return;
    }

    Vector3 desiredPosition = target.position + offset;
    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    transform.position = smoothedPosition;

    Vector3 direction = target.position - transform.position;
    Quaternion rotation = Quaternion.LookRotation(direction);
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, smoothSpeed);
    }
}
