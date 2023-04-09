using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMotion : MonoBehaviour
{
     // Use this script on game objects where you want them to move up and down.
    public float speed = 1f;
    public float amplitude = 1f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float t = Time.time * speed;
        float yOffset = Mathf.Sin(t) * amplitude;
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }
}
