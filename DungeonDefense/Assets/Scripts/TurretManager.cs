using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public static TurretManager instance;

    public GameObject buildEffect;
    public GameObject upgradeEffect;

    private BluePrint turretToBuild;
    private Tile selectedTile;
    public SelectTileUI tileUI;

    public bool isBuild { get { return turretToBuild != null; } }
    public bool isMoney { get { return Player.goldAmount >= turretToBuild.cost; } }

    void Awake()
    {
        if(instance != null)
            return;
        else
            instance = this;
    }

    public void SelectTheTile(Tile tile)
    {
        if(selectedTile == tile)
        {
            DeselectTile();
            return;
        }

        selectedTile = tile;
        turretToBuild = null;

        tileUI.SetTarget(tile);
    }

    public void DeselectTile()
    {
        selectedTile = null;
        tileUI.HideUI();
    }

    public void SelectTurret(BluePrint turret)
    {
        turretToBuild = turret;
        
        DeselectTile();
    }

    public BluePrint GetTurret()
    {
        return turretToBuild;
    }
}
