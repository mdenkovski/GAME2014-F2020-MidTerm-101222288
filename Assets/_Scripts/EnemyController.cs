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
/// </summary>
public class EnemyController : MonoBehaviour
{
    public float verticalSpeed;
    public float verticalBoundary;
    public float direction;

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }
    /// <summary>
    /// move along the y axis based on speed
    /// </summary>
    private void _Move()
    {
        transform.position += new Vector3( 0.0f, verticalSpeed * direction * Time.deltaTime, 0.0f);
    }

    /// <summary>
    /// check vertical bounds and reverse direction once met
    /// </summary>
    private void _CheckBounds()
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
}
