using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightValueEntity : MonoBehaviour
{
    Light light;

    [Header("light Information")]
    public float light_minValue = 4f;
    public float light_maxValue = 20f;
    public float duration = 1f;
    private float m_lightValue = 4f;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(Flickering());
    }
    IEnumerator Flickering()
    {
        while(true)
        {
            // 최소 ~ 최대
            for(float t = light_minValue; t<= light_maxValue; t += Time.deltaTime / duration)
            {
                light.intensity = t;
                yield return null;
            }

            for (float t = light_maxValue; t >= light_minValue; t -= Time.deltaTime / duration)
            {
                light.intensity = t;
                yield return null;
            }
        }
    }
}
