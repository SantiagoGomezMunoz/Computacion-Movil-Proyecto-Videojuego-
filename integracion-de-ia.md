---
description: >-
  La IA que se le asignó a los enemigos para la interacción el jugador y con el
  mismo entorno.
icon: brain
---

# Integración de IA

En el desarrollo del videojuego se ha buscado implementar un sistema básico pero funcional de Inteligencia Artificial para los enemigos que rondan por el juego. Aunque limitado, el comportamiento de los enemigos se basa en detectar y perseguir al jugador, además de patrullar el escenario del videojuego de forma aleatoria cuando el jugador no se encuentre en su rango de visión.

**Comportamiento de los enemigos:**

**Patrullaje Aleatorio:**

Mediante líneas de código alojado en los scripts `EnemigoSeguidor.cs` se encuentran los métodos los cuales dan las funcionalidades motrices de los enemigos. A continuación se va a mostrar un fragmento del código para visualizar mejor este comportamiento.

```csharp
    // Patrullaje
    public float tiempoEntrePatrullas = 2f; // Tiempo entre cambios de dirección
    public float duracionPatrulla = 3f; // Tiempo que el enemigo patrulla en una dirección
    private float tiempoEspera = 0f; // Tiempo restante de espera
    private float tiempoMovimiento = 0f; // Tiempo restante de movimiento
    private Vector3 direccionPatrulla = Vector3.zero; // Dirección actual de patrullaje
    
        void Start()
    {
        ElegirNuevaDireccionPatrulla(); // Inicializa el primer patrón de patrullaje
    }
    
        void FixedUpdate()
    {
        if (persiguiendo && objetivo != null)
        {
            Vector3 direccion = (objetivo.position - transform.position).normalized;
            Vector3 nuevaPos = transform.position + direccion * velocidad * Time.fixedDeltaTime;
            rb.MovePosition(nuevaPos);
        }
        else
        {
            Patrullar();
        }
    }
    
        void ElegirNuevaDireccionPatrulla()
    {
        // Elige una nueva dirección aleatoria para patrullar
        float angulo = Random.Range(0f, 360f);
        direccionPatrulla = new Vector3(Mathf.Cos(angulo), 0f, Mathf.Sin(angulo)).normalized;

        tiempoMovimiento = duracionPatrulla; // Establece el tiempo de movimiento
        tiempoEspera = tiempoEntrePatrullas; // Establece el tiempo de espera
    }

```

**Persecución al Jugador:**

El siguiente fragmento de código perteneciente al mismo script `EnemigorSeguidor.cs` hace que el enemigo pueda detectar la presencia del jugador si este entra en un rango cercano. Si el jugador se posiciona en este rango de visión, el enemigo comienza a perseguirlo, pero si el jugador se aleja lo suficiente para salir de ese rango de visión, el enemigo lo "Pierde de vista" y regresa a su patrullaje aleatorio.

```csharp
    public Transform objetivo;         // Jugador a seguir
    public float velocidad = 3.0f;     // Velocidad de movimiento
    public float rangoVision = 10f;    // Distancia para empezar a seguir
    public float rangoPerdida = 15f;   // Distancia para dejar de seguir

    private bool persiguiendo = false; // Estado del enemigo
    
        void Update()
    {
        if (objetivo != null)
        {
            float distancia = Vector3.Distance(transform.position, objetivo.position);

            if (!persiguiendo && distancia <= rangoVision)
            {
                persiguiendo = true;
            }
            else if (persiguiendo && distancia >= rangoPerdida)
            {
                persiguiendo = false;
                ElegirNuevaDireccionPatrulla(); // Reinicia patrullaj
            }
        }
    }
    
        void FixedUpdate()
    {
        if (persiguiendo && objetivo != null)
        {
            Vector3 direccion = (objetivo.position - transform.position).normalized;
            Vector3 nuevaPos = transform.position + direccion * velocidad * Time.fixedDeltaTime;
            rb.MovePosition(nuevaPos);
        }
        else
        {
            Patrullar();
        }
    }
```

**Reacción al jugador (Recibir el daño y retroceso):**

El siguiente fragmento de código se encarga de gestionar el cómo el enemigo reacciona a los ataques el jugador, haciendo posible que reciba daño, además de que cuando su vida llegue a 0 el enemigo muera.

```csharp
    public void RecibirDaño(int cantidad)
    {
        vida -= cantidad;

        //Retroceso
        Vector3 direccionEmpuje = (transform.position - objetivo.position).normalized;
        rb.AddForce(direccionEmpuje * 4f, ForceMode.Impulse); 
        // Cambio de color
        StartCoroutine(CambiarColorTemporal());

        if (vida <= 0)
        {
            Morir();
        }
    }
    
        void Morir()
    {
        if (sistemaXP != null)
        {
            sistemaXP.GanarExperiencia(35); // Gana 2 de experiencia al morir
        }
        Destroy(gameObject);
    }
```

4. **Representación visual del Daño:**

Cuando el enemigo reciba cualquier tipo de daño de parte del jugador, sea daño con arma cuerpo a cuerpo o con arma de fuego, cambiará de color a un rojo intenso por un tiempo reducido para indicarle al jugador que efectivamente el enemigo recibió daño. También retrocediendo ligeramente con cada golpe.

```csharp
    IEnumerator FeedbackDaño()
    {
        // Cambiar color a rojo
        foreach (Renderer rend in renderers)
        {
            rend.material.color = Color.red;
        }

        // Retroceso leve hacia atrás
        if (objetivo != null && rb != null)
        {
            Vector3 direccionRetroceso = (transform.position - objetivo.position).normalized;
            rb.AddForce(direccionRetroceso * 2f, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(0.1f);

        // Volver al color original
        foreach (Renderer rend in renderers)
        {
            rend.material.color = colorOriginal;
        }
    }
```
