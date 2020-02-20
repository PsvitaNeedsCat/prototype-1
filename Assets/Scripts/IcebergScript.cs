using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcebergScript : MonoBehaviour
{
    [SerializeField] private float amp;
    [SerializeField] private float freq;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private void Start()
    {
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * freq) * amp;

        transform.position = tempPos;
    }
}
