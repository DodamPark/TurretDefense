using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTileUI : MonoBehaviour
{
    public GameObject ui;

    private Tile target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget(Tile tile)
    {
        target = tile;

        transform.position = target.BuildPos();

        ui.SetActive(true);
    }

    public void HideUI()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        TurretManager.instance.DeselectTile();
    }
}
