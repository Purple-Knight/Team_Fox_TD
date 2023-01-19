using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float coolDownBtwShot;
    private float actualCoolDownBtwShot;
    [SerializeField] private float attackPower;
    [SerializeField] private List<GameObject> enemisList;
    
    
}
