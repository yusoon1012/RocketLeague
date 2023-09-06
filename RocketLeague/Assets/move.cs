using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 30f;
    public float rotationSpeed = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float rotationInput = Input.GetAxis("Horizontal");

        Vector3 moveDirection =  Vector3.left* moveInput * moveSpeed;
        rb.velocity = moveDirection;

        float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotationAmount);
    }
}
