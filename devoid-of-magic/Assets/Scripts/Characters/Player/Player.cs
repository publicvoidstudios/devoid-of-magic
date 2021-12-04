using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player retrievable progress
    public int level; //Player level
    public int sublevel; //Player sublevel
    public int progress; //Current game scenario progression
    public int axeLevel; //Level of AXE skill
    public int bowLevel; //Level of BOW skill
    public int armorLevel; //Level of ARMOR skill
    public int strengthLevel; //Level of HP skill
    public int gold; //Gold \_(-_-)_/
    public string player_name; //Player nickname 
    public int tools; //Player tools for instant repairs
    public int magicDamage; //Level of MAGIC skill
    //End of retrievable attributes
    [HideInInspector]
    public bool respawned; //Has a Player already used ADs to ressurrect? 
    public float speed; //player speed. Def = 3
    [HideInInspector]
    public int maxHealth; // player health
    public int currentHealth; // player's current health
    [HideInInspector]
    public int maxArmor; //player armor
    public int currentArmor; // player's current armor
    public int range; //Player's range of attacks using bow. Note: All enemies depend on this attribute, they will see Player and try attacking within this range
    private bool armorBroken; //Armor eq. 0? 
    private bool switchDamagetoHealth; //If armor eq 0, has to be true
    [SerializeField] private float jumpForce = 0.5f; // jump force
    private bool isGrounded = false; //Standing on the ground?
    [HideInInspector] 
    public bool lookingRight; // Obvious
    public Transform feetPos; //Starting point to check ground in raduis
    public float checkRadius; //Radius in which ground is being checked
    public LayerMask whatIsGround; //WHAT IS GROUND???
    public Health healthBar; //Health script
    public Armor armorBar; //Armor script
    public Joystick joystick; //Reference to main movement controller
    private Rigidbody2D rb; //Rigidbody2D
    public Animator anim; //Animator
    [HideInInspector]
    public bool activeQuest; //Has active quest?
    private bool jumpCooldown; //Can jump already?
    public GameObject arrow; //Projectile prefab
    public Transform arrowSpawnPoint; //Point of instantiation of prefab
    [HideInInspector] 
    public bool isDead; //Player dead?
    public GameObject[] enemies; //All enemies on the scene, to find later the closest among them
    public static Player Instance { get; set; } //Purpose: Unknown. Do not touch. Lol
    [HideInInspector]
    public int jumpCount; //Jump counter
    public int jumpCountE; //Editable variable of jumps
    public bool attacking; //Is Player attacking now?

    private void Awake()
    {
        Load(); //Load progress
        maxHealth = 100 + (strengthLevel * 6); //Set max health accordingly to current progress (skill level)
        maxArmor = 100 + (armorLevel * 4); //Same for armor       
        Instance = this; //Purpose: Unknown
        rb = GetComponent<Rigidbody2D>(); //Get rigidbody2d
        anim = GetComponent<Animator>(); //Get animator
    }
    private void Start()
    {        
        isDead = false; //Player alive by default
        currentHealth = maxHealth; //On each level Player get's max health on the start, armor saves if still not broken
        StartCoroutine(StartHealthRestore()); //Prevent's health to stay on 100 if HP level is not 0
        armorBroken = false; //By default armor is considered to be not broken
        switchDamagetoHealth = false; //One more action for Damage Switcher
        jumpCooldown = true; //Can jump right at the start        
    }    
    IEnumerator StartHealthRestore() //Delayed by small amount of time to prevent errors in setting Player's HP to maximum
    {
        yield return new WaitForSeconds(0.1f); //Wait
        currentHealth = maxHealth; //Do
    }

    
    private void Update()
    {
        attacking = anim.GetBool("Attacking"); //Detect's if Player is attacking by getting this info from animator
        if (isGrounded) //When is grounded refreshes jump counter to default value
        {
            jumpCount = jumpCountE;
        }
        anim.SetBool("InAir", !isGrounded); //Set bool in animator accordingly to Player's state
        anim.SetBool("DoubleJump", jumpCount < jumpCountE); //Detection if Player is jumping more than 1 time in a row        
        anim.SetBool("Dead", isDead); //Connection between script and animator, detecting if Player is alive or dead
        healthBar.SetMaxAmount(maxHealth); //max health always equals value that depends on Player's skill level
        armorBar.SetMaxAmount(maxArmor); //Same for armor
        healthBar.SetAmount(currentHealth); //Set current HP to slider in GUI
        armorBar.SetAmount(currentArmor); //Same for Armor
        if(currentHealth > maxHealth) //Current HP can't be more than max
        {
            currentHealth = maxHealth;
        }
        if(currentArmor > maxArmor) //Same for armor
        {
            currentArmor = maxArmor;
        }
        maxHealth = 100 + (strengthLevel * 6); //Set max HP if it was changed (Ex. if skill upgraded)
        maxArmor = 50 + (armorLevel * 4); //same for armor
        enemies = GameObject.FindGameObjectsWithTag("Enemy"); //Keep enemies array filled and updated
        if (Input.GetKeyDown(KeyCode.P)) //Testing func.
        {
            DamageRecount(20); //Get 20 damage
        }
        if (Input.GetKeyDown(KeyCode.O)) //Save game instantly
        {
            Save();
            Debug.Log("Progress Saved");
        }
        if (currentHealth > 0) //If Player is alive
        {
            if (Input.GetButton("Horizontal") || joystick.Horizontal != 0) //Horizontal movement
            {
                anim.SetFloat("Speed", 1);
                Run();
            }
            else //Not moving in horizontal axis
            {
                anim.SetFloat("Speed", 0);
            }

            if (Input.GetKeyDown(KeyCode.R)) //Keyboard controls for testing.  Jump func
            {
                Jump();
            }
                
            if (isGrounded == true && joystick.Vertical > 0.5f) //Unused function for all axis joystick. Vertical movement was replaced by Jump button.
            {
                JumpJoystick();
            }
        }        
        if (currentHealth <= 0 && !isDead) // Player just died. Need to set bool to another state
        {
            isDead = true;            
            Die();
        }
        if (level == 0 || level == 2 || level == 4) //Detection of quest
        {
            activeQuest = false;
        }
        else //Player has a quest
        {
            activeQuest = true;
        }
    }

    public void JumpButton() //Jump button method
    {
        Jump();
    }
    public void DamageRecount(int damage) //Method for switching damage to HP if armor is empty.
    {
        if (armorBroken == false)
        {
            GetDamage(damage/2);
            anim.SetTrigger("Hurt");
            speed = 3;
        }
        if (armorBroken == true)
        {
            StartCoroutine(DamageSwitcher());
        }
        if (switchDamagetoHealth == true)
        {
            GetDamageHealth(damage);
            anim.SetTrigger("Hurt");
            speed = 3;
        }
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

    }
    private void JumpJoystick() //Outdated
    {
        if (jumpCooldown)
        {
            StartCoroutine(JumpCooldown());
        }
    }
    private void RunJoystick() // Relic
    {
        Vector3 dir = transform.right * joystick.Horizontal;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        if (dir.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            lookingRight = true;
        }
        if (dir.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            lookingRight = false;
        }
    }
    private void FixedUpdate() //Only to check ground
    {
        CheckGround();
    }
    private void TransformLocalScale(float X) //Turns Player left or right. Receives 1 or -1 for X
    {
        if (!attacking)
        {
            transform.localScale = new Vector3(X, 1, 1);
        }
        
    }

    private void Run() //Movement method
    {
        if(Input.GetAxis("Horizontal") != 0) //Input detection
        {
            Vector3 dir = transform.right * Input.GetAxis("Horizontal"); //Set direction vector
            transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime); //Move object
            if (dir.x > 0) //Move right
            {
                TransformLocalScale(1f); //Turn object right
                lookingRight = true; //Set bool
            }
            if (dir.x < 0) //Move left
            {
                TransformLocalScale(-1f); //Turn left
                lookingRight = false; //Set bool
            }
        }
        if(joystick.Horizontal != 0) //Same for joystick controls. Receives movement data from on screen joystick input.
        {
            Vector3 dir = transform.right * joystick.Horizontal;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
            if (dir.x > 0)
            {
                TransformLocalScale(1f);
                lookingRight = true;
            }
            if (dir.x < 0)
            {
                TransformLocalScale(-1f);
                lookingRight = false;
            }
        }
        
    }
    private void Jump() //JUMP!
    {
        if (jumpCount > 0) //If jumps left
        {
            jumpCount -= 1; //Decrease amount of jumps left
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); //Push RB up!
        }
    }
    IEnumerator JumpCooldown() //Coroutine doing nearly the same. But it has a cooldown.
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Force);
        if (!isGrounded)
        {
            anim.SetBool("InAir", !isGrounded);
        }
        else
        {
            anim.SetBool("InAir", false);
        }
        //rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.01f);
        jumpCooldown = false;
        yield return new WaitForSeconds(0.99f);
        jumpCooldown = true;
    }
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround); //Draw a circle around feetPos GO, and detect if objects of ground layer mask are in range.
    }
    public void GetDamage(int damage) //Makes Player mortal. Get damage to armor
    {
        if(currentArmor > 0)
        {
            currentArmor -= damage;
            if (currentArmor < 0)
            {
                currentArmor = 0;
            }
            armorBar.SetAmount(currentArmor);
        }
        if(currentArmor == 0)
        {
            armorBroken = true;
        }        
    }
    public void GetDamageHealth(int damage) //Get damage to health
    {
        currentHealth -= damage;
        healthBar.SetAmount(currentHealth);       
    }
    IEnumerator DamageSwitcher() //Switches damage from armor to health
    {
        yield return new WaitForSeconds(0.01f);
        switchDamagetoHealth = true;
    }
    public void Die() //Called is Player is dying.
    {
        Save(); //Saves current progress.
        GameOver gameOver = GameObject.FindGameObjectWithTag("GameOver").GetComponent<GameOver>(); //Find GO containing GameOver script by tag
        gameOver.gameOverPanel.SetActive(true); // Activate YOU DIED canvas
    }
    public void PlaySound(string name) //Sound. Requires name of Sound to play.
    {
        AudioManager audio = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
        audio.Play(name);
    }
    public void Save()
    {
        SaveProtocol.SaveProgress(this);
    }
    public void Load() //Which data to load?
    {
        PlayerData data = SaveProtocol.LoadProgress();
        //Switch attributes to newly loaded
        level = data.level;
        sublevel = data.sublevel;
        progress = data.progress;
        axeLevel = data.axeLevel;
        bowLevel = data.bowLevel;
        armorLevel = data.armorLevel;
        strengthLevel = data.strengthLevel;
        gold = data.gold;
        player_name = data.player_name;
        tools = data.tools;
        currentArmor = data.currentArmor;
        magicDamage = data.magicDamage;
    }

}
