using System;
using UnityEngine;


[Serializable]
public struct BonusData
{
    public int _amount;
}

public sealed class BonusesSaveLoader : ISaveLoader
{
    public void LoadData()
    {
        BonusData data = Repository.GetData<BonusData>();
        BonusStorage.Instance.SetupBonuses(data._amount);
    }

    public void SaveData()
    {
        int amount = BonusStorage.Instance.GetBonuses();
        var data = new BonusData
        {
            _amount = amount
        };
        Repository.SetData(data);
    }
}
