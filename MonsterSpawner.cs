using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Tilemap ground;
    [SerializeField] GameObject spawn;
    [SerializeField] GameObject monster;

    // Start is called before the first frame update
    void Start()
    {
        monster.transform.position = spawn.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            var cel = ground.WorldToCell(spawn.transform.position);
            var clone = Instantiate(monster);
            clone.transform.position = ground.CellToWorld(cel) + ground.cellSize / 2;
            clone.SetActive(true);
        }
    }
}
