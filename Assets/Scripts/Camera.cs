
using UnityEngine;

public class Camera : MonoBehaviour
{
    [Header("References")]
    public Transform target;
    
    [Header("Attributes")]
    public Vector3 offset;
    public float smoothSpeed=0.125f;
    public Vector3 camerarot;

    void Start()
    {
        transform.localEulerAngles = camerarot;
    }
    void LateUpdate()
    {
        
        Vector3 desiredposition = target.position + offset;
        Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredposition, smoothSpeed);
        transform.position = smoothedposition;
        //transform.LookAt(target);
    }
}
