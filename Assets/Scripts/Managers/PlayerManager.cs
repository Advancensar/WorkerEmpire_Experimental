using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; } = new PlayerManager();

    public int Energy;
    public int Gold;

    private string PATH = @"/Database/PlayerData.json";

    public struct IntVector2
    {
        public int energy;
        public int gold;

    }
    public void SavePlayerData()
    {
        IntVector2 playerdata = new IntVector2() {energy = Energy, gold = Gold};
        
        FileTool.SaveFileAsJson(PATH, playerdata);
    }

    public void LoadPlayerData()
    {
        var playerdata = FileTool.LoadObjectFromJson<IntVector2>(PATH);
        Energy = playerdata.energy;
        Gold = playerdata.gold;
    }

}
