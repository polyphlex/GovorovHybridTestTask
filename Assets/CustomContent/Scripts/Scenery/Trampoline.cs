using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Объект контроллер трамплина
/// </summary>
public class Trampoline : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.instance.AddTrampoline();
        }
    }
}
