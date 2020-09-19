
using UnityEngine;
public class Player : MonoBehaviour
{
    private CharacterController controller;
    private float directionalPlayerSpeed = 4.0f;
    private float mousePlayerSpeed = 10.0f;
    public enum ControlType { Tank, Directional, Mouse };

    public ControlType controlType;

    public GameObject weapon;
    public GameObject pointer;
    


    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        weapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (controlType)
        {
            case ControlType.Tank:
                {
                    transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * 70, 0);
                    controller.Move(transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * 5);
                }
                break;
            case ControlType.Directional:
                {
                    Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    controller.Move(move * Time.deltaTime * directionalPlayerSpeed);

                    if (move != Vector3.zero)
                    {
                        gameObject.transform.forward = move;    
                    }
                }
                break;
            case ControlType.Mouse:
                {
                    Plane plane = new Plane(Vector3.up, 0);
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    float distance;
                    if (plane.Raycast(ray, out distance))
                    {
                        Vector3 pointer = ray.GetPoint(distance);
                        Vector3 target = pointer - gameObject.transform.position;
                        gameObject.transform.forward = target;

                    }
                    Vector3 v = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
                    Vector3 h = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up);
                    Vector3 move = v * Input.GetAxis("Vertical") + h * Input.GetAxis("Horizontal");
                    controller.Move(move * mousePlayerSpeed * Time.deltaTime);

                }
                break;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            weapon.SetActive(true);
        }
        else
        {
            weapon.SetActive(false);
        }

    }

}
