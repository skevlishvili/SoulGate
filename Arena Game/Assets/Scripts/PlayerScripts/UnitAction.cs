using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitAction
{
    void Idle();
    void Move();
    void Walk();
    void Run();
    void Jump();
    void Attack(KeyCode key);
}
