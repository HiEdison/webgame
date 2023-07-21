using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadGms : MonoBehaviour
{
    public string sceneName = "819";
    public string ip = "cdvps2.woogiworld.cn";
    IEnumerator Start()
    {
        // UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(
        //     $"https://{ip}/client/assets_view/1/StreamingAssets/AssetBundle/WebGL/aaresourcesextends/scenebake/{sceneName}.webgl");
//https://cdvps2.woogiworld.cn/client/assets_view/1/StreamingAssets/AssetBundle/WebGL/aaresourcesextends/scenebake/819.webgl
        UnityWebRequest request =
            UnityWebRequestAssetBundle.GetAssetBundle($"{Application.streamingAssetsPath}/{sceneName}.webgl");
        yield return request.SendWebRequest();

        var ab = DownloadHandlerAssetBundle.GetContent(request);
        var wait = SceneManager.LoadSceneAsync("819", LoadSceneMode.Additive);
        while (!wait.isDone)
        {
            yield return 0;
        }

        var scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
        var golist = scene.GetRootGameObjects();
#if UNITY_EDITOR
        
        foreach (var item in golist)
        {
            var render = item.GetComponentInChildren<Renderer>();
            if (render != null)
            {
                foreach (var mat in render.sharedMaterials)
                {
                    if (mat != null)
                    {
                        mat.shader = Shader.Find(mat.shader.name);
                    }
                }
            }
        }
        RenderSettings.skybox.shader = Shader.Find(RenderSettings.skybox.shader.name);
        #else
#endif
        
        SendMessage("AllReady",golist);
    }
}