using UnityEditor;
using UnityEngine;
using System.IO;

/// <summary>
/// 에셋 생성 - SO 생성 부분
/// </summary>
public static partial class BBAssetCreator
{
    /// <summary>
    /// SO 생성 기본 경로
    /// </summary>
    const string MENU_NAME_CREATE_SO = MENU_NAME_CREATE_ASSET + "SO/";
    const string PATH_CREATE_SO_TARGET = BBConstants.PATH_ASSETS + "SO/";
    //==============================================================================================
    /// <summary>
    /// SO 생성
    /// </summary>
    private static void CreateSO<T>(string leafPath = "") where T : ScriptableObject
    {
        // 1. 타겟 디렉토리 설정 : SO 생성 기본 경로 + 지정한 마지막 경로
        string targetPath = PATH_CREATE_SO_TARGET + leafPath;
        
        // 2. 근데 뭔가 설정한 디렉토리가 이상하면 기본값
        if (string.IsNullOrEmpty(targetPath))
        {
            targetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrEmpty(targetPath))
            {
                targetPath = "Assets";
            }
        }

        // 3. 디렉토리 보장
        if (AssetDatabase.IsValidFolder(targetPath) == false)
        {
            Debug.LogWarning($" [ BB Asset Creator ] 유효하지 않은 경로 : {targetPath}");
            // 중첩 경로를 단계별로 생성
            var parts = targetPath.Split('/');
            var current = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                var next = current + "/" + parts[i];
                if (AssetDatabase.IsValidFolder(next) == false)
                    AssetDatabase.CreateFolder(current, parts[i]);
                current = next;
            }
        }

        // 4. SO 생성
        var fileName = typeof(T).Name + ".asset";
        var fullPath = AssetDatabase.GenerateUniqueAssetPath($"{targetPath}/{fileName}");
        Debug.Log($" [ BB Asset Creator ] SO 생성 : {fullPath}");

        var asset = ScriptableObject.CreateInstance<T>();
        AssetDatabase.CreateAsset(asset, fullPath);
        AssetDatabase.SaveAssets();

        // 5. 마무리 Project 창 포커스 + 생성된 에셋 선택
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }


    //==============================================================================================
    #region  [ SO 생성 메소드 ]
    //==============================================================================================

    ///<summary> 스테이지 데이터 SO 생성 </summary>
    [MenuItem(MENU_NAME_CREATE_SO + "StageDataSource", false, 1)]
    private static void CreateStageData() => CreateSO<StageDataSource>("StageData/");


    //==============================================================================================
    #endregion
}