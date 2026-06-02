using UnityEngine;

public class LinearRectController : MonoBehaviour
{
    public Vector2Int x = Vector2Int.zero;
    public Vector2Int y = Vector2Int.zero;
    public Vector2Int z = Vector2Int.one;
    public Vector2Int w = Vector2Int.one;

    public int frameSkipCount = 1;

    private int _frameIdx = 0;

    private int _x = 0;
    private int _y = 0;
    private int _z = 0;
    private int _w = 0;

    private int _xSign = 1;
    private int _ySign = 1;
    private int _zSign = 1;
    private int _wSign = 1;

    private int ValidateRange(int value, Vector2Int range)
    {
        return Mathf.Clamp(value, range.x, range.y);
    }

    private void UpdateValue(ref int value, Vector2Int range, ref int sign)
    {
        value = ValidateRange(value + sign, range);
        if (value == range.x)
        {
            sign = 1;
        }
        if (value == range.y)
        {
            sign = -1;
        }
    }

    private void Update()
    {
        if (_frameIdx < frameSkipCount)
        {
            _frameIdx++;
            return;
        }

        _frameIdx = 0;

        UpdateValue(ref _x, x, ref _xSign);
        UpdateValue(ref _y, y, ref _ySign);
        UpdateValue(ref _z, z, ref _zSign);
        UpdateValue(ref _w, w, ref _wSign);

        if (URPRenderFeatureUtils.TryGetRenderFeature(out RectDrawerFeature feature))
        {
            feature.SetPosSize(new Vector4(_x, _y, _z, _w));
        }
    }
}
