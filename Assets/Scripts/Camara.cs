using UnityEngine;

public class Camara : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform objetivo;

    [Header("Posicion")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 3f, -4f);

    [Header("Rotacion con Mouse")]
    [SerializeField] private float sensibilidad = 3f;
    [SerializeField] private float limiteSuperior = 20f;
    [SerializeField] private float limiteInferior = 60f;

    private float rotacionX = 20f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (objetivo == null)
            objetivo = transform.parent;
    }

    private void LateUpdate()
    {
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad;
        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, limiteSuperior, limiteInferior);

        float mouseX = Input.GetAxis("Mouse X") * sensibilidad;
        objetivo.Rotate(Vector3.up * mouseX);

        Quaternion rotacion = objetivo.rotation * Quaternion.Euler(rotacionX, 0f, 0f);
        transform.position = objetivo.position + rotacion * offset;
        transform.LookAt(objetivo.position + Vector3.up * 1f);
    }
}