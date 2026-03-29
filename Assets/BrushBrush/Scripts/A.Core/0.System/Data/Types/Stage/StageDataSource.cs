#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;


public class StageDataSource : ScriptableObject, IDataSource
{

    public string id;
    [SerializeField] GameObject prefabRef;
    public string PrefabPath;  // ← PrefabPath 대신

    //=============================
#if UNITY_EDITOR
    private void OnValidate()
    {
        // 빌드 중/플레이 중에는 실행되지 않도록
        if (Application.isPlaying)
        {
            return;
        }

        //
        if (prefabRef != null)
        {
            string fullPath = AssetDatabase.GetAssetPath(prefabRef);
            const string marker = "Resources/";
            int index = fullPath.IndexOf(marker);
            if (index >= 0)
            {
                PrefabPath = fullPath.Substring(index + marker.Length).Replace(".prefab", "");
            }
            else
            {
                Debug.LogWarning("이 프리팹은 Resources 폴더 안에 있어야 합니다!", this);
                PrefabPath = "";
            }
        }
    }
#endif
}
