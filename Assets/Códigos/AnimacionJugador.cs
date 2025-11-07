using UnityEngine;

[System.Serializable]
public class SpritesPorDireccion
{
    public Sprite[] spritesAbajo;
    public Sprite[] spritesArriba;
    public Sprite[] spritesDerecha;
    public Sprite[] spritesIzquierda;
    public Joystick joystick;
}

public enum TipoItemEquipado
{
    Ninguno,
    Botiquin,
    Hacha,
    Arma,
    Escopeta   
}

public class AnimacionJugador : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public SpritesPorDireccion spritesBase;
    public SpritesPorDireccion spritesBotiquin;
    public SpritesPorDireccion spritesHacha;
    public SpritesPorDireccion spritesArma;
    public SpritesPorDireccion spritesEscopeta; 

    public float velocidadAnimacion = 0.2f;

    private float animTimer;
    private int animFrame;
    private Vector2 ultimaDireccion = Vector2.down;

    private MovimientoPersonaje movimiento;

    public TipoItemEquipado itemEquipado = TipoItemEquipado.Ninguno;

    void Start()
    {
        movimiento = GetComponentInParent<MovimientoPersonaje>();
        if (spritesBase != null && spritesBase.spritesAbajo.Length > 0)
            spriteRenderer.sprite = spritesBase.spritesAbajo[0];
    }

    void Update()
    {
        Vector2 direccion = movimiento != null ? movimiento.DireccionMovimiento : Vector2.zero;

        // Orientación corregida para que coincida con el joystick
        Vector2 direccionCorregida = new Vector2(-direccion.y, direccion.x);

        if (direccionCorregida.magnitude > 0.1f)
        {
            ultimaDireccion = direccionCorregida;

            animTimer += Time.deltaTime;
            if (animTimer >= velocidadAnimacion)
            {
                animTimer = 0;
                animFrame = (animFrame + 1) % 4;
            }

            ActualizarSprite();
        }
        else
        {
            animFrame = 0;
            ActualizarSprite();
        }
    }

    void ActualizarSprite()
    {
        SpritesPorDireccion spritesActuales = spritesBase;

        switch (itemEquipado)
        {
            case TipoItemEquipado.Botiquin:
                spritesActuales = spritesBotiquin;
                break;
            case TipoItemEquipado.Hacha:
                spritesActuales = spritesHacha;
                break;
            case TipoItemEquipado.Arma:
                spritesActuales = spritesArma;
                break;
            case TipoItemEquipado.Escopeta:
                spritesActuales = spritesEscopeta;
                break;
        }

        Sprite[] sprites = spritesActuales != null ? spritesActuales.spritesAbajo : null;

        if (spritesActuales != null)
        {
            if (Mathf.Abs(ultimaDireccion.x) > Mathf.Abs(ultimaDireccion.y))
            {
                sprites = ultimaDireccion.x > 0 ? spritesActuales.spritesDerecha : spritesActuales.spritesIzquierda;
            }
            else
            {
                sprites = ultimaDireccion.y > 0 ? spritesActuales.spritesArriba : spritesActuales.spritesAbajo;
            }
        }

        if (sprites != null && sprites.Length > 0)
        {
            if (animFrame >= sprites.Length) animFrame = 0;
            spriteRenderer.sprite = sprites[animFrame];
        }
    }

    // Llamar cuando el jugador recoja o equipe un ítem
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
}