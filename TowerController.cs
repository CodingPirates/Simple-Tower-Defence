using System.Net.WebSockets;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] int range = 1;
    [SerializeField] float fireRate = 1.0f;
    [SerializeField] float fireSpeed = 20.0f;
    [SerializeField] BulletController bullet;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var monsters = FindObjectsOfType<MonsterController>();
        var closestRange = float.MaxValue;
        MonsterController closest = null;
        foreach(var monster in monsters)
        {
            var pos = monster.transform.position;
            var dist = Vector3.Distance(transform.position, pos);
            if (dist < closestRange)
            {
                closest = monster; 
                closestRange = dist;
            }
        }
        if (closestRange > range)
        {
            timer = 0;
            return;
        }
        
        timer += Time.deltaTime;
        var v  = closest.transform.position - transform.position;
        var rotZ = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
        
        if (fireRate > 0 && timer > fireRate)
        {
            timer = 0;
            var bul = Instantiate(bullet);
            bul.Fire(transform.position, closest, fireSpeed);
        }
    }
}
