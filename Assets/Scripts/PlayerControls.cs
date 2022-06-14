using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControls : MonoBehaviour
{
    public GameObject target;
    public Collider groundCollider;
    public GameObject table;

    public static bool canWalk = true;
    void Start()
    {

        this.target.transform.position = this.transform.position;
        this.target.SetActive(false);

        Animator animator = GetComponent<Animator>();
        animator.speed = 2f;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.angularSpeed = 999f;
        agent.acceleration = 40f;
    }

    

    void Update()
    {
        Move();
    }

    private void OnAnimatorMove()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Animator animator = GetComponent<Animator>();
        float speed = (animator.deltaPosition / Time.deltaTime).magnitude;
        if (Time.timeScale != 0)
        {
            agent.speed = speed;
        }
        
    }

    public void Move() 
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Animator animator = GetComponent<Animator>();

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Table", false);
            agent.angularSpeed = 999f;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;

            if (Physics.Raycast(ray, out hitData, 1000))
            {
                if (this.groundCollider.Equals(hitData.collider) && canWalk)
                {
                    this.target.transform.position = hitData.point;
                    agent.destination = hitData.point;
                    this.target.SetActive(true);
                    animator.SetBool("IsWalking", true);
                }
            }
        }
        if ((this.target.transform.position - this.transform.position).magnitude < 0.2f)
        {
            this.target.SetActive(false);
            animator.SetBool("IsWalking", false);
            if (ClickOnTableReciever.activate)
            {
                agent.angularSpeed = 0f;
                this.transform.localEulerAngles = new Vector3(0, 270, 0);
                //animator.SetBool("IsWalking", true);
                animator.SetBool("Table", true);
            }
            ClickOnTableReciever.activate = false;
        }
    }


    public void MoveToTable() 
    {
        Animator animator = GetComponent<Animator>();

        if (ClickOnTableReciever.activate && canWalk)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = target.transform.position;
            this.target.SetActive(true);
            
            animator.SetBool("IsWalking", true);
        }
    }
    
}
