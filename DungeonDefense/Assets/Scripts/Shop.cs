using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    TurretManager turretManager;

    public BluePrint gunTurret;
    public BluePrint missileTurret;
    public BluePrint lazerTurret;

    // Start is called before the first frame update
    void Start()
    {
        turretManager = TurretManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectGunTurret()
    {
        turretManager.SelectTurret(gunTurret);
    }

    public void SelectMissileTurret()
    {
        turretManager.SelectTurret(missileTurret);
    }

    public void SelectLazerTurret()
    {
        turretManager.SelectTurret(lazerTurret);
    }
}
