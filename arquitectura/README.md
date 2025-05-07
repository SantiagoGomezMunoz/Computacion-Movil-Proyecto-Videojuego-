---
icon: shovel
---

# Arquitectura

El videojuego CypherCTRL hace uso de una arquitectura basada en componentes en Unity, motor en cual el videojuego se está desarrollando. En donde cada GameObject (Personaje jugador, NPC's, Objetos obtenibles, etc)  se compone y hace uso de uno o más de un script el cual le brinda tanto su comportamiento como funcionalidades.&#x20;

Por poner un ejemplo, el jugador cuenta con varios componentes en su haber, los cuales definen su funcionamiento características como `MovimientoJugador`, `Vida` e `Inventario`. Pudiendo interactuar con demás componentes como `Arma` o `Enemigo`.&#x20;

A continuación se detallará más acerca de lo que usuario verá y con lo que interactuará.
