#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Utils
{
    
    [InitializeOnLoad]
    public class AutoSave
    {
        // Static constructor that gets called when unity fires up.
        static AutoSave()
        {
            EditorApplication.playModeStateChanged += EditorApplicationOnplayModeStateChanged;
        }

        private static void EditorApplicationOnplayModeStateChanged(PlayModeStateChange obj)
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isPlaying)
            {
                return;
            }

            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
            AssetDatabase.SaveAssets();
        }
    }
}
#endif