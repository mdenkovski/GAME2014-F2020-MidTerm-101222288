using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BackgroundController.cs
/// Michael Denkovski 101222288
/// GAME 2014 Mobile Game Dev
/// Last Modified Oct 20
/// - changed variable names to reflect horizontal movement orientation in landscape
/// -chanegd functions to move along x axis instead of y
/// -check horizontal position
/// -update orientation and position based on layout
/// </summary>
public class BackgroundController : MonoBehaviour
{
    public float horizontalSpeed;
    public float horizontalBoundary;
    public float verticalSpeed;
    public float verticalBoundary;
    public int id;


    //private variables
    private bool m_bOrientationChanged;
    ScreenOrientation m_screenOrientation;

    void Start()
    {
        m_screenOrientation = Screen.orientation;
        m_bOrientationChanged = false;
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            _SetHorizontal();
        }
        else
        {
            _SetVertical();
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
                _SetHorizontal();
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
                _SetVertical();
            }
            _MoveVertical();
            _CheckBoundsVertical();
        }
    }

    /// <summary>
    /// reset the position based on the horizontal boundary
    /// </summary>
    private void _ResetHorizontal()
    {
        transform.position = new Vector3(horizontalBoundary, 0.0f);
    }

    /// <summary>
    /// reset the position based on the horizontal boundary
    /// </summary>
    private void _ResetVertical()
    {
        transform.position = new Vector3(0.0f, verticalBoundary);
    }

    /// <summary>
    /// move the background to the left(-x axis) based on its horizontal speed
    /// </summary>
    private void _MoveHorizontal()
    {
        transform.position -= new Vector3(horizontalSpeed, 0.0f) * Time.deltaTime;
        //Debug.Log("moving left");
    }

    /// <summary>
    /// move the background to the bottom(-y axis) based on its vertical speed
    /// </summary>
    private void _MoveVertical()
    {
        transform.position -= new Vector3(0.0f, verticalSpeed) * Time.deltaTime;
        //Debug.Log("moving down");
    }

    /// <summary>
    /// check the horizontal bounds if the background is past
    /// </summary>
    private void _CheckBoundsHorizontal()
    {
        // if the background is lower than the bottom of the screen then reset
        if (transform.position.x <= -horizontalBoundary)
        {
            _ResetHorizontal();
        }
    }

    /// <summary>
    /// check the vertical bounds if the background is past
    /// </summary>
    private void _CheckBoundsVertical()
    {
        // if the background is lower than the bottom of the screen then reset
        if (transform.position.y <= -verticalBoundary)
        {
            _ResetVertical();
        }
    }

    private void _SetVertical()
    {
        if(id == 0)
        {
            transform.SetPositionAndRotation(new Vector2(0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, -90.0f));
            transform.localScale = new Vector3(3.687814f, 3.5f, 1.0f);
        }
        else
        {
            transform.SetPositionAndRotation(new Vector2(0.0f, 10.0f), Quaternion.Euler(0.0f, 0.0f, -90.0f));
            transform.localScale = new Vector3(3.687814f, 3.5f, 1.0f);
        }
        m_bOrientationChanged = false;

    }

    private void _SetHorizontal()
    {
        if (id == 0)
        {
            transform.SetPositionAndRotation(new Vector2(0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            transform.localScale = new Vector3(7.777619f, 6.2569685f, 1.0f);
        }
        else
        {
            transform.SetPositionAndRotation(new Vector2(21.17f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            transform.localScale = new Vector3(7.777619f, 6.2569685f, 1.0f);
        }
        m_bOrientationChanged = false;

    }
}
