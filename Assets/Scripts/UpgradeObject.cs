using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class UpgradeObject : ScriptableObject
{

    public string upgradeName = "(Upgrade Name)";
    [TextArea] public string upgradeDescription = "(Upgrade description...)";
    public Upgrades upgrade;

}
