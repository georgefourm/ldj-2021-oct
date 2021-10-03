using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterController : MonoBehaviour
{
    public Transform whole, shattered;

    private bool shouldShatter = false;

    private float shatteredAt = 0;

    public float despawnAfter = 8;

    public float shatterThreshold = 1f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shatteredAt == 0) return;
        if (Time.time - shatteredAt >= despawnAfter)
            Destroy(transform.gameObject);
    }

    private void OnMouseUp()
    {
        shouldShatter = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!shouldShatter || rb.velocity.magnitude < shatterThreshold) return;
        Shatter();
    }

    public void Shatter(bool delete = true)
    {
        whole.gameObject.SetActive(false);
        shattered.gameObject.SetActive(true);
        if (delete) shatteredAt = Time.time;
    }

    public void Unshatter()
    {
        whole.gameObject.SetActive(true);
        shattered.gameObject.SetActive(false);
    }
}
