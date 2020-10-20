using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// BulletController.cs
/// Michael Denkovski 101222288
/// GAME 2014 Mobile Game Dev
/// Last Modified Oct 20
/// - changed variable names to reflect horizontal movement orientation in landscape
/// - move along x axis instead of y
/// -check horizontal boundaries
/// -bullet to return on orientation change
/// - bullet able to move based on responsive layout
/// </summary>
public class BulletController : MonoBehaviour, IApplyDamage
{
    public float horizontalSpeed;
    public float horizontalBoundary;
    public float verticalSpeed;
    public float verticalBoundary;
    public BulletManager bulletManager;
    public int damage;

    /// <summary>
    /// private variables
    /// </summary>
    private bool m_bOrientationChanged;
    ScreenOrientation m_screenOrientation;

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
        m_screenOrientation = Screen.orientation;
        m_bOrientationChanged = false;
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
            }
            if (m_bOrientationChanged)
            {
                //return the bullet if orientation changed
                bulletManager.ReturnBullet(gameObject);
            }
            _MoveHorizontal();
            _CheckBoundsHorizontal();
        }
        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            //update our internal orientation to portrait
            if (m_screenOrientation != ScreenOrientation.Portrait && m_screenOrientation != ScreenOrientation.PortraitUpsideDown)
            {
                m_screenOrientation = Screen.orientation;
                m_bOrientationChanged = true;
            }
            if (m_bOrientationChanged)
            {
                //return the bullet if orientation changed
                bulletManager.ReturnBullet(gameObject);
            }
            _MoveVertical();
            _CheckBoundsVertical();
        }
        

    }

    /// <summary>
    /// move the bullet along the x axis based on its horizontal speed
    /// </summary>
    private void _MoveHorizontal()
    {
        transform.position += new Vector3(horizontalSpeed, 0.0f, 0.0f) * Time.deltaTime;
    }

    /// <summary>
    /// move the bullet along the y axis based on its vertical speed
    /// </summary>
    private void _MoveVertical()
    {
        transform.position += new Vector3(0.0f, verticalSpeed, 0.0f) * Time.deltaTime;
    }

    /// <summary>
    /// check the bullet if it is past the horizontal boundary
    /// </summary>
    private void _CheckBoundsHorizontal()
    {
        if (transform.position.x > horizontalBoundary)
        {
            bulletManager.ReturnBullet(gameObject);
        }
    }

    /// <summary>
    /// check the bullet if it is past the vertical boundary
    /// </summary>
    private void _CheckBoundsVertical()
    {
        if (transform.position.y > verticalBoundary)
        {
            bulletManager.ReturnBullet(gameObject);
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);
        bulletManager.ReturnBullet(gameObject);
    }

    public int ApplyDamage()
    {
        return damage;
    }
}
