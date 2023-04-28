using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform[] walkPoints;

    public Transform[] WalkPoints => walkPoints;
}
