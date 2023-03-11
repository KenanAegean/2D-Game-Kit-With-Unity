using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayer : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpPower = 10;
    [SerializeField] public int coinsCollected;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int health = 100;
    [SerializeField] public int ammo;
    [SerializeField] private GameObject attackBox;
    [SerializeField] private float attackDuration = 0.1f;

    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();
    public Image inventoryItemImage;  
    public Sprite keySprite;
    public Sprite keyGemSprite;
    public Sprite inventoryItemBlank;
    public int attackPower = 25;
    public Text coinsText;
    public Image healthBar;
    private Vector2 healthBarOrigSize;

    
    //Singleton instantation
    private static NewPlayer instance;
    public static NewPlayer Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }


    
    // Start is called before the first frame update
    void Start()
    {
        healthBarOrigSize = healthBar.rectTransform.sizeDelta;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);
        
        // if player press "Jump" and  grounded, set the velocity to a jump power value
        if (Input.GetButtonDown("Jump") && grounded)
        {        
            velocity.y = jumpPower;    
        }

        //Flip the player
        if ( targetVelocity.x < -.01 )
        {
            transform.localScale = new Vector2(-1,1);
        }
        else if( targetVelocity.x > .01 )
        {
            transform.localScale = new Vector2(1,1);
        }

        //ıf we prees fire1, then set
        if (Input.GetButtonDown("Fire1"))
        {
            //start coroutine
            StartCoroutine(ActivateAttack());
        }
        
        
    }

    //Activate Attack Function
    public IEnumerator ActivateAttack()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(false);
    }

    //Update UI Elements
    public void UpdateUI()
    {
        coinsText.text = coinsCollected.ToString();

        healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)health / (float)maxHealth), healthBarOrigSize.y);
    }

    public void AddInventoryItem(string inventoryName, Sprite image = null)
    {
        inventory.Add(inventoryName, image);
        inventoryItemImage.sprite = inventory[inventoryName];
    }

    public void RemoveInventoryItem(string inventoryName)
    {
        inventory.Remove(inventoryName);
        inventoryItemImage.sprite = inventoryItemBlank;
    }
}
