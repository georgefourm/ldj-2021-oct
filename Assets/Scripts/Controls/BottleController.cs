using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour
{
    Vector3 screenSpace;
    Vector3 offset;

    bool dragging = false;

    public float rotationSpeed = 20f;

    public float movementSpeed = 10f;

    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
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

    private void OnMouseDown()
    {
        dragging = true;

        screenSpace = Camera.main.WorldToScreenPoint(transform.position);

        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.velocity = Vector3.zero;

        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    }

    private void OnMouseDrag()
    {
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
        transform.position = new Vector3(curPosition.x,curPosition.y,transform.position.z);
    }

    private void OnMouseUp()
    {
        dragging = false;

        //rigidbody.AddForce(Vector3.down * 50f);
    }

    public bool IsPouring()
    {
        return Mathf.Abs(transform.rotation.eulerAngles.z) > 90;
    }
}
