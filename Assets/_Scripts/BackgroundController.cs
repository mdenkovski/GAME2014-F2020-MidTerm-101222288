using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BackgroundController.cs
/// Michael Denkovski 101222288
/// GAME 2014 Mobile Game Dev
/// Last Modified Oct 20
/// - changed variable names to reflect horizontal movement orientation in landscape
/// </summary>
public class BackgroundController : MonoBehaviour
{
    public float horizontalSpeed;
    public float horizontalBoundary;

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    /// <summary>
    /// reset the position based on the horizontal boundary
    /// </summary>
    private void _Reset()
    {
        transform.position = new Vector3(horizontalBoundary, 0.0f);
    }

    /// <summary>
    /// move the background to the left(-x axis) based on its horizontal speed
    /// </summary>
    private void _Move()
    {
        transform.position -= new Vector3(horizontalSpeed, 0.0f) * Time.deltaTime;
    }

    /// <summary>
    /// check the horizontal bounds if the background is past
    /// </summary>
    private void _CheckBounds()
    {
        // if the background is lower than the bottom of the screen then reset
        if (transform.position.x <= -horizontalBoundary)
        {
            _Reset();
        }
    }
}
