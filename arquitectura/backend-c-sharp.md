---
description: >-
  El backend es la parte que el usuario no puede ver, la parte funcional, la
  lógica y el procesamiento del software. Permitiendo su buen funcionamiento.
---

# Backend (C Sharp)

El backend del videojuego fue hecho utilizando C Sharp para cada uno de los scripts que sirven de componentes para los diferentes GameObjects y les brindan sus respectivas funcionalidades, funcionalidades que pueden ser exclusivas de un solo Objeto o compartidas por varios al mismo tiempo. Unity se encarga de ejecutar este código `C#` como lógica del juego.&#x20;

El backend en general del videojuego se estructuran de la clase principal `MonoBehaviour`, permitiendo que el script en cuestión pueda ser agregado como componente a un objeto dentro de la escena, proporcionando el acceso a las funciones que el script contenga y que el motor permita. &#x20;

Ejemplo: Sistema de combate

Este sistema le permite al jugador disparar varios proyectiles únicamente si posee un arma equipada en su inventario, en el momento en que el jugador presiona el click izquierdo, un proyectil sale del jugador en la dirección en la que este mire.&#x20;

Para ello se necesitan dos componentes:

Arma.cs: Su función es controlar la generación de los proyectiles y reducir la munición cuando el jugador dispare.

public void Disparar()\
{\
if (municionActual <= 0 || proyectilPrefab == null || puntoDisparo == null) return;

<pre><code><strong>
</strong>public void Disparar()
{
if (municionActual &#x3C;= 0 || proyectilPrefab == null || puntoDisparo == null) return;
<strong>
</strong><strong>Vector3 direccionDisparo = movimientoJugador?.ultimaDireccionMovimiento ?? puntoDisparo.forward;
</strong>
GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.LookRotation(direccionDisparo));
Rigidbody rb = proyectil.GetComponent&#x3C;Rigidbody>();

if (rb != null)
{
    rb.linearVelocity = direccionDisparo * velocidadProyectil;
}

municionActual--;
</code></pre>

}
