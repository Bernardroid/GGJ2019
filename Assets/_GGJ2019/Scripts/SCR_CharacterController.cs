using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CharacterController : MonoBehaviour {

    public GameObject aim;
    public float rateOfFire;
    public float shootingDelay;
    Rigidbody rb;
    Animator anim;

    public float characterSpeed;
    public GameObject spwnBullet;
    public GameObject bulletPre;
    public int bulletLimit;
    WaitForSeconds myWait;
    WaitForSeconds shootWait;
    List<GameObject> bulletPool;

    public LineRenderer aimLine;
    Ray myRay;
    RaycastHit hitInfo;

    float x;
    float z;
    float aimX;
    float aimZ;
    bool isShooting = false;

    float radius = 6f;
    public float sensitivity;

    Vector3 aimVector;

    void Start ()
    {
        myWait = new WaitForSeconds(rateOfFire);
        shootWait = new WaitForSeconds(shootingDelay);
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        bulletPool = new List<GameObject>();
        hitInfo = new RaycastHit();
        //for(int i = 0; i < bulletLimit; i++)
        //{
        //    bulletPool.Enqueue(Instantiate(bulletPre, Vector3.one * -1000, Quaternion.identity));
        //}
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

        //DrawAim();

        if((Input.GetButtonDown("Shoot") || Input.GetButtonDown("Shoot2")) && !isShooting)
        {
            //Debug.Log("SCHOOL SHOOTING");
            isShooting = true;
            StartCoroutine(ShootBullet());
        }
        //if(Input.GetButtonUp("Shoot") || Input.GetButtonUp("Shoot"))
        //{
        //    anim.SetBool("Shooting", false);
        //    StopAllCoroutines();
        //}
    }

    #region POOLING
    void InstantiateBullet()
    {
        GameObject temp;
        for (int i = 0; i < bulletLimit; i++)
        {
            temp = Instantiate(bulletPre);
            bulletPool.Add(temp);
            temp.SetActive(false);
        }

    }
    GameObject GetPooledBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeSelf)
            {
                return bulletPool[i];
            }
        }

        GameObject temp = Instantiate(bulletPre);
        bulletPool.Add(temp);
        return temp;

    }
    #endregion

    IEnumerator ShootBullet()
    {
        GameObject temp;
        for(int i = 0; i < 3; i++)
        {
            temp = GetPooledBullet();
            anim.SetBool("Shooting", true);
            temp.transform.position = spwnBullet.transform.position;
            temp.SetActive(true);
            temp.transform.LookAt((Vector3.right * aim.transform.position.x) + (Vector3.up * spwnBullet.transform.position.y) + (Vector3.forward * aim.transform.position.z));
            yield return myWait;
        }
        anim.SetBool("Shooting", false);
        yield return shootWait;
        isShooting = false;
    }

    void ShootSpecialAttack()
    {
        
    }

    void DrawAim()
    {
        myRay = new Ray(transform.position, (new Vector3(aim.transform.position.x, 0.5f, aim.transform.position.z) - spwnBullet.transform.position).normalized);
        aimLine.SetPosition(0, spwnBullet.transform.position);

        aimLine.SetPosition(1, new Vector3(aim.transform.position.x, 0.5f, aim.transform.position.z));
        //if(Physics.Raycast(myRay, out hitInfo, radius))
        //{
        //    Debug.Log("THERE");
        //    aimLine.SetPosition(1, new Vector3(hitInfo.point.x, 0.5f, hitInfo.point.z));
        //}
        //else
        //{
        //    aimLine.SetPosition(1, (aim.transform.position - spwnBullet.transform.position).normalized * radius);
        //}
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
