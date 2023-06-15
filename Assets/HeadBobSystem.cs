using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobSystem : MonoBehaviour
{
    [Range(0.001f, 0.05f)]
    public float amount = 0.002f;
    [Range(1f, 30f)]

    public float frequency = 10.0f;

    [Range(10f, 100f)]
    public float smooth = 10.0f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        CheckForHeadBobTrigger();
        StopHeadBob();
    }

    private void CheckForHeadBobTrigger()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        if(inputMagnitude > 0)
        {
            StarHeadBob();
        }
    }

    private Vector3 StarHeadBob()
    {
        Vector3 pos = Vector3.zero;

        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);

        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency / 2f) * amount * 1.6f, smooth * Time.deltaTime);

        transform.localPosition += pos;

        return pos;
    }

    private void StopHeadBob()
    {
        if (transform.localPosition == startPos) return;
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, 1 * Time.deltaTime);

    }
}
