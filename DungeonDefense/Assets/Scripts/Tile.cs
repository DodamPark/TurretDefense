using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public Color tileColor;

    public Color warningColor;

    public Vector3 offsetPos;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public BluePrint turretBluePrint;
    [HideInInspector]
    public bool isUpgrade = false;

    private Renderer tileRend;

    private Color originColor;

    TurretManager turretManager;

    void Start()
    {
        tileRend = GetComponent<Renderer>();
        originColor = tileRend.material.color;
        turretManager = TurretManager.instance;
    }

    public Vector3 BuildPos()
    {
        return transform.position + offsetPos;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(turret != null)
        {
            turretManager.SelectTheTile(this);
            return;
        }

        if (!turretManager.isBuild)
            return;

        BuildTurret(turretManager.GetTurret());
    }

    void BuildTurret(BluePrint bluePrint)
    {
        if (Player.goldAmount < bluePrint.cost)
        {
            Debug.Log("돈이 부족합니다!");
            return;
        }
        else
        {
            Player.goldAmount -= bluePrint.cost;
            GameObject t = (GameObject)Instantiate(bluePrint.prefab, BuildPos(), Quaternion.identity);
            turret = t;

            GameObject buildEff = (GameObject)Instantiate(turretManager.buildEffect, BuildPos(), Quaternion.identity);
            Destroy(buildEff, 5f);

            Debug.Log("포탑을 구입했습니다.");
        }
    }

    public void UpgradeTurret()
    {
        if (Player.goldAmount < turretBluePrint.upgradeCost)
        {
            Debug.Log("돈이 부족합니다!");
            return;
        }
        else
        {
            Player.goldAmount -= turretBluePrint.upgradeCost;

            Destroy(turret);

            GameObject t = (GameObject)Instantiate(turretBluePrint.upgradePrefab, BuildPos(), Quaternion.identity);
            turret = t;

            GameObject buildEff = (GameObject)Instantiate(turretManager.upgradeEffect, BuildPos(), Quaternion.identity);
            Destroy(buildEff, 5f);

            isUpgrade = true;

            Debug.Log("포탑을 구입했습니다.");
        }
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!turretManager.isBuild)
            return;

        if (turretManager.isMoney)
        {
            tileRend.material.color = tileColor;
        }
        else
        {
            tileRend.material.color = warningColor;
        }
    }

    private void OnMouseExit()
    {
        tileRend.material.color = originColor;
    }
}
