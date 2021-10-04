using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterController : MonoBehaviour
{
    public Transform whole;

    public GameObject shatteredPrefab;

    public float despawnAfter = 5.0f;

    public float shatterThreshold = 1f;

    private bool shattered = false;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (rb != null && rb.velocity.magnitude > shatterThreshold && !shattered)
        {
            Shatter();
            shattered = true;
        }
    }

    public void Shatter(bool delete = true)
    {
        whole.gameObject.SetActive(false);
        Instantiate(shatteredPrefab, whole.gameObject.transform.position, whole.gameObject.transform.rotation);
        if (delete) Destroy(transform.gameObject, despawnAfter);
    }

    public void Unshatter()
    {
        whole.gameObject.SetActive(true);
    }
}
