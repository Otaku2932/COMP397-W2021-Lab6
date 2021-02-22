using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CryptoState
{
    IDLE,
    RUN,
    JUMP
}

public class CryptoBehavior : MonoBehaviour
{
    [Header("Line of Sight")]
    /*
    public LayerMask collissionLayer;
    public Vector3 LOSoffset = new Vector3(0f, 2f, -5f);
    */
    public bool hasLOS;

    private GameObject player;

    private NavMeshAgent agent;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        RaycastHit hit;
        var size = new Vector3(4f, 2f, 10f);
        //hasLOS = Physics.BoxCast(transform.position + LOSoffset, transform.localScale, transform.forward, transform.rotation, 10f, collissionLayer);
        hasLOS = Physics.BoxCast(transform.position + LOSoffset, size, transform.forward, out hit, transform.rotation, 0f, collissionLayer);
        //hasLOS = Physics.BoxCast(transform.position + LOSoffset, size * 0.5f, transform.forward, transform.rotation, 10f);
        */

        if (hasLOS)
        {
            agent.SetDestination(player.transform.position);

            if (Vector3.Distance(transform.position, player.transform.position) < 2.5)
            {
                animator.SetInteger("AnimState", (int)CryptoState.IDLE);
                transform.LookAt(transform.position - player.transform.forward);
            }
            else
            {
                animator.SetInteger("AnimState", (int)CryptoState.RUN);
            }
        }
        else
        {
            animator.SetInteger("AnimState", (int)CryptoState.IDLE);
        }

        /*
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetInteger("AnimState", (int)CryptoState.IDLE);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetInteger("AnimState", (int)CryptoState.RUN);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetInteger("AnimState", (int)CryptoState.JUMP);
        }
        */
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            hasLOS = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasLOS = false;
        }
    }

    /*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + LOSoffset + transform.forward, new Vector3(4f, 2f, 10f));
    }
    */
}
