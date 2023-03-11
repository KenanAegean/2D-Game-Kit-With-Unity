using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    [SerializeField] private float maxSpeed;
    private int direction = 1;
    public int health = 100;
    private int maxHealth = 100;
    private RaycastHit2D rightLedgeRaycastHit;
    private RaycastHit2D leftLedgeRaycastHit;
    private RaycastHit2D rightWallRaycastHit;
    private RaycastHit2D leftWallRaycastHit;
    [SerializeField] private LayerMask rayCastLayerMask;

    [SerializeField] private Vector2 rayCastOffset;
    [SerializeField] private float rayCastLength = 1;
    [SerializeField] private int attackPower = 10; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(maxSpeed*direction, 0);

        //Check for right ledge!
        rightLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x + rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down, rayCastLength);
        Debug.DrawRay(new Vector2(transform.position.x + rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down * rayCastLength, Color.blue);
        if(rightLedgeRaycastHit.collider == null) direction = -1;

        //Check for left ledge!
        leftLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x - rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down, rayCastLength);
        Debug.DrawRay(new Vector2(transform.position.x - rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down * rayCastLength, Color.green);
        if (leftLedgeRaycastHit.collider == null) direction = 1;

        //Check for right wall!
        rightWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, rayCastLength/4, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right * rayCastLength, Color.red);
        if (rightWallRaycastHit.collider != null) direction = -1;

        //Check for left wall!
        leftWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, rayCastLength/4, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.left * rayCastLength, Color.magenta);
        if (leftWallRaycastHit.collider != null) direction = 1;

         //If health < 0, destroy me
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    //If I collide with the player, hurt the player (health is going to decrease, update the UI)
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject == NewPlayer.Instance.gameObject)
        {
            Debug.Log("Hurt the player!");
            NewPlayer.Instance.health -= attackPower;
            NewPlayer.Instance.UpdateUI();
        }
    }

    //will use later
    int ledgeChecker(RaycastHit2D rayCast)
    {
        if(rayCast.collider == null)
        {
            return direction *= -1;
        }
        else
        {
            Debug.Log("I am touching: " + rayCast.collider.gameObject);
            return 1;
        }
    }
}
