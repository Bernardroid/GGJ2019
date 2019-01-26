using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CharacterController : MonoBehaviour {

    public GameObject aim;

    Rigidbody rb;
    public float characterSpeed;

    float x;
    float z;
    float aimX;
    float aimZ;

    Vector3 aimTarget;
    RaycastHit hitInfo;

    float rotateSpeed = 5f;
    float radius = 3f;
    public float sensitivity;

    float aimOffset;


    Vector3 aimCenter;
    Vector3 aimVector;
    float aimAngle;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        hitInfo = new RaycastHit();
        aimCenter = transform.position;
        aimOffset = radius;

    }

    void Update ()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        

        aimX = Input.GetAxisRaw("Mouse X") * sensitivity;
        aimZ = Input.GetAxisRaw("Mouse Y") * sensitivity;

        aimVector = new Vector3(aimX, 0, aimZ);

        MoveCharacter(new Vector3(x, 0, z) * characterSpeed);
        transform.LookAt(aim.transform.position.x * Vector3.right + transform.position.y * Vector3.up + aim.transform.position.z * Vector3.forward);
        MoveAim(aimVector);
    }

       
    void MoveCharacter(Vector3 _moveVector)
    {
        rb.velocity = _moveVector;
    }

    void MoveAim(Vector3 _moveVector)
    {
        aim.transform.position = new Vector3(aim.transform.position.x + _moveVector.x, aim.transform.position.y, aim.transform.position.z + _moveVector.z);
        Vector3 centerPosition = transform.position;
        float distance = Vector3.Distance(aim.transform.position, centerPosition);

        if (_moveVector.normalized == -transform.forward)
        {
            Debug.Log("HAPPENIN");
            aimOffset -= aimVector.magnitude;
        }

        if (_moveVector.normalized == transform.forward && distance < radius)
        {
            aimOffset += aimVector.magnitude;
        }

        if (distance > radius || distance > aimOffset)
        {
            //Debug.Log("Exceeding");
            Vector3 fromOriginToObject = aim.transform.position - centerPosition;
            fromOriginToObject *= radius / distance;
            aim.transform.position = Vector3.right * (centerPosition.x + fromOriginToObject.x) + aim.transform.position.y * Vector3.up + Vector3.forward * (centerPosition.z + fromOriginToObject.z);
        }
    }
}
