using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(-25, -90, 0); // Siempre fija la rotación
    }
}