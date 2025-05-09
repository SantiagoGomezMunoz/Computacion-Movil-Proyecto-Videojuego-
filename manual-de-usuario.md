---
description: >-
  Guía que le explicará al jugador cómo jugar al videojuego de la mejor forma
  sin complicaciones en el proc
icon: book-open-cover
---

# Manual de usuario

1. **Introducción:**\
   \
   Es el año 2060. Un mundo azotado por un apocalipsis provocado por una superinteligencia artificial, capaz de fabricar sus propios androides con conciencia propia, los humanos por su dependencia a la IA no fueron capaces de contrarrestar la amenaza, un único superviviente saldrá de su pueblo natal para encontrarse con los vestigios de la tragedia, abriéndose paso entre enemigos para llegar a la gran ciudad y encontrar más respuestas.\

2. **Pantalla de título:**

<figure><img src=".gitbook/assets/image (9).png" alt="" width="563"><figcaption><p>Pantalla de título, mostrando la versión 0.5 del desarrollo de CypherCTRL</p></figcaption></figure>

Para la versión en la que el videojuego se encuentra (Versión 0.5), la pantalla de título solo cuenta con el logo del videojuego, un fondo reflejando una ciudad desolada y dos botones, Jugar (Enviará al jugador directamente al escenario principal del juego) y Salir (Saldrá del juego). \
<sup>La documentación irá cambiando a medida que el juego se acerque a su Versión 1.0, para mantenerla actualizada con los cambios más actuales.</sup>&#x20;

1. **Controles:**\
   \
   **Moverse:** Teclas W (Arriba), A (Derecha), S (Abajo), D (Izquierda)\
   **Atacar (disparar o golpear):** Clic Izquierdo del mouse\
   **Usar objeto**: Tecla E\
   **Cambiar de objeto en el inventario:** Tecla 1, 2, 3\
   **Recoger Objeto:** Caminar por encima de él\
   **Soltar Objeto:** Tecla Q\

2. **Elementos del HUD (Interfaz):**

**Contenedores de vida:** En la parte superior izquierda del la interfaz, mostrando cinco corazones como vida máxima del jugador.

<figure><img src=".gitbook/assets/image (1).png" alt=""><figcaption><p>Salud del personaje</p></figcaption></figure>

**Inventario:** En la parte derecha inferior se cuentan con tres espacios para almacenar objetos, seleccionables con la tecla 1, 2 y 3.

<figure><img src=".gitbook/assets/image (2).png" alt=""><figcaption><p>Inventario del personaje</p></figcaption></figure>

**Contador de munición:** Contador numérico funcional que aparece en la parte inferior izquierda y que indica la munición máxima y restante, únicamente visible al momento de seleccionar un arma de fuego en el inventario.

<figure><img src=".gitbook/assets/image (3).png" alt=""><figcaption><p>Contador de munición</p></figcaption></figure>

**Barra de experiencia:** En la parte superior hay una barra de experiencia la cual va aumentando de nivel al jugador a medida que este derrota enemigos.

<figure><img src=".gitbook/assets/image (4).png" alt=""><figcaption><p>Barra de experiencia</p></figcaption></figure>

4. **Objetos Obtenibles:**

**Fusil de asalto (arma a larga distancia):** Un arma útil para atacar a los enemigos a distancias lejanas, cuenta con un cargador máximo de 30 balas, y dispara proyectiles de manera semi-automática (proyectil por proyectil).

<figure><img src=".gitbook/assets/image (5).png" alt=""><figcaption><p>Fusil</p></figcaption></figure>

**Hacha (arma a corta distancia):** Un arma útil para los enfrentamientos a cortas distancia, provocando daño de área a enemigos muy cercanos entre sí. Es la primera arma que el jugador obtiene, necesaria para poder avanzar a las siguientes secciones.

<figure><img src=".gitbook/assets/image (6).png" alt=""><figcaption><p>Hacha</p></figcaption></figure>

**Botiquín (Objeto curativo):** Un objeto útil para recuperar vitalidad en caso tal de que los enemigos hayan lastimado al jugador, tiene un único uso y recuera dos corazones de vitalidad.

<figure><img src=".gitbook/assets/image (8).png" alt=""><figcaption><p>Botiquín</p></figcaption></figure>

5. **Sistema de Juego:**

**Combate:**\
\- Se pueden recoger armas de fuego y disparar con ella solo si el jugador las tiene equipadas y seleccionadas en su inventario.\
\- Si la munición del arma llega a 0, no se va a poder disparar y se tendrá que buscar una nueva.\
\- El hacha es única y puede ser llevada durante todo el juego si el jugador desea conservarla.\
\
**Vitalidad:**\
\- El jugador inicia su aventura con cinco corazones.\
\- Si el enemigo toca al jugador, le quitará un corazón de vida con un tiempo entre ataque de dos segundos.\
\- El jugador puede usar un objeto curativo (Tecla E) para recuperar corazones, solo si tiene menos de 5 corazones.\
\
**Inventario:**\
\- Tiene una capacidad máxima de 3 objetos.\
\- Al obtener un nuevo objeto, será añadido a un slot (espacio) disponible.\
\- El jugador puede soltar un objeto para recoger otro con la tecla Q.\
\
**Exploración:** \
\- El jugador deberá explorar su entorno para encontrar objetos que le sean útiles, como más objetos curativos o armas de fuego nuevas con munición.\


6. **Inteligencia Artificial:**

Los enemigos patrullan el escenario del juego de forma aleatoria hasta que el jugador entre en su rango de visión y automáticamente empiecen a seguirlo para atacarlo. Si el jugador se aleja lo suficiente, los enemigos dejan de seguirlo y vuelven a su patrullaje.\
Si el enemigo sufre daño, cambiará de color para indicar al jugador que fueron dañados por él y serán empujados atrás como efecto al golpe.\


7. **Subida de Nivel:**

Derrotar enemigos le dará experiencia al jugador, la cual servirá para subir de nivel. Al llenar la barra de experiencia el jugador subirá de nivel, a medida que suba de nivel, la barra de llenará más lento.\
\
**Consejos y Advertencias:**

* Si el jugador pierde toda su vitalidad, será regresado al punto de inicio ya que el videojuego no cuenta con sistema de guardado de progreso. Sin embargo, contará con todos sus objetos y progreso, solo deberá volver al mismo lugar donde perdió la partida y continuar.&#x20;
* Es recomendable completar el Objetivo Secundario de la primera sección del mapa, ya que los objetos recolectados serán útiles para más adelante, en las demás secciones del juego.
* Siempre es buena idea explorar el mundo del juego, ya que el jugador podría encontrar un arma con más munición o algún objeto curativo para recuperar su vitalidad.
* Si el jugador no cuenta con armas y tiene enemigos cerca, lo mejor es perderlos de vista y esperar a que sea seguro avanzar a la zona donde haya una arma para recolectarla.

**Posibles preguntas:**

**¿Puedo completar el juego solo portando el hacha?**\
**R/** Si, es posible pero algo complicado si no se cuenta con el arma de fuego o algún objeto curativo, un desafío solo para los jugadores más experimentados.

**¿Puedo organizar mi inventario a mi gusto?**\
**R/** Si, si tienes tu inventario completo y por ejemplo quieres el arma de fuego en el primer espacio pero lo tienes en el segundo espacio, con la tecla Q suelta el arma de fuego que tienes en el segundo espacio y suelta el objeto que tengas en el primer espacio, ahora recoge el arma de fuego de primero y el otro objeto de segundo. Ahora el arma de fuego estará en el espacio que quieres.

**¿Cuál es el objetivo de este juego?**\
**R/** Este videojuego quiere crear consciencia sobre la dependencia que la población mundial está generando a las Inteligencias Artificiales, debido a que la sociedad actual utiliza la IA incluso para las tareas más sencillas. Presenta un escenario futurista no muy lejano donde los humanos por su dependencia no fueron capaces de hacer frente a la rebelión de las IA's siendo la gran mayoría de la especie humana erradicada por una Super Inteligencia Artificial, un escenario que aunque de película. No es imposible.&#x20;

**Requisitos mínimos y recomendados del Sistema:**

**Sistema operativo:** Windows 10 o superior\
**Procesador:** Intel Core i3 o su equivalente en AMD\
**Memoria RAM:** 4GB\
**Tarjeta gráfica:** Intel HD Graphics 4000 o superior\
**Almacenamiento:** 500MB disponibles\
\
**Requisitos recomendados:**\
**Sistema operativo:** Windows 10 o superior\
**Procesador:** Intel Core i5 (6 núcleos) o su equivalente en AMD\
**Memoria RAM:** 8GB\
**Tarjeta Gráfica:** NVIDIA GTX 1050 Ti\
**Almacenamiento:** Al menos 1GB disponible\


