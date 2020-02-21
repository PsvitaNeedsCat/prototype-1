using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingScript : MonoBehaviour
{
    [SerializeField] private float amp;
    [SerializeField] private float freq;

    [SerializeField] private bool xScale;
    [SerializeField] private bool yScale;
    [SerializeField] private bool zScale;

    Vector3 scaleOffset = new Vector3();
    Vector3 tempScale = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        scaleOffset = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        tempScale = scaleOffset;

        if (xScale)
        {
            tempScale.x += Mathf.Sin(Time.fixedTime * Mathf.PI * freq) * amp;
        }

        if (yScale)
        {
            tempScale.y += Mathf.Sin(Time.fixedTime * Mathf.PI * freq) * amp;
        }

        if (zScale)
        {
            tempScale.z += Mathf.Sin(Time.fixedTime * Mathf.PI * freq) * amp;
        }

        transform.localScale = tempScale;
    }
}
