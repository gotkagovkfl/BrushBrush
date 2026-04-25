using Cysharp.Threading.Tasks;
using UnityEngine;

public class StageManager
{
    StageData _data;
    Stage _stageObject;   // 생성된


    //====================================
    public StageManager(StageData data)
    {
        _data = data;
    }

    /// <summary>
    /// 스테이지를 생성하고 배치한다.
    /// </summary>
    public async UniTask GenerateStageAsync()
    {
        // 리소스 불러와 배치 - 추후 에셋번들 or 어드레서블
        Stage stageRef = await Resources.LoadAsync<Stage>(_data.PrefabPath).ToUniTask() as Stage;
        if (stageRef == null)
        {
            Debug.LogError($"Stage prefab을 찾을 수 없습니다: {_data.PrefabPath}");
            return;
        }
        _stageObject = GameObject.Instantiate(stageRef, Vector3.zero, Quaternion.identity);

        // 기타 스테이지 초기화 작업
    }
}
