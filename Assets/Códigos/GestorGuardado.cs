using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GestorGuardado : MonoBehaviour
{
    // Prefabs disponibles que se asignan en el Inspector
    public GameObject[] prefabsDisponibles;
    private static Dictionary<string, GameObject> diccionarioPrefabs;

    private static HashSet<string> objetosRecogidos = new HashSet<string>();

    void Start()
    {
       Debug.Log("Ruta de guardado: " + Application.persistentDataPath);
    }

        void Awake()
    {
        // Construir el diccionario de prefabs solo una vez
        diccionarioPrefabs = new Dictionary<string, GameObject>();

        foreach (GameObject prefab in prefabsDisponibles)
        {
            ObjetoRecogible recogible = prefab.GetComponent<ObjetoRecogible>();
            if (recogible != null && !string.IsNullOrEmpty(recogible.ID))
            {
                if (!diccionarioPrefabs.ContainsKey(recogible.ID))
                {
                    diccionarioPrefabs.Add(recogible.ID, prefab);
                }
            }
        }
    }

    private string rutaArchivo => Application.persistentDataPath + "/guardado.json";

    // Función para obtener un prefab por su ID
    public static GameObject ObtenerPrefabPorID(string id)
    {
        if (diccionarioPrefabs != null && diccionarioPrefabs.ContainsKey(id))
        {
            return diccionarioPrefabs[id];
        }
        Debug.LogWarning("No se encontró prefab con ID: " + id);
        return null;
    }

    
    // Métodos para gestionar recogidos
    public static void MarcarObjetoComoRecogido(string id)
    {
        GestorGuardado instancia = FindFirstObjectByType<GestorGuardado>();
        if (instancia == null)
        {
            Debug.LogWarning("No se encontró el GestorGuardado en la escena.");
            return;
        }
        
        DatosJugador datos = instancia.CargarDatos();
        if (!datos.objetosRecogidos.Contains(id))
        {
            datos.objetosRecogidos.Add(id);
            instancia.GuardarDatos(datos);
        }
    }

    public static bool ObjetoFueRecogido(string id)
    {
        GestorGuardado instancia = FindFirstObjectByType<GestorGuardado>();
        if (instancia == null)
        {
            Debug.LogWarning("No se encontró el GestorGuardado en la escena.");
            return objetosRecogidos.Contains(id);
        }
        
        DatosJugador datos = instancia.CargarDatos();
        if (!datos.objetosRecogidos.Contains(id))
        {
            datos.objetosRecogidos.Add(id);
            instancia.GuardarDatos(datos);
        }
        return objetosRecogidos.Contains(id);
    }

    public static List<string> ObtenerListaObjetosRecogidos()
    {
        return new List<string>(objetosRecogidos);
    }

    public static void RestaurarObjetosRecogidos(List<string> ids)
    {

        ObjetoRecogible[] todosObjetos = FindObjectsByType<ObjetoRecogible>(FindObjectsSortMode.None);
        foreach (ObjetoRecogible obj in todosObjetos)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.ID))
            {
                if (objetosRecogidos.Contains(obj.ID))
                {
                    obj.gameObject.SetActive(false);
                    Debug.Log("Objeto desactivado: " + obj.ID);
                }
                else
                {
                    obj.gameObject.SetActive(true); // Asegúrate de que reaparezca
                    Debug.Log("Objeto restaurado: " + obj.ID);
                }
            }
        }
    }

    public void GuardarDatos(DatosJugador datos)
    {
        datos.objetosRecogidos = ObtenerListaObjetosRecogidos();
        string json = JsonUtility.ToJson(datos);
        File.WriteAllText(rutaArchivo, json);
        Debug.Log("Datos guardados en: " + rutaArchivo);
    }

    public DatosJugador CargarDatos()
    {
        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);
            DatosJugador datos = JsonUtility.FromJson<DatosJugador>(json);

            // Restaurar objetos recogidos desde los datos
            RestaurarObjetosRecogidos(datos.objetosRecogidos);
            return datos;
        }
        else
        {
           Debug.LogWarning("No se encontró archivo de guardado.");
           GuardarDatos(new DatosJugador());  // Crear un archivo vacío por si no existe.
           return new DatosJugador();  // Retorna un objeto vacío. 
        }
    }
}
