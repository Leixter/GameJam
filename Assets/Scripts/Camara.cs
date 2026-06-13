using UnityEngine;

public class Camara : MonoBehaviour
{
    [SerializeField] public float Sensibilidad = 100;
    public Transform Player;

    public float RotacionHorizontal = 0;
    public float RotacionVertical = 0;
    
    void Start()
    {
        //Bloque el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        //Oculta el cursor 
        Cursor.visible = false;
    }

    void Update()
    {
        //Se toman los valores del mouse 
        float ValorX = Input.GetAxis("Mouse X") * Sensibilidad * Time.deltaTime;
        float ValorY = Input.GetAxis("Mouse Y") * Sensibilidad * Time.deltaTime;

        //Guarda los valores y queda en el valor para seguir
        RotacionHorizontal -= ValorX;
        RotacionVertical -= ValorY;
        // Limita 80 grados
        RotacionVertical = Mathf.Clamp(RotacionVertical, -80f, 80f);
        //Hace la rotacion vertical fluida
        transform.localRotation = Quaternion.Euler(RotacionVertical, 0, 0);

        // Hace la rotacion horizontal
        if (Player != null)
        {
            Player.Rotate(Vector3.up * ValorX);
        }
        else
        {
            Debug.LogWarning("Warning: asigna el Player en el inspector.");
        }
    }
}