using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float shiftSpeed = 15f;
    [SerializeField] float jumpForce = 7f;
    bool isGrounded = true;
    float currentSpeed;
    Rigidbody rb;
    Vector3 direction;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = movementSpeed;
    }
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = shiftSpeed;
        }
        if(!Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = movementSpeed;
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;       
    }
}
