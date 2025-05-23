using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource musicaFondo;

    void Start()
    {
        if (!musicaFondo.isPlaying)
            musicaFondo.Play();
    }
}
//public class GameManager : MonoBehaviour
//{
    //public VidaJugador vidaJugador;
    //public PlayerLevel playerLevel;
    //public MovimientoPersonaje movimientoJugador;
    //public GestorGuardado gestorGuardado;
    //private InventarioJugador inventarioJugador;

    //void Awake()
    //{
        //inventarioJugador = FindFirstObjectByType<InventarioJugador>();
        //DatosJugador datos = gestorGuardado.CargarDatos();
        //if (datos != null)
        //{
            // Aplica los datos del guardado al jugador
            //vidaJugador.vidaActual = datos.vidaActual;
            //vidaJugador.Curar(0); // Actualiza los corazones
            //playerLevel.nivel = datos.nivel;
            //playerLevel.xpActual = datos.xpActual;
            //playerLevel.SendMessage("ActualizarUI");
            
            // Restaura la posición del jugador
            //Vector3 nuevaPos = new Vector3(datos.posicion[0], datos.posicion[1], datos.posicion[2]);
            //movimientoJugador.transform.position = nuevaPos;
            
            // Restaura el inventario y la munición
            //inventarioJugador.RestaurarInventario(
                //datos.objetosInventarioIDs, 
                //datos.indiceCasillaSeleccionada, 
                //datos.municionActual
            //);
            
            // Restaurar los objetos recogidos
            //GestorGuardado.RestaurarObjetosRecogidos(datos.objetosRecogidos);
            
            //Debug.Log("CARGADO (Awake): vida=" + datos.vidaActual);
        //}
        //else
        //{
            //Debug.LogWarning("No se cargaron datos. Archivo no encontrado o nulo.");
        //}
    //}

    //void Start()
    //{
    //}

    //void Update()
    //{
        //Debug.Log("GameManager activo");
        //if (Input.GetKeyDown(KeyCode.G)) // Guardar con la G
        //{
            //DatosJugador datos = new DatosJugador();
            //datos.vidaActual = vidaJugador.vidaActual;
            //datos.nivel = playerLevel.nivel;
            //datos.xpActual = playerLevel.xpActual;
            //Vector3 pos = movimientoJugador.transform.position;
            //datos.posicion = new float[] { pos.x, pos.y, pos.z };
            
            //datos.objetosInventarioIDs = inventarioJugador.ObtenerIDsObjetos();
            //datos.indiceCasillaSeleccionada = inventarioJugador.casillaSeleccionada;
            //datos.municionActual = inventarioJugador.ObtenerMunicionActual();

            //datos.objetosRecogidos = GestorGuardado.ObtenerListaObjetosRecogidos();
            
            //gestorGuardado.GuardarDatos(datos);
            //Debug.Log("GUARDADO: vida=" + datos.vidaActual + ", nivel=" + datos.nivel + ", xp=" + datos.xpActual + ", pos=" + pos);
        //}

        //if (Input.GetKeyDown(KeyCode.C)) // Cargar con la E
        //{
            //DatosJugador datos = gestorGuardado.CargarDatos();
            //if (datos != null)
            //{
                //vidaJugador.vidaActual = datos.vidaActual;
                //vidaJugador.Curar(0); // Para actualizar corazones
                //playerLevel.nivel = datos.nivel;
                //playerLevel.xpActual = datos.xpActual;
                //playerLevel.SendMessage("ActualizarUI"); // Refrescar UI

                //Vector3 nuevaPos = new Vector3(datos.posicion[0], datos.posicion[1], datos.posicion[2]);
                //movimientoJugador.transform.position = nuevaPos;

                //inventarioJugador.RestaurarInventario(datos.objetosInventarioIDs, datos.indiceCasillaSeleccionada, datos.municionActual);
                //GestorGuardado.RestaurarObjetosRecogidos(datos.objetosRecogidos);
                //Debug.Log("CARGADO: vida=" + datos.vidaActual + ", nivel=" + datos.nivel + ", xp=" + datos.xpActual + ", pos=" + nuevaPos);
            //}
            //else
            //{
                //Debug.LogWarning("No se cargaron datos. Archivo no encontrado o nulo.");
            //}
        //}
    //}
//}