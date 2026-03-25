using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StageDataManager : DataManager<StageData, StageDataSource>
{
    public override DataEnumType DataEnumType => DataEnumType.STAGE;

    /// <summary>
    /// 일단 SO 사용
    /// </summary>
    protected override async UniTask<StageDataSource[]> GetSource()
    {
        string path = "DataSource/Stage";
        StageDataSource[] ret = Resources.LoadAll<StageDataSource>(path);
        
        return ret;
    }
}
