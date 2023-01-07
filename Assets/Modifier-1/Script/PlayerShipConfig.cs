using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatSystem/PlayerShipConfig", fileName = "PlayerShipConfig", order = 0)]
public class PlayerShipConfig : ScriptableObject
{
    public List<ModuleMod> moduleModList;

}
