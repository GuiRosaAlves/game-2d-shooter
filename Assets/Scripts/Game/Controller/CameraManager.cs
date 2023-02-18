using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{   
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _camOffset = new Vector3(0, 0, -10);
    [Range(1f, 10f)] [SerializeField] private float _smoothTimeX = 1f, _smoothTimeY = 1f, _smoothTimeZ = 1f;
    private Vector3 _velocity;
    [SerializeField] private Camera _camera;
    public Transform Target => _target;
    public Vector3 CamOffset => _camOffset;
    public Camera Camera => _camera;
    public float SmoothTimeX => _smoothTimeX;
    public float SmoothTimeY => _smoothTimeY;
    public float SmoothTimeZ => _smoothTimeZ;

    void Awake()
    {
        if (!_camera)
        {
            Debug.Log("No camera reference set in inspector");
        }
    }
    void FixedUpdate()
    {
        if (_target == null)
        {
            Debug.Log("There is no target to follow!");
        }
        else
        {
            var cameraTransform = _camera.transform;
            float posX = Mathf.SmoothDamp(cameraTransform.position.x, _target.position.x + _camOffset.x, ref _velocity.x, 1 / _smoothTimeX);
            float posY = Mathf.SmoothDamp(cameraTransform.position.y, _target.position.y + _camOffset.y, ref _velocity.y, 1 / _smoothTimeY);
            float posZ = Mathf.SmoothDamp(cameraTransform.position.z, _target.position.z + _camOffset.z, ref _velocity.z, 1 / _smoothTimeZ);

            cameraTransform.position = (Vector3.right * posX) + (Vector3.up * posY) + (Vector3.forward * posZ);
        }
    }
}