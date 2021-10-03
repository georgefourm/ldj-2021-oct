using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour
{
    public float rotationSpeed = 20f;

    public float movementSpeed = 10f;

    public float pourDelay = 0.5f;

    public ChemicalComponent chemical;

    public Transform spout;

    Vector3 screenSpace,offset;

    bool dragging = false;

    private float pourTimer = 0f;

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
    }

    private void OnMouseDown()
    {
        dragging = true;

        screenSpace = Camera.main.WorldToScreenPoint(transform.position);

        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;

        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    }

    private void OnMouseDrag()
    {
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
        transform.position = new Vector3(curPosition.x, curPosition.y, transform.position.z);
    }

    private void OnMouseUp()
    {
        dragging = false;

        var curScreenSpace = transform.position;
        rb.AddForce((curScreenSpace - prevMousePosition).normalized * 20);
    }

    public bool IsPouring()
    {
        pourTimer += Time.deltaTime;
        var isTilted = transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270;
        if (pourTimer >= pourDelay && isTilted)
        {
            pourTimer = 0;
            return true;
        }
        return false;
    }
}
