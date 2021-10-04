using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour
{
    public float rotationSpeed = 200f;

    public float movementSpeed = 10f;

    Vector3 screenSpace;

    bool dragging = false;

    private Rigidbody rb;

    private Vector3 prevMousePosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!dragging)
        {
            return;
        }
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(-Vector3.forward * movementSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        prevMousePosition = transform.position;

        if (Input.GetMouseButton(0) && dragging)
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnMouseDown()
    {
        dragging = true;

        screenSpace = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDrag()
    {
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace);
        transform.position = new Vector3(curPosition.x, curPosition.y, transform.position.z);
    }

    private void OnMouseUp()
    {
        dragging = false;

        var curScreenSpace = transform.position;
        rb.AddForce((curScreenSpace - prevMousePosition).normalized * 20);
    }
}
