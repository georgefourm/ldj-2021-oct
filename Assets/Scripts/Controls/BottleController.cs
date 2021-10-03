using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour
{
    public float rotationSpeed = 200f;

    public float movementSpeed = 10f;

    public float pourDelay = 0.5f;

    public ChemicalComponent chemical;

    public Transform spout;

    Vector3 screenSpace;

    bool dragging = false;

    private float pourTimer = 0f;

    private Rigidbody rb;

    private Vector3 prevMousePosition;

    public MeshRenderer FluidMesh;

    private ParticleSystem ps;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ps = GetComponentInChildren<ParticleSystem>();
        var main = ps.main;
        main.startColor = FluidMesh.material.color;
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
        
        if (isTilted())
        {
            if (!ps.gameObject.activeSelf)
            {
                ps.gameObject.SetActive(true);
                ps.Play();
            }
        } else
        {
            ps.gameObject.SetActive(false);
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

    public bool IsPouring()
    {
        pourTimer += Time.deltaTime;
        if (pourTimer >= pourDelay && isTilted())
        {
            pourTimer = 0;
            return true;
        }
        return false;
    }

    private bool isTilted()
    {
        return transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270;
    }
}
