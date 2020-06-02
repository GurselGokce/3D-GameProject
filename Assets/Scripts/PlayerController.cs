﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public LayerMask movementMask;
    Camera cam;
    PlayerMove move;
    public Interact focus;


    public Animator animator;
    private float speed;


    //Topdown Shooting
    public float moveSpeed = 8f;
    public Rigidbody rb;
    Vector3 movement;
    Vector3 mousePos;

    //

    void Start() {
        cam = Camera.main;
        move = GetComponent<PlayerMove>();
        //speed = 1f;

    }

    // Update is called once per frame
    void Update() {
        transform.Translate(speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, speed * Input.GetAxis("Vertical") * Time.deltaTime);
        animator.SetFloat("verticale", Input.GetAxis("Vertical"));
        animator.SetFloat("horizontale", Input.GetAxis("Horizontal"));

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Plane playerPlane = new Plane(Vector3.up, rb.position);
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if (playerPlane.Raycast(r, out hitDist)) //Player looking (rotating) at Mouse
        {
            Vector3 targetPoint = r.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - rb.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 7f * Time.deltaTime);
        }


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
                if(interact != null)
                {
                    SetFocus(interact);
                }
            }
        }

    }

    void FixedUpdate()
    {




        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //Vector3 lookDir = mousePos - rb.position;
        //rb.rotation = Quaternion.Slerp(mousePos - rb.position, Vector3.forward);
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; // Radiance to degrees
        //rb.rotation = angle;
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
