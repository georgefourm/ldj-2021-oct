using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleController : MonoBehaviour
{

    float timeCounter;


    float initXRotation = -90;

    Vector3 initPos;

    public float smoothness = 10;

    public float translocationX = 0.05f;

    public float translocationY = 0.02f;

    private Dictionary<WobbleLevel, WobbleConfig> configs = new Dictionary<WobbleLevel, WobbleConfig>();

    public WobbleLevel wobbleLevel;

    private void Awake()
    {
        initPos = transform.position;

        configs.Add(WobbleLevel.NO, new WobbleConfig(0,0));
        configs.Add(WobbleLevel.LOW, new WobbleConfig(8, 0.02f, 0.005f));
        configs.Add(WobbleLevel.MED, new WobbleConfig(9, 0.05f, 0.005f));
        configs.Add(WobbleLevel.HIGH, new WobbleConfig(10, 0.06f, 0.02f));
        configs.Add(WobbleLevel.EXTREME, new WobbleConfig(11, 0.08f, 0.05f));

        SetWobbleLevel(WobbleLevel.NO);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        timeCounter += Time.deltaTime * smoothness;
        
        var t = new Vector3(
            (Mathf.PerlinNoise(0, timeCounter) - 0.5f) * translocationX,
            0, //(Mathf.PerlinNoise(1, timeCounter) - 0.5f) * 2,
                (Mathf.PerlinNoise(6, timeCounter) - 0.5f) * translocationY
        );

        transform.position = initPos + t;
        
        /*
        transform.Rotate(t * 10);
        Debug.Log(t * 10);*/
       // var wtv = (timeCounter % 2) / 2;
        //var rot = (coorba.Evaluate(wtv) - 0.5f) * woobleStrength;

        //transform.position = initPos + rot * Vector3.right;
        
//        transform.rotation = Quaternion.Euler(initXRotation + rot, transform.rotation.y, transform.rotation.z);
        //transform.Rotate(Vector3.right * (coorba.Evaluate(wtv) -0.5f) * 10);
    }

    public void SetWobbleLevel(WobbleLevel level)
    {
        wobbleLevel = level;
        WobbleConfig config = configs[level];
        smoothness = config.smoothness;
        translocationX = config.translocationX;
        translocationY = config.translocationY;
    }
}
