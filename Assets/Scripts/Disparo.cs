using UnityEngine;

public class Disparo : MonoBehaviour
{
    [Header("Disparo")]
    [SerializeField] private float alcance = 50f;
    [SerializeField] private float cadencia = 0.2f;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private ParticleSystem efectoDisparo;

    // --- NUEVO: ELEGIMOS QUÉ PODEMOS GOLPEAR DESDE EL INSPECTOR ---
    [SerializeField] private LayerMask capasImpactables;

    private float tiempoUltimoDisparo;
    private Camera camaraPrincipal;

    private void Awake()
    {
        camaraPrincipal = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && PuedoDisparar())
        {
            Disparar();
            tiempoUltimoDisparo = Time.time;
        }
    }

    private bool PuedoDisparar()
    {
        return Time.time >= tiempoUltimoDisparo + cadencia;
    }

    private void Disparar()
    {
        if (efectoDisparo != null)
            efectoDisparo.Play();

        // --- LA MAGIA ESTÁ AQUÍ ---
        // Ya no disparamos desde el centro de la pantalla.
        // Disparamos desde el "Punto Disparo" de tu pistola, hacia el frente de tu jugador.
        Ray rayo = new Ray(puntoDisparo.position, puntoDisparo.forward);

        if (Physics.Raycast(rayo, out RaycastHit impacto, alcance, capasImpactables))
        {
            // Le subí el tiempo a la línea roja a 2 segundos para que veas clarito el láser en la vista 'Scene'
            Debug.DrawLine(rayo.origin, impacto.point, Color.red, 2f);
            
            if (impacto.collider.CompareTag("Enemy"))
            {
                Debug.Log("¡Enemigo abatido!");
                Destroy(impacto.collider.gameObject);
            }
            else
            {
                Debug.Log($"Impacto en: {impacto.collider.gameObject.name}");
            }
        }
        else
        {
            Debug.DrawRay(rayo.origin, rayo.direction * alcance, Color.yellow, 2f);
        }
    }
}