using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RandomLight : MonoBehaviour
{
    [SerializeField] Light2D[] pointLights;
    [SerializeField] float speed;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {           
        foreach (Light2D light in pointLights)
        {
            float randInAngle = Random.Range(0.5f, 24f);
            float randOutAngle = Random.Range(25f, 50f);
            float randInRadius = Random.Range(4f, 8f);
            float randIntensity = Random.Range(0.2f, 0.9f);
            float innerAngle = light.pointLightInnerAngle;
            float outerAngle = light.pointLightOuterAngle;
            float innerRadius = light.pointLightInnerRadius;
            float intensity = light.intensity;
            light.pointLightInnerAngle = Mathf.Lerp(innerAngle, randInAngle, Time.deltaTime * speed);
            light.pointLightOuterAngle = Mathf.Lerp(outerAngle, randOutAngle, Time.deltaTime * speed);
            light.pointLightInnerRadius = Mathf.Lerp(innerRadius, randInRadius, Time.deltaTime * speed);
            light.intensity = Mathf.Lerp(intensity, randIntensity, Time.deltaTime * speed);
        }
    }
}
