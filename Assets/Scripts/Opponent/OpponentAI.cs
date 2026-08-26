using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class OpponentAI : MonoBehaviour 
{
    [Header("Opponent Movement")]
    public float movementSpeed=1f;
    public float rotationSpeed=10f;
    public CharacterController characterController;
    public Animator animator;

    [Header("Player Fight")]
    public float attackCoolDown=0.5f;
    public int attackDamages=5;
    public string[] attackAnimation={"Attack1Animation","Attack2Animation","Attack3Animation","Attack4Animation"};
    public float dodgeDistance=2f;
    public int attackCount=0;
    public float randomNumber;
    public float attackRadius=2f;
    public FightingController[] fightingController;
    public Transform[] players;
    public bool isTakingDamage;
    private float lastAttackTime;

    [Header("Effect and sound")]
    public ParticleSystem attack1Effect;
    public ParticleSystem attack2Effect;
    public ParticleSystem attack3Effect;
    public ParticleSystem attack4Effect;
    void Awake() {
        createRandomNumber();
    }
    void Update(){
        for(int i=0;i<fightingController.Length;i++){
            if(players[i].gameObject.activeSelf && Vector3.Distance(transform.position,players[i].position)<=attackRadius){
                animator.SetBool("Walking",false);
                if(Time.time-lastAttackTime > attackCoolDown){
                    int randomAttackIndex=Random.Range(0,attackAnimation.Length);
                    if(!isTakingDamage){
                        PerformAttack(randomAttackIndex);
                    }
                    fightingController[i].StartCoroutine(fightingController[i].PlayHitDamageAnimation(attackDamages));
                }

            }
            else{
                if(players[i].gameObject.activeSelf){
                    Vector3 direction=(players[i].position-transform.position).normalized;
                    characterController.Move(direction*movementSpeed*Time.deltaTime);

                    Quaternion targetRotation=Quaternion.LookRotation(direction);
                    transform.rotation=Quaternion.Slerp(transform.rotation,targetRotation,rotationSpeed*Time.deltaTime);

                    animator.SetBool("Walking",true);
                }
            }
        }
    }
    void PerformAttack(int attackIndex){
        
        animator.Play(attackAnimation[attackIndex]);
        int damage=attackDamages;
        Debug.Log("Performed attack "+(attackIndex+1)+" dealing "+damage+"damages");
        lastAttackTime=Time.time;
        
    }
    void PerformDodgeFront(){
        
        animator.Play("DodgeFrontAnimation");
        Vector3 dodgeDirection=-transform.forward*dodgeDistance;
        characterController.SimpleMove(dodgeDirection);
        
    }
    void createRandomNumber(){
        randomNumber=Random.Range(1,5);
    }
    public IEnumerator PlayHitDamageAnimation(int takeDamage){
        yield return new WaitForSeconds(0.5f);
        animator.Play("HitDamageAnimation");
    }
    public void Attack1Effect(){
        attack1Effect.Play();
    }
    public void Attack2Effect(){
        attack2Effect.Play();
    }
    public void Attack3Effect(){
        attack3Effect.Play();
    }
    public void Attack4Effect(){
        attack4Effect.Play();
    }



}