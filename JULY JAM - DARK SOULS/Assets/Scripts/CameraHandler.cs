using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;
    private Transform myTransform;
    private Vector3 cameraTransformPos;
    private LayerMask ignoreLayers;

    private Vector3 cameraFollowVelocity = Vector3.zero;

    public static CameraHandler singleton;

    public float lookSpeed = 0.1f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.03f;

    private float targetPos;
    private float defaultPos;
    private float lookAngle;
    private float pivotAngle;

    public float minimumPivot = -35;
    public float maximumPivot = 35;

    public float cameraSphereRadius = 0.2f;
    public float cameraCollisionOffest = 0.2f;
    public float minimumCollisionOffest = 0.2f;

    private void Awake(){
        singleton = this;
        myTransform = transform;
        defaultPos = cameraTransform.localPosition.z;
        ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
    }

    public void FollowTarget(float delta){
        Vector3 targetPos = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);
        myTransform.position = targetPos;

        HandleCameraCollisions(delta);
    }

    public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput){
        lookAngle += (mouseXInput * lookSpeed) / delta;
        pivotAngle -= (mouseYInput * pivotSpeed) / delta;
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRot = Quaternion.Euler(rotation);
        myTransform.rotation = targetRot;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;

        targetRot = Quaternion.Euler(rotation);
        cameraPivotTransform.localRotation = targetRot;
    }

    private void HandleCameraCollisions(float delta){
        targetPos = defaultPos;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();
 
        if(Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPos))){
            float dist = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetPos = -(dist - cameraCollisionOffest);
        }

        if(Mathf.Abs(targetPos) < minimumCollisionOffest){
            targetPos = -minimumCollisionOffest;
        }

        cameraTransformPos.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPos, delta / 0.2f);
        cameraTransform.localPosition = cameraTransformPos;
    }

}
