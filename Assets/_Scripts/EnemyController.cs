using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// EnemyController.cs
/// Michael Denkovski 101222288
/// GAME 2014 Mobile Game Dev
/// Last Modified Oct 20
/// - changed variable names to reflect vertical movement orientation in landscape
/// - updated boundary check to be based on vertical axis
/// - updated movement to be along y axis
/// - added responsiveness to orientation change
/// </summary>
public class EnemyController : MonoBehaviour
{
    public float verticalSpeed;
    public float verticalBoundary;
    public float horizontalSpeed;
    public float horizontalBoundary;
    public float direction;


    //private variables
    private bool m_bOrientationChanged;
    ScreenOrientation m_screenOrientation;

    void Start()
    {
        m_screenOrientation = Screen.orientation;
        m_bOrientationChanged = false;
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            _SetLandscape();
        }
        else
        {
            _SetPortrait();
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
            }
            if (m_bOrientationChanged)
            {
                _SetLandscape();
            }
            _MoveVertical();
            _CheckBoundsVertical();
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
                _SetPortrait();
            }
            _MoveHorizontal();
            _CheckBoundsHorizontal();
        }
        
    }

    /// <summary>
    /// sets the position of the enemy based on the portrait orientation
    /// </summary>
    private void _SetPortrait()
    {
        transform.SetPositionAndRotation(new Vector2(Random.Range(-2.35f, 2.35f), Random.Range(3.0f, 4.5f)), Quaternion.Euler(0.0f, 0.0f, 180.0f));
        m_bOrientationChanged = false;
    }

    /// <summary>
    /// sets the position of the enemy based on the landscape orientation
    /// </summary>
    private void _SetLandscape()
    {
        transform.SetPositionAndRotation(new Vector2(Random.Range(5.8f, 7.75f), Random.Range(-4.0f,4.0f)), Quaternion.Euler(0.0f, 0.0f, 90.0f));
        m_bOrientationChanged = false;
    }

    /// <summary>
    /// move along the y axis based on speed
    /// </summary>
    private void _MoveVertical()
    {
        transform.position += new Vector3( 0.0f, verticalSpeed * direction * Time.deltaTime, 0.0f);
    }


    /// <summary>
    /// move along the x axis based on speed
    /// </summary>
    private void _MoveHorizontal()
    {
        transform.position += new Vector3(horizontalSpeed * direction * Time.deltaTime, 0.0f, 0.0f);
    }

    /// <summary>
    /// check vertical bounds and reverse direction once met
    /// </summary>
    private void _CheckBoundsVertical()
    {
        // check right boundary
        if (transform.position.y >= verticalBoundary)
        {
            direction = -1.0f;
        }

        // check left boundary
        if (transform.position.y <= -verticalBoundary)
        {
            direction = 1.0f;
        }
    }

    /// <summary>
    /// check horizontal bounds and reverse direction once met
    /// </summary>
    private void _CheckBoundsHorizontal()
    {
        // check right boundary
        if (transform.position.x >= horizontalBoundary)
        {
            direction = -1.0f;
        }

        // check left boundary
        if (transform.position.x <= -horizontalBoundary)
        {
            direction = 1.0f;
        }
    }
}
