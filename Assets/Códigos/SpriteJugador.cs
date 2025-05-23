using UnityEngine;

public class SpriteJugador : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [Header("Sprites de movimiento")]
    public Sprite idleFront, idleBack, idleLeft, idleRight;
    public Sprite walkFront, walkBack, walkLeft, walkRight;

    [Header("Sprites con objeto equipado")]
    public Sprite idleBotiquinFront, walkBotiquinLeft; // y todos los demás...

    private Vector3 ultimaDireccion = Vector3.down;
    private bool tieneBotiquin = false; 

    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (input != Vector3.zero)
        {
            ultimaDireccion = input;

            // Movimiento
            if (input.x > 0)
                spriteRenderer.sprite = tieneBotiquin ? walkBotiquinLeft : walkRight;
            else if (input.x < 0)
                spriteRenderer.sprite = tieneBotiquin ? walkBotiquinLeft : walkLeft;
            else if (input.z > 0)
                spriteRenderer.sprite = tieneBotiquin ? walkBotiquinLeft : walkBack;
            else if (input.z < 0)
                spriteRenderer.sprite = tieneBotiquin ? walkBotiquinLeft : walkFront;
        }
        else
        {
            // Quieto pero mirando en la última dirección
            if (ultimaDireccion.x > 0)
                spriteRenderer.sprite = tieneBotiquin ? idleBotiquinFront : idleRight;
            else if (ultimaDireccion.x < 0)
                spriteRenderer.sprite = tieneBotiquin ? idleBotiquinFront : idleLeft;
            else if (ultimaDireccion.z > 0)
                spriteRenderer.sprite = tieneBotiquin ? idleBotiquinFront : idleBack;
            else if (ultimaDireccion.z < 0)
                spriteRenderer.sprite = tieneBotiquin ? idleBotiquinFront : idleFront;
        }
    }

    public void EquiparObjeto(string nombreObjeto)
    {
        if (nombreObjeto == "Botiquin")
            tieneBotiquin = true;
        else
            tieneBotiquin = false;
    }
}