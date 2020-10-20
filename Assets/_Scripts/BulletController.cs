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
/// </summary>
public class BulletController : MonoBehaviour, IApplyDamage
{
    public float horizontalSpeed;
    public float horizontalBoundary;
    public BulletManager bulletManager;
    public int damage;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    /// <summary>
    /// move the bullet along the x axis based on its horizontal speed
    /// </summary>
    private void _Move()
    {
        transform.position += new Vector3(horizontalSpeed, 0.0f, 0.0f) * Time.deltaTime;
    }

    /// <summary>
    /// check the bullet if it is past the horixontal boundary
    /// </summary>
    private void _CheckBounds()
    {
        if (transform.position.x > horizontalBoundary)
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
