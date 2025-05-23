using UnityEngine;

public class ArmaCuerpoACuerpo : MonoBehaviour, IArma
{
    public float rangoAtaque = 2f;
    public int daño = 1;
    public Transform puntoAtaque;
    public LayerMask capaEnemigos;

    public void Usar()
    {
        Collider[] enemigos = Physics.OverlapSphere(puntoAtaque.position, rangoAtaque, capaEnemigos);

        foreach (Collider enemigo in enemigos)
        {
            EnemigoSeguidor enemigoScript = enemigo.GetComponent<EnemigoSeguidor>();
            if (enemigoScript != null)
            {
                enemigoScript.RecibirDaño(daño);
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