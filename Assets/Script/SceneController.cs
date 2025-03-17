using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneType
{
    Lobby = 0,
    InGame = 2
}
public class SceneController : NetworkSceneManagerDefault
{
    public static SceneController Instance;
    private void Awake()
    {
        // 인스턴스가 이미 존재하면 현재 오브젝트를 파괴
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            // 현재 인스턴스를 설정하고 파괴되지 않도록 설정
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    protected override IEnumerator LoadSceneCoroutine(SceneRef sceneRef, NetworkLoadSceneParameters sceneParams)
    {
        yield return StartCoroutine(ScreenFader.FadeOut(PageType.Loading));

        yield return base.LoadSceneCoroutine(sceneRef, sceneParams);

        // Delay one frame, so we're sure level objects has spawned locally
        yield return null;

        yield return StartCoroutine(ScreenFader.FadeIn(PageType.Loading));
    }

    public static void SceneTransition(SceneType scene)
    {
        if (Instance.Runner != null)
{
    Instance.Runner.LoadScene(SceneRef.FromIndex((int)scene));
}
else
{
    Debug.LogWarning("System : Scene Null - Runner is not initialized.");
}

    }


}