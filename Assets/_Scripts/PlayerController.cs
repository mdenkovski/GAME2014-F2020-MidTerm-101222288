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
    public float horizontalBoundary;

    /// <summary>
    /// the speed at which the player can move
    /// </summary>
    [Header("Player Speed")]
    public float verticalSpeed;
    public float maxSpeed;
    public float verticalTValue;
    public float horizontalSpeed;
    public float horizontalTValue;

    [Header("Bullet Firing")]
    public float fireDelay;

    // Private variables
    private Rigidbody2D m_rigidBody;
    private Vector3 m_touchesEnded;
    private bool m_bOrientationChanged;
    ScreenOrientation m_screenOrientation;

    // Start is called before the first frame update
    void Start()
    {
        m_touchesEnded = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_screenOrientation = Screen.orientation;
        m_bOrientationChanged = false;
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            _SetLandscapePosition();
        }
        else
        {
            _SetPortraitPosition();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        { 
            //update our internal orientation to landcape
            if (m_screenOrientation != ScreenOrientation.LandscapeLeft && m_screenOrientation != ScreenOrientation.LandscapeRight)
            {
                m_screenOrientation = Screen.orientation;
                m_bOrientationChanged = true;
                Debug.Log("orientation changed");
            }
            if (m_bOrientationChanged)
            {
                _SetLandscapePosition();
            }
            _Move();
            _CheckBounds();
            _FireBullet();
        }
        if(Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            //update our internal orientation to portrait
            if (m_screenOrientation != ScreenOrientation.Portrait && m_screenOrientation  != ScreenOrientation.PortraitUpsideDown)
            {
                m_screenOrientation = Screen.orientation;
                m_bOrientationChanged = true;
                Debug.Log("orientation changed");
            }
            if (m_bOrientationChanged)
            {
                _SetPortraitPosition();
            }
            _MoveHorizontal();
            _CheckBoundsHorizontal();
            _FireBullet();
        }
        Debug.Log(Screen.orientation);

    }

    /// <summary>
    /// move the player to the default landscape position
    /// </summary>
    private void _SetLandscapePosition()
    {
        //transform.position = new Vector2(-7.44f, 0.0f);
        //transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        transform.SetPositionAndRotation(new Vector2(-7.44f, 0.0f), Quaternion.Euler(0.0f, 0.0f, -90.0f));
    }

    private void _SetPortraitPosition()
    {
        //transform.position = new Vector2(0.0f, -4.0f); 
        Quaternion.Euler(0.0f, 0.0f, 90.0f);
        transform.SetPositionAndRotation(new Vector2(0.0f, -4.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
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
    /// move the player along the x axis left or right
    /// </summary>
    private void _MoveHorizontal()
    {
        float direction = 0.0f;

        // touch input support
        foreach (var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

            if (worldTouch.x > transform.position.x)
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
        //use the horizontal axis for the movememnt
        // keyboard support
        if (Input.GetAxis("Horizontal") >= 0.1f)
        {
            // direction is positive
            direction = 1.0f;
        }
        //use the horizontal axis for the movememnt
        if (Input.GetAxis("Horizontal") <= -0.1f)
        {
            // direction is negative
            direction = -1.0f;
        }

        if (m_touchesEnded.x != 0.0f)
        {
            //move the player along the x axis
            transform.position = new Vector2(Mathf.Lerp(transform.position.x, m_touchesEnded.x, horizontalTValue), transform.position.y);
        }
        else
        {
            //set y to 0 and x value based on direction and speed
            Vector2 newVelocity = m_rigidBody.velocity + new Vector2(direction * horizontalSpeed, 0.0f);
            m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
            m_rigidBody.velocity *= 0.99f;
        }
    }

    /// <summary>
    /// check the vertical bounds of the player to make sure it is within
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
    
    /// <summary>
    /// check the horizontal bounds of the player to make sure it is within
    /// </summary>
    private void _CheckBoundsHorizontal()
    {
        // check top bounds
        if (transform.position.x >= horizontalBoundary)
        {
            //move the player in bounds based on horizontal boundary
            transform.position = new Vector3(horizontalBoundary, transform.position.y, 0.0f);
        }

        // check bottom bounds
        if (transform.position.x <= -horizontalBoundary)
        {
            //move the player in bounds based on horizontal boundary
            transform.position = new Vector3(-horizontalBoundary, transform.position.y, 0.0f);
        }

    }
}
