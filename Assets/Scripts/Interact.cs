using UnityEngine;

public class Interact : MonoBehaviour
{
    bool focus = false;
    bool interacted = false;
    public float radius = 3f;
    Transform player;

    public Transform InteractionTransform;


    public virtual void DoInteract()
    {
        Debug.Log("Interact with " + transform.name);
    }

    void Update()
    {
        if (focus && !interacted)
        {
            float distance = Vector3.Distance(player.position, InteractionTransform.position);
            if (distance <= radius)
            {
                DoInteract();
                interacted = true;
                //Debug.Log("Interact");
            }
        }
    }

    public void OnFocus(Transform playerTransform)
    {
        focus = true;
        player = playerTransform;
        interacted = false;
    }

    public void OnDefocused()
    {
        focus = false;
        player = null;
        interacted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if(InteractionTransform == null)
        {
            InteractionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(InteractionTransform.position, radius);

    }

}
