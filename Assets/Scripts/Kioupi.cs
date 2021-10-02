using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kioupi : MonoBehaviour
{

    private MeshRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = transform.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.SetColor(Color.green);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            this.SetLevel(0.1f);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            this.SetLevel(0.9f);
        }
    }

    void SetLevel(float level)
    {
        float maxScale = 1.3f;
        level = Mathf.Clamp(level, 0, maxScale);
        transform.localScale += Vector3.up * (level - transform.localScale.y) ;
    }

    void SetColor(Color color)
    {
        renderer.material.color = color;
    }
}
