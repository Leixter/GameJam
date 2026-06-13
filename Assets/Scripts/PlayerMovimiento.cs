using UnityEngine;

public class PlayerMovimiento : MonoBehaviour
{
    [Header("Referencias")]
     [SerializeField] private Transform camara;
     private CharacterController controlador;
    

    [Header("Movimiento")]
    [SerializeField] private bool UsaGetAxisRaw;
   
    [SerializeField] private float velocidadMovimiento=5f;
    
    [Header("Gravedad")]
    [SerializeField] private float Gravedad = -9f ;
    private Vector3 velocidadVertical;
    void Start()
    {
        
    }

    private void Awake()
    {
        controlador=GetComponent<CharacterController>();
        if(camara == null && Camera.main !=null)
            camara= Camera.main.transform;
    }

    void Update()
    {
        MoverJugadorEnPlano();
        AplicarGravedad();
    }

    private void MoverJugadorEnPlano()
    {
        //Se captura las teclas de movimiento
        float ValorHorizontal = UsaGetAxisRaw ? Input.GetAxisRaw("Horizontal"): Input.GetAxis("Horizontal");
        float ValorVertical = UsaGetAxisRaw ? Input.GetAxisRaw("Vertical"): Input.GetAxis("Vertical");
        //Se calcula hacia donde mira la camra solo en en el eje
        Vector3 adelanteCamara= camara.forward;
        Vector3 derechaCamara = camara.right;
        //Se elimina eje y para no molestar
        adelanteCamara.y=0f;
        derechaCamara.y=0f;
        //Se normaliza para no tener valores diferentes
        adelanteCamara.Normalize();
        derechaCamara.Normalize();
        //Combina para tener flechas diagonales
        Vector3 direccionplano = (derechaCamara * ValorHorizontal + adelanteCamara * ValorVertical);
        //Hace que cuando hayan 2 direciones en diagonal camine vaya mas rapido
        if(direccionplano.sqrMagnitude > 0.0001f)
            direccionplano.Normalize();
        //Le da velocidad 
        Vector3 desplazamientoXZ = direccionplano*(velocidadMovimiento * Time.deltaTime);

        controlador.Move (desplazamientoXZ);

        //Debug.log($"ValorHorizontal: {ValorHorizontal:F1} | ValorVertical:{ValorVertical:F1}");
    }
    private void AplicarGravedad()
    {
        velocidadVertical.y+= Gravedad * Time.deltaTime;
        controlador.Move(velocidadVertical * Time.deltaTime);

        if(controlador.isGrounded && velocidadVertical.y < 0) 
        {
            velocidadVertical.y = -2f;
        }
    }
}
