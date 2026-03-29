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
        // 추후 addressable 로 바꿀거임. 그래서 그전까지는 so생성해서 resource 폴더로 이동시킴
        string path = "DataSource/Stage";
        StageDataSource[] ret = Resources.LoadAll<StageDataSource>(path);
        
        return ret;
    }
}
