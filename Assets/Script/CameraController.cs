using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smooth = 0.125f;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = transform.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(target.position, desiredPosition, smooth);
        transform.position = smoothPosition;
    }
}
