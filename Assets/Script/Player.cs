using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{ //Le focus pour une interaction
    public Interraction focus;

    //L'ennemie
    public EnnemyAI ennemy;

    //public Weapon weapon;
    public int Money;
    public int HealthPoint;
    public int HealthMax;
    public int Tenacity;
    public int Defence;
    public int Damage;
    public int Manapoint;
    public int Manamax;
    public int Sagacity;
    // public string[] Skill;
    public int MaxInventory = 2;
    public int Level;
    public float Experience;
    public float ExperienceMax;
    public Interraction[] Targets;
    public GameObject Animlvlup;

    // Variables concernant l'attaque / les interactions
    public float attackCooldown;
    public bool isAttacking = false;
    private float currentCooldown;
    public float attackRange;
    public GameObject rayHit;
     public float interractRange;
    public string inputInterract = "F";

    // Le personnage est-il mort ?
    public bool isDead = false;

    private CharacterController cc;
    public float speed = 6f;
    public float runspeed = 10f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    private Vector3 moveDirection = Vector3.zero;
    float toground = 0.8f;


    public Animator anim;
    private bool IsRunning = false;
    private bool IsWalking = false;
    void Start()
    {
        rayHit = GameObject.Find("Rayhit");
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    public int GetMaxInv()
    {
        return MaxInventory;
    }
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, toground);
    }

    public void UpdateStatus(int level)
    {
        HealthMax += 20;
        HealthPoint = HealthMax;
        Tenacity += 3;
        Defence += 3;
        Damage += 3;
        Manamax += 10;
        Manapoint = Manamax;
        Sagacity += 3;
    }
    void SetFocus(Interraction newFocus)
    {
        if (focus != null && focus != newFocus)
        {
            focus.PlusFocus();
        }
        focus = newFocus;
        newFocus.Focus(this);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.PlusFocus();
            focus = null;
        }
    }

    void Update()
    {
        //Level-UP
        if (Experience >= ExperienceMax)
        {
            Level += 1;
            Experience -= ExperienceMax;
            ExperienceMax *= 1.5f;
            UpdateStatus(Level);
            GameObject LVLUP = Instantiate(Animlvlup, transform.position, Quaternion.identity.normalized);
            Destroy(LVLUP, 5);
        }

        if (isGrounded())
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection *= runspeed;
                IsRunning = true;
                IsWalking = false;

            }
            else
            {
                moveDirection *= speed;
                IsRunning = false;
                IsWalking = true;
            }


            if (Input.GetKey(KeyCode.Space))
            {
                moveDirection.y = jumpSpeed;
            }

            if (moveDirection.x == 0 || moveDirection.z == 0)
            {
                IsWalking = false;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        anim.SetBool("IsRunning", IsRunning);
        anim.SetBool("IsWalking", IsWalking);
        cc.Move(moveDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            Attack();
        }
        else
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0)
            {
                currentCooldown = attackCooldown;
                isAttacking = false;
            }
        }
        if (Input.GetKeyDown(inputInterract))
        {
            Targets = FindObjectsOfType<Interraction>();
            foreach (var Target in Targets)
            {
                if (Vector3.Distance(Target.transform.position, transform.position) <= interractRange)
                {
                    Interraction interraction = Target.GetComponent<Interraction>();
                    if (interraction) //On vérifie qu'on a une interraction
                    {
                        Debug.Log("On tient un truc là !");
                        SetFocus(interraction);
                        interraction.Interract(this);
                        return;
                    }
                    else
                    {
                        Debug.Log("Trop loin");
                    }
                }
            }
        }

        //Lacher le focus
        if (Input.GetKeyDown(KeyCode.F4))
        {
            RemoveFocus();
        }  
    }


    // Fonction d'attaque
    public void Attack()
    {
        if (!isAttacking)
        {
            //animations.Play("attack");

            RaycastHit hit;
            isAttacking = true;

            if (Physics.Raycast(rayHit.transform.position, transform.TransformDirection(Vector3.forward), out hit, attackRange))
            {

                Debug.DrawLine(rayHit.transform.position, hit.point, Color.red);

                if (hit.transform.tag == "test")
                {
                    print(hit.transform.name + " detected");
                    ennemy = hit.transform.GetComponent<EnnemyAI>();
                    ennemy.Interract(this);
                }

            }
            
        }

    }


    public void Dead()
    {
        isDead = true;
        //animations.Play("die");
        Destroy(transform.gameObject,5);
    }
}
