using UnityEngine;

public class ManualRectController : MonoBehaviour
{
    public int x = 1;
    public int y = 1;
    public int z = 1;
    public int w = 1;

    private void OnValidate()
    {
        if (URPRenderFeatureUtils.TryGetRenderFeature(out RectDrawerFeature feature))
        {
            feature.SetPosSize(new Vector4(x, y, z, w));
        }
    }
}
