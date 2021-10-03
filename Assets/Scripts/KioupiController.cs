using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KioupiController : MonoBehaviour
{

    public Transform Fluid;

    public float Speed = 8;

    private MeshRenderer renderer;

    private float targetLevel = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = Fluid.GetComponent<MeshRenderer>();
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
            this.SetLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            this.SetLevel(.5f);
        }

        if (targetLevel!=GetCurrentScale())
        {
            int dir = targetLevel > GetCurrentScale() ? 1 : -1;
            float currentScale = GetCurrentScale() + dir * Speed * Time.deltaTime;
            SetScale(Mathf.Clamp(currentScale, 0, dir == 1 ? targetLevel: currentScale));
        }
    }

    void SetLevel(float level)
    {
        float maxScale = 19.0f;
        level = Mathf.Clamp(level, 0, 1);
        targetLevel = level * maxScale;
        //Fluid.localScale += Vector3.forward * (level - Fluid.localScale.z);
    }

    void SetColor(Color color)
    {
        renderer.material.color = color;
    }

    private void SetScale(float scale)
    {
        Fluid.localScale += Vector3.forward * (scale - GetCurrentScale());
    }

    private float GetCurrentScale()
    {
        return Fluid.localScale.z;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chemical")
        {
            var controller = other.GetComponent<BottleController>();
            if (controller.IsPouring()) Debug.Log("Yeaah");
        }
    }
}
