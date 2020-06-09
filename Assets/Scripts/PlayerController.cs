using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public LayerMask movementMask;
    Camera cam;
    PlayerMove move;
    public Interact focus;


    Animator animator;
    private float speed;


    //Topdown Shooting
    public float moveSpeed = 8f;
    public float jumpHeight = 7f;
    public Rigidbody rb;
    Vector3 movement;
    Vector3 mousePos;
    Vector3 axisVector;

    Vector3 targetPoint;

    bool isFalling = false;
    bool hasJumped = false;




    void Start()
    {
        cam = Camera.main;
        move = GetComponent<PlayerMove>();

        animator = GetComponent<Animator>();
        //Cursor.visible = false;

    }


    void Update()
    {

        axisVector = new Vector3(
        Input.GetAxis("Horizontal"),
        0, Input.GetAxis("Vertical"
        ));
        bool isShiftKeyDown = Input.GetKey(KeyCode.LeftShift);

        if ((isShiftKeyDown) && (!hasJumped) && (!isFalling))
        {
            moveSpeed = 12f;
        }

        else
        {
            moveSpeed = 8f;
        }

        if (rb.velocity.y > 0.1)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (2.5f - 1f) * Time.deltaTime;

            hasJumped = true;
        }
        //if (rb.velocity.y == 0.0f)
        //{
        //    hasJumped = false;
        //}
        RaycastHit hitGround;
        float distance = 1f;
        Vector3 d = new Vector3(0, -1);
        if (Physics.Raycast(transform.position, d, out hitGround, distance)) //Grounded, tekent een raycast onder de speler om te zien of het op de grond zit of niet.
        {
            hasJumped = false;
            isFalling = false;
        }

        if (rb.velocity.y < -0.1f)
        {

            rb.velocity+=Vector3.up * Physics.gravity.y * (2f - 1f) *Time.deltaTime;
            isFalling = true;

        }
        else
        {
            isFalling = false;
        }
        if (Input.GetButtonDown("Jump") && !isFalling && !hasJumped)
        {
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }


        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = 2;


        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, rb.position);
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if (playerPlane.Raycast(r, out hitDist)) //Player looking (rotating) at Mouse
        {
            targetPoint = r.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - rb.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 7f * Time.deltaTime);
            lineRenderer.SetPosition(1, new Vector3(0, 0, hitDist));
        }



        //UpdateAnimator();
        //Movement with mouse key
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit, 100, movementMask))
        //    {
        //        move.Move(hit.point);
        //        //Debug.Log("Hit" + hit.collider.name +" " + hit.point);
        //        //Move player to what hits

        //        //Stop focusing object
        //        RemoveFocus();


        //    }
        //}

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //move.Move(hit.point);
                Interact interact = hit.collider.GetComponent<Interact>();
                if (interact != null)
                {
                    SetFocus(interact);
                }
            }
        }

    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + axisVector * moveSpeed * Time.fixedDeltaTime);
        UpdateAnimator();

        //Vector3 lookDir = mousePos - rb.position;
        //rb.rotation = Quaternion.Slerp(mousePos - rb.position, Vector3.forward);
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; // Radiance to degrees
        //rb.rotation = angle;
    }

    private void UpdateAnimator()
    {
        float forwardBackwardsMagnitude = 0;
        float rightLeftMagnitude = 0;
        if (axisVector.magnitude > 0)
        {
            Vector3 normalizedLookingAt = /*lookedAtPoint -*/targetPoint - transform.position;
            normalizedLookingAt.Normalize();
            forwardBackwardsMagnitude = Mathf.Clamp(
                    Vector3.Dot(axisVector, normalizedLookingAt), -1, 1
            );

            Vector3 perpendicularLookingAt = new Vector3(
                   normalizedLookingAt.z, 0, -normalizedLookingAt.x
            );
            rightLeftMagnitude = Mathf.Clamp(
                   Vector3.Dot(axisVector, perpendicularLookingAt), -1, 1
           );

            animator.SetBool("IsMoving", true);

        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        // update the animator parameters
        animator.SetFloat("Forward", forwardBackwardsMagnitude);
        animator.SetFloat("Right", rightLeftMagnitude);
    }



    void SetFocus(Interact newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
            //move.FollowTarget(newFocus);

        }

        newFocus.OnFocus(transform/*rb*/);

    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
        move.StopFollowingTarget();
    }
}
