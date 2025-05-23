using UnityEngine;

public class SpriteEnemigo : MonoBehaviour
{
    public Sprite[] arribaSprites;
    public Sprite[] abajoSprites;
    public Sprite[] izquierdaSprites;
    public Sprite[] derechaSprites;

    public float tiempoEntreFrames = 0.2f; // Tiempo entre cambios de sprite
    private float temporizador = 0f;

    private SpriteRenderer sr;
    private int frameActual = 0;
    private Vector3 ultimaPosicion;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ultimaPosicion = transform.parent.position;
    }

    private void Update()
    {
        temporizador += Time.deltaTime;

        // Solo cambiar el sprite si ha pasado suficiente tiempo
        if (temporizador >= tiempoEntreFrames)
        {
            temporizador = 0f;
            ActualizarSprite();
        }
    }

    void ActualizarSprite()
    {
        Vector3 direccion = (transform.parent.position - ultimaPosicion).normalized;

        Sprite[] sprites = abajoSprites; // Por defecto

        if (Mathf.Abs(direccion.x) > Mathf.Abs(direccion.z))
        {
            if (direccion.x > 0) sprites = derechaSprites;
            else sprites = izquierdaSprites;
        }
        else
        {
            if (direccion.z > 0) sprites = arribaSprites;
            else sprites = abajoSprites;
        }

        sr.sprite = sprites[frameActual];
        frameActual = (frameActual + 1) % sprites.Length;

        ultimaPosicion = transform.parent.position;
    }
}