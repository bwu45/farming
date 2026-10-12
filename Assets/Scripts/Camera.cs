using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform target;
    Vector3 camOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camOffset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        transform.position = target.position + camOffset;
    }
}
