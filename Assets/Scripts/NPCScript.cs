using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCScript : MonoBehaviour
{

    private Vector3 machineCoordinate;
    public bool played = false;

    public BuildingGrid bg;
    Building[,] mas;
    public Building temp;

    List<Building> unTaken;

    public bool ava;

    public static float coins;
    public bool pay;


   

    void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.speed = 2f;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.angularSpeed = 999f;
        agent.acceleration = 40f;
        ava = true;
        //bg = FindObjectOfType<BuildingGrid>();
        coins = PlayerPrefs.GetFloat("Money", coins);

        //mas = bg.grid;


    }



    private void Awake()
    {
        bg = FindObjectOfType<BuildingGrid>();
        mas = BuildingGrid.grid;
        pay = true;
        //if (bg.grid is null)
        //{
        //    mas = bg.grid;
        //    Debug.Log("Nul");
        //}
        //Debug.Log(bg.grid[0,0]);

        unTaken = new List<Building>();
        foreach (Building b in mas)
        {
            if (b != null && !b.isTaken)
            {
                unTaken.Add(b);
            }
        }

        findNotTakenMachine();
        //ExpandArr();
        //Debug.Log(mas[0,0]);

        
    }

    private float secundomer = 0;
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Animator animator = GetComponent<Animator>();
        if (temp == null)
        {
            animator.Play("walking");// Чтоб остановить анимацию тоже пойдёт
            animator.SetBool("Table", false);
            ExitFromScene();
        }

        if (!played)
        {
            Move();
        }

        

       //Debug.Log((machineCoordinate - this.transform.position).magnitude);
        if ((machineCoordinate - this.transform.position).magnitude < 0.42f && temp != null)
        {
            temp.isTaken = true;
            animator.SetBool("IsWalking", false);
            //agent.angularSpeed = 0f;
            animator.SetBool("Table", true);
            if (pay)
            {
                
                temp.earnMoney();
                pay = false;
            }

            StartCoroutine(ExecuteAfterTime(temp));
        }

        
        if ((machineCoordinate - this.transform.position).magnitude > 0.91f && (machineCoordinate - this.transform.position).magnitude < 0.98f)
        {
            if (secundomer < 5) secundomer += Time.fixedDeltaTime;
            if (secundomer >= 5)
            {
                ExitFromScene();
            }
        }
    }

    private void ExitFromScene() 
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Animator animator = GetComponent<Animator>();
        
            temp.isTaken = false;
            played = true;
            animator.SetBool("IsWalking", true);
            animator.speed = 1f;
            agent.angularSpeed = 999f;
            agent.destination = new Vector3(5, 0, 0);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpawnPanel" && played)
        {
            Destroy(this.gameObject);
        }
    }
    public IEnumerator ExecuteAfterTime(Building a)
    {
        yield return new WaitForSeconds(Random.Range(5, 10));
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Table", false);
        ExitFromScene();
        //animator.Play("walking"); Чтоб остановить анимацию тоже пойдёт
        //a.isTaken = false;
        //played = true;
        //animator.SetBool("IsWalking", true);
        //animator.speed = 1f;
        //agent.angularSpeed = 999f;
        //agent.destination = new Vector3(5, 0, 0);
    }
    public Building findNotTakenMachine()
    {
        

        Building choice = unTaken[Random.Range(0, unTaken.Count)];

        machineCoordinate = new Vector3(choice.nonStaticX, 0, choice.nonStaticZ);
        
        choice.isTaken = true;
        //Debug.Log(choice.isTaken);
        temp = choice;
            

        return choice;

    }
    public void Move()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Animator animator = GetComponent<Animator>();

        animator.SetBool("Table", false);
        agent.angularSpeed = 999f;
        agent.destination = machineCoordinate;
        animator.SetBool("IsWalking", true);

    }
}
