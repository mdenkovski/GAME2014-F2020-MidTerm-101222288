using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

/// <summary>
/// PlayerController.cs
/// Michael Denkovski 101222288
/// GAME 2014 Mobile Game Dev
/// Last Modified Oct 20
/// - changed variable names to reflect vertical movement orientation in landscape
/// - adjusted functions to change from x axis to y axis movement (horizontal to vertical)
/// </summary>
public class PlayerController : MonoBehaviour
{
    public BulletManager bulletManager;

    /// <summary>
    /// how far up or down the player can go
    /// </summary>
    [Header("Boundary Check")]
    public float verticalBoundary;

    /// <summary>
    /// the speed at which the player can move
    /// </summary>
    [Header("Player Speed")]
    public float verticalSpeed;
    public float maxSpeed;
    public float verticalTValue;

    [Header("Bullet Firing")]
    public float fireDelay;

    // Private variables
    private Rigidbody2D m_rigidBody;
    private Vector3 m_touchesEnded;

    // Start is called before the first frame update
    void Start()
    {
        m_touchesEnded = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
        _FireBullet();
    }

     private void _FireBullet()
    {
        // delay bullet firing 
        if(Time.frameCount % 60 == 0 && bulletManager.HasBullets())
        {
            bulletManager.GetBullet(transform.position);
        }
    }

    /// <summary>
    /// move the player along the y axis up or down
    /// </summary>
    private void _Move()
    {
        float direction = 0.0f;

        // touch input support
        foreach (var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

            if (worldTouch.y > transform.position.y)
            {
                // direction is positive
                direction = 1.0f;
            }

            if (worldTouch.y < transform.position.y)
            {
                // direction is negative
                direction = -1.0f;
            }

            m_touchesEnded = worldTouch;

        }
        //use the vertical axis for the movememnt
        // keyboard support
        if (Input.GetAxis("Vertical") >= 0.1f) 
        {
            // direction is positive
            direction = 1.0f;
        }
        //use the vertical axis for the movememnt
        if (Input.GetAxis("Vertical") <= -0.1f)
        {
            // direction is negative
            direction = -1.0f;
        }

        if (m_touchesEnded.y != 0.0f)
        {
            //move the player along the y axis
           transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, m_touchesEnded.y, verticalTValue));
        }
        else
        {
            //set x to 0 and y value based on direction and speed
            Vector2 newVelocity = m_rigidBody.velocity + new Vector2(0.0f, direction * verticalSpeed);
            m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
            m_rigidBody.velocity *= 0.99f;
        }
    }

    /// <summary>
    /// check the vertical bounds of the ship to make sure it is within
    /// </summary>
    private void _CheckBounds()
    {
        // check top bounds
        if (transform.position.y >= verticalBoundary)
        {
            //move the player in bounds based on vertical boundary
            transform.position = new Vector3(transform.position.x, verticalBoundary, 0.0f);
        }

        // check bottom bounds
        if (transform.position.y <= -verticalBoundary)
        {
            //move the player in bounds based on vertical boundary
            transform.position = new Vector3(transform.position.x, -verticalBoundary, 0.0f);
        }

    }
}
