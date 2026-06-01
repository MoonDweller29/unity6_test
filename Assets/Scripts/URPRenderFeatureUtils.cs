using System.Reflection;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public static class URPRenderFeatureUtils
{
    public static bool TryGetRenderFeature<T>(out T feature)
        where T : ScriptableRendererFeature
    {
        feature = null;

        var urpAsset = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
        if (urpAsset == null)
            return false;

        var rendererData = GetDefaultRendererData(urpAsset);
        if (rendererData == null)
            return false;

        foreach (var f in rendererData.rendererFeatures)
        {
            if (f is T typedFeature)
            {
                feature = typedFeature;
                return true;
            }
        }

        return false;
    }

    private static ScriptableRendererData GetDefaultRendererData(UniversalRenderPipelineAsset urpAsset)
    {
        // Unity/URP versions expose renderer data slightly differently.
        // This reflection fallback is useful across URP versions.
        var property = typeof(UniversalRenderPipelineAsset).GetProperty(
            "scriptableRendererData",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        if (property != null)
            return property.GetValue(urpAsset) as ScriptableRendererData;

        var field = typeof(UniversalRenderPipelineAsset).GetField(
            "m_RendererDataList",
            BindingFlags.Instance | BindingFlags.NonPublic);

        if (field?.GetValue(urpAsset) is ScriptableRendererData[] rendererDataList)
        {
            foreach (var data in rendererDataList)
            {
                if (data != null)
                    return data;
            }
        }

        return null;
    }
}