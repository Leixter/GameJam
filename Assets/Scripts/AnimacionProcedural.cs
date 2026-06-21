using UnityEngine;

public class AnimacionProcedural : MonoBehaviour
{
    public bool estaMoviendose = false;
    public float velocidadResp = 3f;
    public float amplitudResp = 0.05f;
    public float velocidadCaminar = 12f; 
    public float amplitudCaminar = 0.15f; 

    private Vector3 escalaInicial;

    void Start()
    {
        // Esto nos dirá si Unity realmente está leyendo el archivo
        escalaInicial = transform.localScale;
    }

    void Update()
    {
        float velocidadActual = estaMoviendose ? velocidadCaminar : velocidadResp;
        float amplitudActual = estaMoviendose ? amplitudCaminar : amplitudResp;

        float ciclo = Mathf.Sin(Time.time * velocidadActual);

        float nuevaX = escalaInicial.x + (ciclo * amplitudActual * 0.5f);
        float nuevaY = escalaInicial.y + (-ciclo * amplitudActual); 
        float nuevaZ = escalaInicial.z + (ciclo * amplitudActual * 0.5f);

        transform.localScale = new Vector3(nuevaX, nuevaY, nuevaZ);
    }
}