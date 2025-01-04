using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [Range(0,1)]public float smooth;
    public Vector3 offset;
    public Vector3 velocity = Vector3.zero;

    [Header("Axis Limitation")]
    public Vector2 xLitmit;
    public Vector2 yLitmit;

    internal void MoveToNewRoom(Transform parent)
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xLitmit.x, xLitmit.y), Mathf.Clamp(targetPosition.y, yLitmit.x, yLitmit.y),-10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smooth);
    }
}
