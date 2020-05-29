using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public LayerMask movementMask;
    Camera cam;
    PlayerMove move;
    public Interact focus;
    public Animator animator;
    private float speed;
    // Start is called before the first frame update
    void Start() {
        cam = Camera.main;
        move = GetComponent<PlayerMove>();
        speed = 1f;

    }

    // Update is called once per frame
    void Update() {
        transform.Translate(speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, speed * Input.GetAxis("Vertical") * Time.deltaTime); ;
        animator.SetFloat("verticale", Input.GetAxis("Vertical"));
        animator.SetFloat("horizontale", Input.GetAxis("Horizontal"));
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100, movementMask))
            {
                move.Move(hit.point);
                //Debug.Log("Hit" + hit.collider.name +" " + hit.point);
                //Move player to what hits

                //Stop focusing object
                RemoveFocus();

            
            }
        }

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

    void SetFocus(Interact newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
            move.FollowTarget(newFocus);

        }

        newFocus.OnFocus(transform);

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
