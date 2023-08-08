using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 5f;  // Velocidade de rotação da câmera
    public Transform target;         // Ponto ao redor do qual a câmera irá se mover
    public float distance = 10f;     // Distância da câmera ao ponto

    public float currentLatitude = 0f;
    public float currentLongitude = 0f;

    private void Start()
    {
        Vector3 position = Quaternion.Euler(currentLatitude, currentLongitude, 0) * new Vector3(0, 0, -distance);
        transform.position = target.position + position;
        transform.LookAt(target.position);
    }

    private void Update()
    {
        if(Input.mouseScrollDelta.y != 0)
            distance -= Input.mouseScrollDelta.y;

        if (Input.GetMouseButton(1))
        {
            currentLatitude -= Input.GetAxis("Mouse Y") * rotationSpeed;
            currentLongitude += Input.GetAxis("Mouse X") * rotationSpeed;

            // Limitar a latitude entre -90 e 90 graus para evitar inversões
            currentLatitude = Mathf.Clamp(currentLatitude, -90f, 90f);

        }
        Vector3 position = Quaternion.Euler(currentLatitude, currentLongitude, 0) * new Vector3(0, 0, -distance);
        transform.position = target.position + position;
        transform.LookAt(target.position);
    }
}
