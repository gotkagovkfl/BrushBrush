
/// <summary>
/// 스테이지 데이터 - T: 초기화 받는 데이터 파일
/// </summary>
public class StageData : BaseData<StageDataSource>
{
    public string PrefabPath;

    public override void Init(StageDataSource source)
    {
        //
        this.Id = source.id;

        //
        this.PrefabPath = source.PrefabPath;
    }
}
