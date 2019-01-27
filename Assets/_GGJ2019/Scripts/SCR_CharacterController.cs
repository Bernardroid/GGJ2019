using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CharacterController : MonoBehaviour {

    public GameObject aim;
    public float rateOfFire;

    Rigidbody rb;
    Animator anim;

    public float characterSpeed;
    public GameObject spwnBullet;
    public GameObject bulletPre;
    WaitForSeconds myWait;
    Queue<GameObject> bulletPool;

    float x;
    float z;
    float aimX;
    float aimZ;
    bool isShooting = false;

    float radius = 3f;
    public float sensitivity;

    Vector3 aimVector;

    void Start ()
    {
        myWait = new WaitForSeconds(rateOfFire);
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        bulletPool = new Queue<GameObject>();
        for(int i = 0; i < 30; i++)
        {
            bulletPool.Enqueue(Instantiate(bulletPre, Vector3.one * -1000, Quaternion.identity));
        }
    }

    void Update ()
    {
        x = Input.GetAxisRaw("ChidoHorizontal") * characterSpeed;
        z = Input.GetAxisRaw("ChidoVertical") * characterSpeed;

        //Debug.Log("X: " + x);
        //Debug.Log("Z: " + z);


        aimX = Input.GetAxisRaw("Mouse X") * sensitivity;
        aimZ = Input.GetAxisRaw("Mouse Y") * sensitivity;

        aimVector = new Vector3(aimX, 0, aimZ);

        MoveCharacter(new Vector3(x, 0, z) * characterSpeed);
        transform.LookAt(aim.transform.position.x * Vector3.right + transform.position.y * Vector3.up + aim.transform.position.z * Vector3.forward);
        MoveAim(aimVector);

        if((Input.GetButtonDown("Shoot") || Input.GetButtonDown("Shoot2")) && !isShooting)
        {
            Debug.Log("SCHOOL SHOOTING");
            StartCoroutine(ShootBullet());
        }
        //if(Input.GetButtonUp("Shoot") || Input.GetButtonUp("Shoot"))
        //{
        //    anim.SetBool("Shooting", false);
        //    StopAllCoroutines();
        //}
    }

    IEnumerator ShootBullet()
    {
        isShooting = true;
        anim.SetBool("Shooting", true);
        bulletPool.Peek().transform.position = spwnBullet.transform.position;
        bulletPool.Peek().SetActive(true);
        bulletPool.Peek().transform.rotation = spwnBullet.transform.parent.transform.rotation;
        bulletPool.Enqueue(bulletPool.Dequeue());
        yield return myWait;
        anim.SetBool("Shooting", true);
        bulletPool.Peek().transform.position = spwnBullet.transform.position;
        bulletPool.Peek().SetActive(true);
        bulletPool.Peek().transform.rotation = spwnBullet.transform.parent.transform.rotation;
        bulletPool.Enqueue(bulletPool.Dequeue());
        yield return myWait;
        anim.SetBool("Shooting", true);
        bulletPool.Peek().transform.position = spwnBullet.transform.position;
        bulletPool.Peek().SetActive(true);
        bulletPool.Peek().transform.rotation = spwnBullet.transform.parent.transform.rotation;
        bulletPool.Enqueue(bulletPool.Dequeue());
        yield return myWait;
        anim.SetBool("Shooting", false);
        isShooting = false;
    }

    void MoveCharacter(Vector3 _moveVector)
    {
        rb.velocity = _moveVector;
        //if(_moveVector.normalized )
        anim.SetFloat("VelX", x/characterSpeed);
        anim.SetFloat("VelZ", z/characterSpeed);
    }

    void MoveAim(Vector3 _moveVector)
    {
        aim.transform.position += _moveVector;
        Vector3 centerPosition = new Vector3(transform.position.x, 0, transform.position.z);
        float distance = Vector3.Distance(aim.transform.position, centerPosition);

        if (distance > radius)
        {
            Vector3 fromOriginToObject = new Vector3(aim.transform.position.x, 0.1f, aim.transform.position.z) - centerPosition;
            fromOriginToObject *= radius / distance;
            aim.transform.position = centerPosition + fromOriginToObject;
        }
    }
}
