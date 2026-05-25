using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RectDrawerFeature : ScriptableRendererFeature
{
    [System.Serializable, ReloadGroup]
    sealed class Shaders
    {
        [Reload("Shaders/RectDrawer.shader")]
        public Shader fullscreenTriangle;
    }

    [SerializeField] private Shaders shaders;
    [SerializeField] private RenderPassEvent passEvent = RenderPassEvent.AfterRenderingTransparents;
    private Material _material;

    private RectDrawerPass _pass;

    public override void Create()
    {
        ResourceReloader.ReloadAllNullIn(this, "Assets/");

        if (shaders?.fullscreenTriangle is not null)
        {
            _material = CoreUtils.CreateEngineMaterial(shaders.fullscreenTriangle);
        }

        _pass = new RectDrawerPass(_material)
        {
            renderPassEvent = passEvent
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (_material == null)
            return;

        renderer.EnqueuePass(_pass);
    }


}