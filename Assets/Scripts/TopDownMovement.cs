using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Tốc độ di chuyển
    public float sprintSpeed = 8f; // Tốc độ chạy nước rút
    public float rotationSpeed = 10f; // Tốc độ xoay nhân vật
    public float jumpForce = 5f; // Lực nhảy

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;

    public float mouseRotationSpeed = 200f; // Tốc độ xoay về phía chuột
    private Quaternion targetRotation; // Góc quay mục tiêu

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Rotate();
        Jump();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal"); // Nhận input A/D hoặc phím mũi tên trái/phải
        float moveZ = Input.GetAxis("Vertical"); // Nhận input W/S hoặc phím mũi tên lên/xuống

        // Kiểm tra có nhấn Shift để chạy nhanh không
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        // Tạo hướng di chuyển
        moveDirection = new Vector3(moveX, 0, moveZ).normalized * speed;

        // Di chuyển nhân vật bằng Rigidbody
        rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);
    }

    void Rotate()
    {
        if (moveDirection != Vector3.zero)
        {
            // Xoay nhân vật theo hướng di chuyển
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Đánh dấu là đang nhảy
        }
    }

    



    // Kiểm tra nhân vật có đang trên mặt đất không
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
