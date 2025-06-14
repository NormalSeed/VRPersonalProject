using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterModel : MonoBehaviour
{
    [field: SerializeField] public ObservableProperty<bool> IsMoving { get; private set; } = new();
    [field: SerializeField] public ObservableProperty<bool> IsAttacking { get; private set; } = new();
}
