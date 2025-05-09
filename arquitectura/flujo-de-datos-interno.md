---
description: >-
  Se trata de cómo la información se mueve y se procesa dentro del mismo
  videojuego, teniendo en cuenta apartados como la entrada del jugador hasta la
  salida visual.
---

# Flujo de Datos Interno

El videojuego CypherCTRL funciona y opera haciendo uso de un flujo de información entre los scripts de los componentes. Permitiendo que el videojuego funcione de una forma coherente y dinámica incluso sin disponer de un conexión a internet ni servidores externos para su ejecución, siendo un videojuego totalmente Offline y para un solo jugador. Los datos incluyen en su haber entradas de jugador, objetos de entorno, la lógica del comportamiento de los enemigos y los elementos visuales del HUD.&#x20;

Cada evento e interacción que se genera son procesados por los scripts, los cuales al ejecutarse actualizan los sistemas en tiempo real implementando cambios ya sea en el gameplay o entorno. Por ejemplo, al recoger un objeto, automáticamente lo refleja en el inventario visualmente y ese mismo objeto desaparece del mundo porque ya fue recogido por el jugador.&#x20;

Teniendo en cuenta que el videojuego es Offline y no cuenta con servidores, la forma en que la comunicación entre el frontend (**Capa visual**) y el backend (**Capa lógica**) se pueden relacionar entre sí, es por medio de **Archivos locales** en formato **JSON**, siendo más fáciles de manejar en Unity. Archivos que pueden leerse directamente desde la Capa visual para el guardado y cargado de datos como las configuraciones, los progresos del juego y los inventarios con los objetos que el jugador tenga.&#x20;

Mediante la utilización de este Flujo de Datos Interno, se utilizaban API's para el guardado y cargado de datos locales del videojuego. Estado del jugador, objetos recogidos, vida restante, avance en la historia, etc. Sin embargo, por la complejidad de la implementación de un sistema de guardado y cargado de partidas funcional, se optó por no implementar dichas API's.&#x20;
