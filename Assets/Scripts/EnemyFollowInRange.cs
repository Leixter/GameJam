using UnityEngine;
using UnityEngine.AI; // Esto es obligatorio para usar NavMesh

public class EnemyFollowInRange : MonoBehaviour
{
    [Header("Configuración de Persecución")]
    [Tooltip("Arrastra aquí a tu Player desde la jerarquía")]
    public Transform player; 
    
    [Tooltip("A qué distancia el enemigo te empieza a ver")]
    public float detectionRadius = 10f;

    private NavMeshAgent agent;
    private AnimacionProcedural animacion; // Referencia a la animación que hicimos antes

    void Start()
    {
        // Agarramos el NavMeshAgent que está en este mismo objeto (Enemy)
        agent = GetComponent<NavMeshAgent>();
        
        // Buscamos el script de animación en el ModeloVisual (el hijo)
        animacion = GetComponentInChildren<AnimacionProcedural>();
    }

    void Update()
    {
        // Calculamos la distancia real entre el Enemigo y el Player
        float distanciaAlJugador = Vector3.Distance(transform.position, player.position);

        // Si el jugador entra en el círculo de visión...
        if (distanciaAlJugador <= detectionRadius)
        {
            // Le decimos al NavMeshAgent: "¡Persíguelo!"
            agent.SetDestination(player.position);

            // Activamos nuestra animación de rebote rápido
            if (animacion != null) 
            {
                animacion.estaMoviendose = true;
            }
        }
        else
        {
            // Si el jugador se aleja mucho, el enemigo se rinde y se frena
            agent.ResetPath();

            // Vuelve a la animación de respiración suave (Idle)
            if (animacion != null) 
            {
                animacion.estaMoviendose = false;
            }
        }
    }

    // Este método es un "truquito" visual para el Editor. 
    // Te va a dibujar una esfera roja alrededor del enemigo para que veas su rango de visión.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}