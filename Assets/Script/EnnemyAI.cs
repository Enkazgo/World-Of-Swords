using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyAI : Interraction
{

    public int Level;
    public int HealthPoint;
    public int HealthMax; //Pour la barre de vie
    public int Defence;
    public int Damage;
    public int Manapoint; //Pour les mages
    public int Manamax; //Pour les mages
    public int Sagacity; //Pour les mages
    public GameObject lootdrop;
    public GameObject loot_a;

    //Variable de patrouille
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float WaitTime;
    private float WaitTimeCount;
    //Distance entre le joueur et l'ennemi
    private float Distance;

    // Cible de l'ennemi
    public Transform Target;

    //Distance de poursuite
    public float chaseRange = 10;

    // Portée des attaques
    public float attackRange = 2.2f;

    // Cooldown des attaques
    public float attackRepeatTime = 1;
    private float attackTime;

    // Agent de navigation
    private NavMeshAgent agent;

    // Animations de l'ennemi
    //private Animation animations;

    // Vie de l'ennemi
    private bool isDead = false;

    // Animations de l'ennemi
    private Animator animations;
    private bool IsWalking = false;
    private bool IsRunning = false;


    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        animations = gameObject.GetComponent<Animator>();
        attackTime = Time.time;
        WaitTimeCount = WaitTime;
    }



    void Update()
    {
        animations.SetBool("IsWalking", IsWalking);
        animations.SetBool("IsRunning", IsRunning);

        if (!isDead)
        {

            // On cherche le joueur en permanence
            Target = GameObject.Find("Player").transform;

            // On calcule la distance entre le joueur et l'ennemi, en fonction de cette distance on effectue diverses actions
            Distance = Vector3.Distance(Target.position, transform.position);

            // Quand l'ennemi est loin = idle
            if (Distance > chaseRange)
            {
                Patrole();
            }

            // Quand l'ennemi est proche mais pas assez pour attaquer
            if (Distance < chaseRange && Distance > attackRange)
            {
                chase();
            }

            // Quand l'ennemi est assez proche pour attaquer
            if (Distance < attackRange)
            {
                attack(Target.GetComponent<Player>());
            }

            if (HealthPoint <= 0)
            {
                Dead(Target.GetComponent<Player>());
            }
        }

        // poursuite
        void chase()
        {
            IsRunning = true;
            IsWalking = false;
            //animations.Play("IsRunning");
            agent.destination = Target.position;
        }

        void Patrole()
        {
            if (!walkPointSet)
            {
                SearchWalkPoint();
            }
            else
            {
                agent.SetDestination(walkPoint);
            }
            Vector3 distancetopoint = transform.position - walkPoint;
            if (distancetopoint.magnitude < 1f)
            {
                if (WaitTime <= 0)
                {
                    WaitTime = WaitTimeCount;
                    walkPointSet = false;
                }
                else
                {
                    WaitTime -= Time.deltaTime;
                }

            }
        }
        void SearchWalkPoint()
        {
            float randomz = Random.Range(-walkPointRange, walkPointRange);
            float randomx = Random.Range(-walkPointRange, walkPointRange);
            walkPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomz);
            walkPointSet = true;
            IsWalking = true;
            IsRunning = false;
        }

        // Combat
        void attack(Player player)
        {
            // empeche l'ennemi de traverser le joueur
            agent.destination = transform.position;

            //Si pas de cooldown
            if (Time.time > attackTime)
            {
                //animations.Play("hit");
                player.HealthPoint -= ((2 * Level / 5) + 2) * (Damage / player.Defence);
                attackTime = Time.time + attackRepeatTime;
            }
        }

        // idle
        /*void idle()
        {
            // animations.Play("idle");
        }*/
    }

   public override void Interract(Player player)
    {
        base.Interract(player);

        if (!isDead && player.isAttacking)
        {
            int dmg = ((2 * player.Level / 5) + 2) * (player.Damage / Defence);
            HealthPoint -= dmg ;
            Debug.Log("On a infligé "+dmg+" point de dégats");
            Debug.Log(HealthPoint);
            if (HealthPoint <= 0)
            {
                Dead(player);
            }
        }
    }

    public void Dead(Player player)
    {
        isDead = true;
        //animations.Play("die");
        player.Money += Random.Range(0, 100);
        player.Experience += 10;
        Instantiate(lootdrop, loot_a.transform.position, Quaternion.identity);
        Destroy(transform.gameObject);
    }
}
