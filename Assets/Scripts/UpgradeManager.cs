using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Upgrades
{
    Heal,
    MoreMaxHealth,
    BetterFlying,
    MoreThrust,
    FireFaster
}

public class UpgradeManager : MonoBehaviour
{

    [SerializeField] private UpgradeObject[] possibleUpgrades;

    public void GetRandomUpgrade(out UpgradeObject upgradeObject) {
        upgradeObject = possibleUpgrades[Random.Range(0, possibleUpgrades.Length)];
    }

    public void ActivateUpgrade(UpgradeObject upgradeObject) {
        switch (upgradeObject.upgrade)
        {
            case Upgrades.Heal: { PlayerHealth.Instance.Heal(); break; }
        }
    }

}
