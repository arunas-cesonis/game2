
using UnityEditor;
using UnityEngine;
public class Player : MonoBehaviour
{
    private float playerSpeed = 10.0f;
    public enum ControlType { Tank, Directional, Mouse };

    public ControlType controlType;

    public GameObject weapon;
    public GameObject pointer;
    private Rigidbody body;
    private Vector3 moveInput = Vector3.zero;
    private Quaternion rotateInput = Quaternion.identity;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        weapon.SetActive(false);
    }

    private void SetRotateInputFrom(Vector3 target)
    {
        Vector3 current = gameObject.transform.forward;
        rotateInput.SetFromToRotation(current, target);
    }

    void Update()
    {
        switch (controlType)
        {
            case ControlType.Tank:
                {
                    rotateInput = Quaternion.Euler(0, Input.GetAxis("Horizontal") * 10.0f, 0);
                    moveInput = transform.forward * Input.GetAxis("Vertical");
                }
                break;
            case ControlType.Directional:
                {
                    Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    moveInput = move;

                    if (move != Vector3.zero)
                    {
                        SetRotateInputFrom(move);
                        gameObject.transform.forward = move;    
                    }
                    else
                    {
                        rotateInput = Quaternion.identity;
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
                        SetRotateInputFrom(target);
                    }
                    else
                    {
                        rotateInput = Quaternion.identity;
                    }
                    Vector3 v = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
                    Vector3 h = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up);
                    Vector3 move = v * Input.GetAxis("Vertical") + h * Input.GetAxis("Horizontal");
                    moveInput = move;
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

    void FixedUpdate()
    {
        body.MovePosition(body.position + moveInput * playerSpeed * Time.fixedDeltaTime);
        Quaternion rotateStep = Quaternion.Lerp(Quaternion.identity, rotateInput, Time.fixedDeltaTime * playerSpeed);
        body.MoveRotation(body.rotation * rotateStep);
    }

}
