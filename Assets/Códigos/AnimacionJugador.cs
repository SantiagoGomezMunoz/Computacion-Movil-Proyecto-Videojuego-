using UnityEngine;

[System.Serializable]
public class SpritesPorDireccion
{
    public Sprite[] spritesAbajo;
    public Sprite[] spritesArriba;
    public Sprite[] spritesDerecha;
    public Sprite[] spritesIzquierda;
    // joystick no hace sentido aquí (lo tenías en el struct anterior), lo manejamos fuera si hace falta
}

[System.Serializable]
public class PersonajeSprites
{
    [Header("Sprites base (sin objetos)")]
    public SpritesPorDireccion baseSprites;

    [Header("Versiones con objetos (opcionales). Si están vacías, cae al baseSprites")]
    public SpritesPorDireccion botiquinSprites;
    public SpritesPorDireccion hachaSprites;
    public SpritesPorDireccion armaSprites;
    public SpritesPorDireccion escopetaSprites;
}

public enum TipoItemEquipado
{
    Ninguno,
    Botiquin,
    Hacha,
    Arma,
    Escopeta
}

[RequireComponent(typeof(SpriteRenderer))]
public class AnimacionJugador : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [Header("Todos los personajes (ej: length = 4)")]
    public PersonajeSprites[] personajes;

    [Header("Velocidad animación")]
    public float velocidadAnimacion = 0.2f;

    // estado runtime
    private PersonajeSprites personajeActualSprites;
    private float animTimer;
    private int animFrame;
    private Vector2 ultimaDireccion = Vector2.down;

    // cache de movimiento (tu script MovimientoPersonaje debe exponer Direction/ultimaDireccion compatible)
    private MovimientoPersonaje movimiento;

    [Header("Item equipado actual (puede ser modificado por InventarioJugador)")]
    public TipoItemEquipado itemEquipado = TipoItemEquipado.Ninguno;

    void Awake()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        movimiento = GetComponentInParent<MovimientoPersonaje>();

        // seleccionar personaje guardado (si existe); si no, 0
        int seleccionado = PlayerPrefs.GetInt("SelectedCharacter", 0);
        seleccionado = Mathf.Clamp(seleccionado, 0, Mathf.Max(0, personajes.Length - 1));

        if (personajes != null && personajes.Length > 0 && seleccionado < personajes.Length)
            personajeActualSprites = personajes[seleccionado];
        else
            personajeActualSprites = null;

        // set inicial
        SpritesPorDireccion inicio = ObtenerSpritesActivos();
        if (inicio != null && inicio.spritesAbajo != null && inicio.spritesAbajo.Length > 0)
            spriteRenderer.sprite = inicio.spritesAbajo[0];
    }

    void Update()
    {
        Vector2 direccion = Vector2.zero;
        // intenta obtener la dirección desde tu script MovimientoPersonaje
        if (movimiento != null)
        {
            // Asegúrate que MovimientoPersonaje exponga alguna propiedad pública con la dirección de movimiento.
            // Ejemplo asumido: public Vector2 DireccionMovimiento { get; }
            direccion = movimiento.DireccionMovimiento;
        }

        // corregir orientación para que coincida con tu sistema (igual que antes)
        Vector2 direccionCorregida = new Vector2(-direccion.y, direccion.x);

        if (direccionCorregida.magnitude > 0.1f)
        {
            ultimaDireccion = direccionCorregida;
            animTimer += Time.deltaTime;
            if (animTimer >= velocidadAnimacion)
            {
                animTimer = 0f;
                animFrame = (animFrame + 1) % 8; // permitimos hasta 8 frames por seguridad; se recorta después
            }
            ActualizarSprite();
        }
        else
        {
            animFrame = 0;
            ActualizarSprite();
        }
    }

    // Devuelve el SpritesPorDireccion que corresponde según personaje + item equipado (con fallback al base)
    SpritesPorDireccion ObtenerSpritesActivos()
    {
        if (personajeActualSprites == null) return null;

        switch (itemEquipado)
        {
            case TipoItemEquipado.Botiquin:
                if (personajeActualSprites.botiquinSprites != null &&
                    TieneAlguno(personajeActualSprites.botiquinSprites)) return personajeActualSprites.botiquinSprites;
                break;
            case TipoItemEquipado.Hacha:
                if (personajeActualSprites.hachaSprites != null &&
                    TieneAlguno(personajeActualSprites.hachaSprites)) return personajeActualSprites.hachaSprites;
                break;
            case TipoItemEquipado.Arma:
                if (personajeActualSprites.armaSprites != null &&
                    TieneAlguno(personajeActualSprites.armaSprites)) return personajeActualSprites.armaSprites;
                break;
            case TipoItemEquipado.Escopeta:
                if (personajeActualSprites.escopetaSprites != null &&
                    TieneAlguno(personajeActualSprites.escopetaSprites)) return personajeActualSprites.escopetaSprites;
                break;
        }

        // fallback al base
        return personajeActualSprites.baseSprites;
    }

    // comprueba si el SpritesPorDireccion tiene al menos un arreglo con elementos
    bool TieneAlguno(SpritesPorDireccion s)
    {
        if (s == null) return false;
        if ((s.spritesAbajo != null && s.spritesAbajo.Length > 0) ||
            (s.spritesArriba != null && s.spritesArriba.Length > 0) ||
            (s.spritesDerecha != null && s.spritesDerecha.Length > 0) ||
            (s.spritesIzquierda != null && s.spritesIzquierda.Length > 0))
            return true;
        return false;
    }

    void ActualizarSprite()
    {
        SpritesPorDireccion usados = ObtenerSpritesActivos();
        if (usados == null) return;

        Sprite[] sprites = usados.spritesAbajo;

        if (Mathf.Abs(ultimaDireccion.x) > Mathf.Abs(ultimaDireccion.y))
        {
            sprites = ultimaDireccion.x > 0 ? usados.spritesDerecha : usados.spritesIzquierda;
        }
        else
        {
            sprites = ultimaDireccion.y > 0 ? usados.spritesArriba : usados.spritesAbajo;
        }

        if (sprites == null || sprites.Length == 0)
        {
            // si no hay sprites en la dirección elegida, intenta usar spritesAbajo como fallback
            sprites = usados.spritesAbajo;
            if (sprites == null || sprites.Length == 0) return;
        }

        // asegurar que animFrame esté dentro de rango
        if (animFrame >= sprites.Length) animFrame = 0;
        if (animFrame < 0) animFrame = 0;

        spriteRenderer.sprite = sprites[animFrame];
    }

    // Llamar cuando el jugador recoja o equipe un ítem (desde InventarioJugador)
    public void EquiparItem(TipoItemEquipado nuevoItem)
    {
        itemEquipado = nuevoItem;
        animFrame = 0;
        ActualizarSprite();
    }

    public void DesequiparItem()
    {
        itemEquipado = TipoItemEquipado.Ninguno;
        animFrame = 0;
        ActualizarSprite();
    }

    // Llamable desde fuera si cambias el personaje seleccionado en runtime (p. ej. selección)
    public void CambiarPersonajeSeleccionado(int indice)
    {
        if (personajes == null || personajes.Length == 0) return;
        int i = Mathf.Clamp(indice, 0, personajes.Length - 1);
        personajeActualSprites = personajes[i];
        animFrame = 0;
        ActualizarSprite();
    }
}