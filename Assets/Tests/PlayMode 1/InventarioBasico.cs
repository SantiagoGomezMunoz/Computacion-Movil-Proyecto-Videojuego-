using UnityEngine;

public class InventarioBasico : MonoBehaviour
{
    public GameObject[] objetos = new GameObject[3];

    public bool AgregarObjeto(GameObject obj)
    {
        for (int i = 0; i < objetos.Length; i++)
        {
            if (objetos[i] == null)
            {
                objetos[i] = obj;
                return true;
            }
        }
        return false;
    }

    public void Vaciar()
    {
        for (int i = 0; i < objetos.Length; i++)
        {
            objetos[i] = null;
        }
    }
}