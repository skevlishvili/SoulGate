using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitAction
{
    void Idle();
    void Move();
    void Attack(KeyCode key);
}
