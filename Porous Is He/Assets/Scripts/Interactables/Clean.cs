using System;
using UnityEngine;

public class Clean : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private Texture2D _dirtMaskBase;
    [SerializeField] private Texture2D _brush;

    [SerializeField] private Material _material;

    private Texture2D _templateDirtMask;

    private void Start()
    {
        CreateTexture();
    }

    private void CreateTexture()
    {
        _templateDirtMask = new Texture2D(_dirtMaskBase.width, _dirtMaskBase.height);
        _templateDirtMask.SetPixels(_dirtMaskBase.GetPixels());
        _templateDirtMask.Apply();

        _material.SetTexture("_DirtMask", _templateDirtMask);
    }

    public void CleanArea(Vector2 textureCoord)
    {
        //Debug.Log(textureCoord);

        int pixelX = (int)(textureCoord.x * _templateDirtMask.width);
        int pixelY = (int)(textureCoord.y * _templateDirtMask.height);

        for (int x = -_brush.width/2; x < _brush.width/2; x++)
        {
            for (int y = -_brush.height/2; y < _brush.height/2; y++)
            {
                Color pixelDirt = _brush.GetPixel(x + _brush.width / 2, y + _brush.height / 2);
                Color pixelDirtMask = _templateDirtMask.GetPixel(pixelX + x, pixelY + y);

                _templateDirtMask.SetPixel(pixelX + x,
                    pixelY + y,
                    new Color(0, pixelDirtMask.g * pixelDirt.g, 0));
            }
        }

        _templateDirtMask.Apply();
    }
}