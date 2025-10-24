using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPersonajeData", menuName = "Personaje/Personaje Data")]
public class PersonajeData : ScriptableObject
{
    [Header("Identificación del Personaje")]
    public string personajeNombre;

    [Header("Sprites de Movimiento (Idle)")]
    public Sprite idleFront;
    public Sprite idleBack;
    public Sprite idleLeft;
    public Sprite idleRight;

    [Header("Sprites de Movimiento (Caminando)")]
    public Sprite walkFront1;
    public Sprite walkFront2;
    public Sprite walkBack1;
    public Sprite walkBack2;
    public Sprite walkLeft1;
    public Sprite walkLeft2;
    public Sprite walkRight1;
    public Sprite walkRight2;

    [Header("Sprites con objeto equipado")]
    public Sprite idleBotiquinFront;
    public Sprite walkBotiquinLeft;
}
