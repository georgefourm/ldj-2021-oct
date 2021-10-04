using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour
{
    public float rotationSpeed = 200f;

    public float movementSpeed = 10f;

    public float resetTimer = 2.0f;

    Vector3 screenSpace;

    private bool dragging = false;

    private bool reset = false;

    private float resetTimeRemaining;

    private Rigidbody rb;

    private float originalPositionX;

    private float originalPositionZ;

    private Vector3 prevMousePosition;

    private void Start()
    {
        resetTimeRemaining = resetTimer;
        originalPositionX = transform.position.x;
        originalPositionZ = transform.position.z;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (dragging)
        {
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.D))
                transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.W))
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.S))
                transform.Translate(-Vector3.forward * movementSpeed * Time.deltaTime);
        } else
        {
            if (!reset && (transform.position.x != originalPositionX || transform.position.z != originalPositionZ)) reset = true;
            if (reset) resetTimeRemaining -= Time.deltaTime;
            if (resetTimeRemaining < 0) Destroy(gameObject);
        }
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
        CancelReset();
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
        reset = true;

        var curScreenSpace = transform.position;
        rb.AddForce((curScreenSpace - prevMousePosition).normalized * 20);
    }

    private void CancelReset()
    {
        resetTimeRemaining = resetTimer;
        reset = false;
    }
}
