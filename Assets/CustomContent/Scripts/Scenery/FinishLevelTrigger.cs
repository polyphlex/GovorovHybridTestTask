using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Объект триггер завершения уровня
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class FinishLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameUI.instance.FinishLevel();
        PlayerController.instance.DisablePlayerInput();
    }
}
