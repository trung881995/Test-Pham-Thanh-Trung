using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTower : MonoBehaviour
{
    public float mouseRotationSpeed = 200f; // Tốc độ xoay về phía chuột
    private Quaternion targetRotation; // Góc quay mục tiêu

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateToMouse();
    }
    void RotateToMouse()
    {
        if (Input.GetMouseButton(0)) // Kiểm tra nếu đang giữ chuột trái
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                targetRotation = Quaternion.LookRotation(targetPosition - transform.position); // Xác định góc quay mục tiêu
            }

            // Xoay từ từ về phía mục tiêu
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, mouseRotationSpeed * Time.deltaTime);
        }
    }

}
