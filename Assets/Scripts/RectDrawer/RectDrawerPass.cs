using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

sealed class RectDrawerPass : ScriptableRenderPass
{
    sealed class PassData
    {
        public Material material;
    }

    private readonly Material _material;
    private static readonly Vector4 DefaultScaleBias = new Vector4(1f, 1f, 0f, 0f);

    public RectDrawerPass(Material material)
    {
        _material = material;
    }

    public override void RecordRenderGraph(
        RenderGraph renderGraph,
        ContextContainer frameData)
    {
        var resourceData = frameData.Get<UniversalResourceData>();

        using var builder = renderGraph.AddRasterRenderPass<PassData>(
            nameof(RectDrawerPass),
            out var passData
        );

        passData.material = _material;

        builder.SetRenderAttachment(
            resourceData.activeColorTexture,
            0
        );

        builder.AllowPassCulling(false);

        builder.SetRenderFunc(static (PassData data, RasterGraphContext context) =>
        {
            Blitter.BlitTexture(context.cmd, DefaultScaleBias, data.material, 0);
        });
    }
}