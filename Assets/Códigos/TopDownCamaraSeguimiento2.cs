using UnityEngine;

public class TopDownCamaraSeguimiento2 : MonoBehaviour
{
 public Transform target;  // El objeto que la cámara debe seguir (en este caso, el cilindro)
    public float followDistance = 20f;  // Distancia fija en el eje Y
    public float smoothSpeed = 0.125f;  // Velocidad de suavizado para hacer el movimiento más fluido

    private Vector3 offset;  // Desplazamiento de la cámara respecto al cilindro

    void Start()
    {
       // Posicionamos la cámara arriba y un poco hacia atrás del personaje,
        // pero también le damos una vista diagonal rotando el offset en Y
        float yRotationDegrees = 90f; // Grados que quieres inclinar sobre el eje Y
        Quaternion rotation = Quaternion.Euler(10f, yRotationDegrees, 0);
        offset = rotation * new Vector3(0, followDistance + 5f, -followDistance); // Arriba y hacia atrás, rotado

        // Inclinación vertical para que mire hacia el personaje desde arriba en diagonal
        transform.rotation = Quaternion.Euler(30f, yRotationDegrees, 0); // X = inclinación hacia abajo, Y = ángulo lateral
    }

    void LateUpdate()
    {
         if (target == null)
    {
        Debug.LogWarning("La cámara no tiene asignado un 'target'. Asigna el cilindro o cápsula en el Inspector.");
        return;
    }

    Vector3 desiredPosition = target.position + offset;
    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    transform.position = smoothedPosition;
    transform.LookAt(target);
    }
}