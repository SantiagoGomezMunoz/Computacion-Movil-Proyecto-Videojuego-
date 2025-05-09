---
description: >-
  El backend es la parte que el usuario no puede ver, la parte funcional, la
  lógica y el procesamiento del software. Permitiendo su buen funcionamiento.
---

# Backend (Lógica del videojuego)

El término backend tampoco aplica del todo bien al desarrollo de videojuegos como sí se hace en el desarrollo web, por lo que se le llamará **Lógica del juego.**

La lógica del videojuego fue hecho utilizando C Sharp para cada uno de los scripts que sirven de componentes para los diferentes GameObjects y les brindan sus respectivas funcionalidades, funcionalidades que pueden ser exclusivas de un solo Objeto o compartidas por varios al mismo tiempo. Unity se encarga de ejecutar este código `C#` como lógica del juego.&#x20;

La lógica en general del videojuego se estructuran de la clase principal `MonoBehaviour`, permitiendo que el script en cuestión pueda ser agregado como componente a un objeto dentro de la escena, proporcionando el acceso a las funciones que el script contenga y que el motor permita. &#x20;

**Ejemplo: Sistema de combate**

Este sistema le permite al jugador disparar varios proyectiles únicamente si posee un arma equipada en su inventario, en el momento en que el jugador presiona el click izquierdo, un proyectil sale del jugador en la dirección en la que este mire.&#x20;

Para ello se necesitan dos componentes:

**`ArmaADistancia.cs`:** Su función es controlar la generación de los proyectiles y reducir la munición cuando el jugador dispare.

```csharp
    public void Usar()
    {
        if (municionActual <= 0 || proyectilPrefab == null || puntoDisparo == null) return;

        Vector3 direccionDisparo = Vector3.forward;

        // Obtener la dirección desde el script de movimiento
        if (movimientoJugador != null)
        {
            direccionDisparo = movimientoJugador.ultimaDireccionMovimiento;
        }

        if (direccionDisparo == Vector3.zero)
        {
            direccionDisparo = puntoDisparo.forward; // fallback
        }

        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.LookRotation(direccionDisparo));
        Rigidbody rb = proyectil.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = direccionDisparo * velocidadProyectil;
        }

        municionActual--;
    }
}
```

**`Proyectil.cs`:** Se encarga de definir el comportamiento del proyectil al disparar, destruyéndose de forma automática al cabo de unos segundos o instantáneamente al impactar con un enemigo.

```csharp
void OnCollisionEnter(Collision collision)
{
    // Verifica si el objeto tiene el tag "Enemigo"
    if (collision.gameObject.CompareTag("Enemigo"))
    {
        EnemigoSeguidor enemigo = collision.gameObject.GetComponent<EnemigoSeguidor>();
        if (enemigo != null)
        {
            enemigo.RecibirDaño(daño); // Aplica daño configurable
        }

        Destroy(gameObject); // Destruye el proyectil tras impactar
    }
}
```

Ambos scripts deben comunicarse con otros componentes para funcionar. `Arma.cs` con `MovimientoPersonaje` para saber en qué dirección el disparo saldrá y `Proyectil.cs` se comunica con `EnemigoSeguidor` para aplicarle el daño.
