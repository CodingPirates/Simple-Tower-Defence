using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterController : MonoBehaviour
{
    [SerializeField] BFSearch bfSearch;
    [SerializeField] Tilemap ground;
    [SerializeField] float speed = 5;
    [SerializeField] float hitPoints = 5;
    [SerializeField] RectTransform healthBar;

    float maxHitPoints;
    private Vector3? target;

    private void Start()
    {
        maxHitPoints = hitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            var cel = ground.WorldToCell(transform.position);
            var next = bfSearch.GetNext(cel);
            if (next.HasValue)
                target = ground.CellToWorld(next.Value) + ground.cellSize / 2;
        }
        else
        {
            var v = target.Value - transform.position;
            var direction = v.normalized;
            GetComponent<SpriteRenderer>().flipX = direction.x < 0;

            if (Vector3.Distance(transform.position, target.Value) < Time.deltaTime * speed)
            {
                transform.position = target.Value;
                target = null;
            }
            else
                transform.position += direction * Time.deltaTime * speed;
        }
    }

    public void Hit(float dmg)
    {
        hitPoints -= dmg;
        healthBar.offsetMax = new Vector2(-0.5f * (1-hitPoints/maxHitPoints), healthBar.offsetMax.y);
        if (hitPoints <= 0)
            Destroy(gameObject);
    }
}
