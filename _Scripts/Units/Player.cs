using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    public Stats stats;
    public int currentHealth;
    public UIHandler ui;
    private Rigidbody2D rb2D;
     GameManager gameManager;
    Animator anim;
    bool isFacingRight = true;
    bool canDash = true;
    bool isDashing = false;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = stats.maxHealth;
        timer = stats.regenerateTime;
        rb2D = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        ui.SetMaxHealth(stats.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // we cannot move if we are dashing
        if(isDashing){
            return;
        }
        //make sure the game is not paused
        if(!gameManager.IsPaused()){
            //if user clicked right click
            if(Input.GetMouseButtonDown(0)){
                DealDamageToNeighbors();
            }
            //check for movement
            Move();
            // we can dash and user clicked Left Shift
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
            }

            if(timer > 0){
                timer -= Time.deltaTime;
            }
            // start regeneratin [increase health by 10]
            if(timer <= 0 && currentHealth != stats.maxHealth){
                StartRegenerateHealth();
            }
            
        }
        

        
    }

    private void StartRegenerateHealth()
    {
        currentHealth += 10;
        ui.SetHealth(currentHealth);
        timer = stats.regenerateTime;
    }

    void Move(){
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Flip(x);
        rb2D.velocity = new Vector2(x,y) * stats.speed;
        anim.SetFloat("PlayerSpeed",Mathf.Abs(x));
    }

     private void Flip(float horizontal)
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
      private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb2D.velocity = new Vector2(transform.localScale.x * stats.dashingPower, 0f);
        yield return new WaitForSeconds(stats.dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(stats.dashingCooldown);
        canDash = true;   
    }
    

    public Vector3 GetObjectPosition(){
        return gameObject.transform.position;
    }
    public void TakeDamage(int dmg){
        currentHealth -= dmg;
        ui.SetHealth(currentHealth);
        timer = stats.regenerateTime;
        if(currentHealth <= 0){
            ui.SetGameOverMenu(true);
        }
    }
    public void DealDamageToNeighbors(){
        Collider2D[] objectsInsideRange = Physics2D.OverlapCircleAll(transform.position, stats.damageRadius);
        for(int i = 0; i < objectsInsideRange.Length;i++){
            if(objectsInsideRange[i].CompareTag("Enemy")){
                objectsInsideRange[i].GetComponent<EnemyUnitBase>().TakeDamage(stats.damage);
            }
        }
    }


    
}   
[Serializable]
public struct Stats{
    public int maxHealth;
    public float speed;
    public int damage;
    public float damageRadius;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;
    public float regenerateTime;
}

