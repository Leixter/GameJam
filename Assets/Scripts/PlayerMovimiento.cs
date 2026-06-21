using UnityEngine;

public class PlayerMovimiento : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform camara;
    private CharacterController controlador;

    [Header("Movimiento")]
    [SerializeField] private bool UsaGetAxisRaw;
    [SerializeField] private float velocidadMovimiento = 5f;

    [Header("Salto y Gravedad")]
    [SerializeField] private float fuerzaSalto = 5f;
    [SerializeField] private float gravedad = -20f;
    private Vector3 velocidadVertical;

    private void Awake()
    {
        controlador = GetComponent<CharacterController>();
        if (camara == null && Camera.main != null)
            camara = Camera.main.transform;
    }

    void Update()
    {
        MoverJugadorEnPlano();
        AplicarGravedad();
        Saltar();
    }

    private void MoverJugadorEnPlano()
    {
        float ValorHorizontal = UsaGetAxisRaw ? Input.GetAxisRaw("Horizontal") : Input.GetAxis("Horizontal");
        float ValorVertical = UsaGetAxisRaw ? Input.GetAxisRaw("Vertical") : Input.GetAxis("Vertical");

        Vector3 adelanteCamara = camara.forward;
        Vector3 derechaCamara = camara.right;

        adelanteCamara.y = 0f;
        derechaCamara.y = 0f;

        adelanteCamara.Normalize();
        derechaCamara.Normalize();

        Vector3 direccionPlano = (derechaCamara * ValorHorizontal + adelanteCamara * ValorVertical);

        if (direccionPlano.sqrMagnitude > 0.0001f)
            direccionPlano.Normalize();

        controlador.Move(direccionPlano * velocidadMovimiento * Time.deltaTime);
    }

    private void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && controlador.isGrounded)
        {
            velocidadVertical.y = fuerzaSalto;
        }
    }

    private void AplicarGravedad()
    {
        if (controlador.isGrounded && velocidadVertical.y < 0)
        {
            velocidadVertical.y = -2f;
        }

        velocidadVertical.y += gravedad * Time.deltaTime;
        controlador.Move(velocidadVertical * Time.deltaTime);
    }
}