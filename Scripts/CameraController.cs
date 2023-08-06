using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 initialPosition;
    private Transform eye;
    public  Transform target;
    public float velocity;

    void Start()
    {
        eye = GetComponent<Transform>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("d")) HorizontalMove(1);
        if(Input.GetKey("a")) HorizontalMove(-1);
        if(Input.GetKey("w")) VerticalMove(1);
        if(Input.GetKey("s")) VerticalMove(-1);

        FrontMove(Input.mouseScrollDelta);

        if(Input.GetKeyDown("r")) ResetPosition();


        LookAt();
    }

    private void ResetPosition()
    {
        transform.position = initialPosition;
    }

    private void LookAt()
    {
        Vector3 direction = (target.position - eye.position);
        eye.forward = direction;
    }

    private void HorizontalMove(int axe = 1)
    {
        eye.Translate(axe * velocity * Time.deltaTime, 0, 0);
    }

    private void VerticalMove(int axe = 1)
    {
        eye.Translate(0, axe * velocity * Time.deltaTime, 0);
    }

    private void FrontMove(Vector2 v)
    {
        eye.Translate(0, 0, velocity * Time.deltaTime * v.y);
    }
}
