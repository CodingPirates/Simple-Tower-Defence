using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Tilemap ground;
    [SerializeField] Sprite placeable;
    [SerializeField] GameObject turretTemplate;
    [SerializeField] Camera cam;

    List<Vector3Int> towers = new List<Vector3Int>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var pos = cam.ScreenToWorldPoint(Input.mousePosition);
            var cell = ground.WorldToCell(pos);
            if (ground.GetSprite(cell) == placeable && !towers.Contains(cell))
            {
                var turret = Instantiate(turretTemplate);
                turret.transform.position = ground.CellToWorld(cell) + new Vector3(0.5f, 0.5f, 0); ;
                turret.SetActive(true);
                towers.Add(cell);
            }
        }
    }
}
