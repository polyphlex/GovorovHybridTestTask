using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BaseBlock : MonoBehaviour
{

    private BoxCollider2D localCollider;

    // Start is called before the first frame update
    void Start()
    {
        localCollider = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Общая обработка события столкновения
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void CollisionHappened(Collision2D collision)
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionHappened(collision);
    }
}
