using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float splashRange = 0.5f;
    public float splashDamage = 1;
    float speed = 1.0f;
    float maxRange;
    Vector3 direction;
    Vector2 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, originalPosition) > maxRange)
        {
            foreach(var monster in FindObjectsOfType<MonsterController>())
            {
                var dist = Vector3.Distance(monster.transform.position, transform.position);
                if (dist < splashRange)
                    monster.Hit((splashRange-dist)/splashRange * splashDamage);
            }
            Destroy(gameObject);
        }
    }
    public void Fire(Vector3 position, MonsterController target, float speed)
    {
        this.speed = speed;
        var v = target.transform.position - position;
        maxRange = v.magnitude;
        direction = v / maxRange;

        originalPosition = transform.position = position;
        gameObject.SetActive(true);
    }
}
