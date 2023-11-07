using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GreasableObject : MonoBehaviour
{
/*    private RenderTexture support;
    private RenderTexture mask;

    [SerializeField, Tooltip("Used to blit newly drawn ink on top of the existing inking.")]
    protected Material alphaCombiner;

    [SerializeField, Tooltip("If left blank a blank render texture of size textureSize will be automatically generated.")]
    protected Texture sourceMap;
    [SerializeField]
    private int textureSize = 1024;

    [Header("Grease Splat Appearence")]
    [SerializeField] float radius = 1.49f;
    [SerializeField] float hardness = 0;
    [SerializeField] float strength = 0.2f;
    [SerializeField] Color inkColor;


    int prepareUVID = Shader.PropertyToID("_PrepareUV");
    int positionID = Shader.PropertyToID("_PainterPosition");
    int hardnessID = Shader.PropertyToID("_Hardness");
    int strengthID = Shader.PropertyToID("_Strength");
    int radiusID = Shader.PropertyToID("_Radius");
    int blendOpID = Shader.PropertyToID("_BlendOp");
    int colorID = Shader.PropertyToID("_PainterColor");
    int textureID = Shader.PropertyToID("_MainTex");
    int uvOffsetID = Shader.PropertyToID("_OffsetUV");
    int uvIslandsID = Shader.PropertyToID("_UVIslands");

    public RenderTexture Splatmap
    {
        get { return support; }
    }

    private Material splatMaterial;
    private Material thisMaterial;
    private CommandBuffer cmd;

    void Start()
    {
        if(sourceMap)
        {
            print(sourceMap.graphicsFormat);
            support = new RenderTexture(sourceMap.width, sourceMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(sourceMap, support, alphaCombiner);
        }
        else
        {
            support = new RenderTexture(textureSize, textureSize, 0, RenderTextureFormat.ARGBFloat);
        }
        mask = new RenderTexture(support.width, support.height, 0, RenderTextureFormat.ARGBFloat);

        splatMaterial = new Material(Shader.Find("Unlit/SplatMask"));       
        thisMaterial = GetComponent<Renderer>().material;
        thisMaterial.SetTexture("_Splatmap", support);

         cmd = new CommandBuffer();
    }

    public void DrawGreaseSplat(Vector3 worldPos)
    {
        splatMaterial.SetFloat(Shader.PropertyToID("_Radius"), radius);
        splatMaterial.SetFloat(Shader.PropertyToID("_Hardness"), hardness);
        splatMaterial.SetFloat(Shader.PropertyToID("_Strength"), strength);
        splatMaterial.SetVector(Shader.PropertyToID("_SplatPos"), worldPos);
        splatMaterial.SetVector(Shader.PropertyToID("_InkColor"), inkColor);

        cmd.SetRenderTarget(mask);
        cmd.DrawRenderer(GetComponent<Renderer>(), splatMaterial, 0);

        cmd.SetRenderTarget(support);
        cmd.Blit(mask, support); // paramater alphacombiner
        
        Graphics.ExecuteCommandBuffer(cmd);
        cmd.Clear();
    }*/
}
