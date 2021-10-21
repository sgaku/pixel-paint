using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class origami : MonoBehaviour
{
    private const float FoldHalfWidth = 0.05f;
    private static readonly int[] BackIndices = { 0, 1, 4, 5, 4, 1 };
    private static readonly int[] FrontIndices = { 4, 1, 0, 1, 4, 5 };
    [Range(0.0f, 1.0f)] public float Folding = 0.0f;
    [Range(0.0f, 1.0f)] public float Completeness = 0.98f;
    [Range(0.0f, 0.5f)] public float Overwrap = 0.1f;
    private bool inited;
    private Transform boneZ0;
    private Transform boneX;
    private Transform boneZ1;

    private void Init()
    {
        if (this.inited)
        {
            return;
        }
        this.inited = true;
        var renderer = this.GetComponent<SkinnedMeshRenderer>();
        if (renderer != null)
        {
            return;
        }
        renderer = this.gameObject.AddComponent<SkinnedMeshRenderer>();
        var mesh = new Mesh();

        // 頂点を作成
        var vertices = new Vector3[32];
        var normals = new Vector3[32];
        var tangents = new Vector4[32];
        var uv = new Vector2[32];
        var boneWeights = new BoneWeight[32];
        var colors = new Color[32];
        // 前面の頂点
        var count = 0;
        for (var z0 = -1; z0 < 1; z0++)
        {
            for (var z1 = 0; z1 < 2; z1++)
            {
                var sz = z1 - z0 - 1;
                for (var x0 = -1; x0 < 1; x0++)
                {
                    var boneIndex = (2 * (z0 + 1)) + (((((-2 * z0) - 1) * ((2 * x0) + 1)) + 1) / 2);
                    for (var x1 = 0; x1 < 2; x1++)
                    {
                        var sx = x1 - x0 - 1;
                        var x = (x0 + x1) - (FoldHalfWidth * sx);
                        var z = (z0 + z1) - (FoldHalfWidth * sz);
                        vertices[count] = new Vector3(x, 0, z);
                        normals[count] = Vector3.up;
                        tangents[count] = new Vector4(1, 0, 0, 1);
                        uv[count] = 0.5f * new Vector2(x + 1, z + 1);
                        boneWeights[count].boneIndex0 = boneIndex;
                        boneWeights[count].weight0 = 1;
                        colors[count] = Color.clear;
                        count++;
                    }
                }
            }
        }

        for (var i = 0; i < count; i++)
        {
            var j = i + count;
            vertices[j] = vertices[i];
            normals[j] = Vector3.down;
            tangents[j] = new Vector4(-1, 0, 0, 1);
            uv[j] = new Vector2(1.0f - uv[i].x, uv[i].y);
            boneWeights[j] = boneWeights[i];
            colors[j] = Color.white;
        }

        var triangles = new List<int>();
        for (var i = 0; i < 3; i++)
        {
            var i4 = i * 4;
            for (var j = 0; j < 3; j++)
            {
                // 前面
                var o = i4 + j;
                for (var k = 0; k < 6; k++)
                {
                    triangles.Add(FrontIndices[k] + o);
                }
                // 背面
                o += 16;
                for (var k = 0; k < 6; k++)
                {
                    triangles.Add(BackIndices[k] + o);
                }
            }
        }

        // ボーンを作成
        var bones = new Transform[4];
        var bindposes = new Matrix4x4[4];
        for (var i = 0; i < 4; i++)
        {
            var bone = new GameObject("Bone" + i).transform;
            bone.SetParent(i > 0 ? bones[i - 1] : this.transform, false);
            bones[i] = bone;
        }
        bones[3].localPosition = Vector3.forward;
        for (var i = 0; i < 4; i++)
        {
            bindposes[i] = bones[i].worldToLocalMatrix * bones[0].localToWorldMatrix;
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.tangents = tangents;
        mesh.uv = uv;
        mesh.boneWeights = boneWeights;
        mesh.triangles = triangles.ToArray();
        mesh.bindposes = bindposes;
        mesh.colors = colors;
        mesh.RecalculateBounds();

        var shader = Shader.Find("Custom/Origami");
        if (shader == null)
        {
            var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            renderer.sharedMaterial = quad.GetComponent<MeshRenderer>().sharedMaterial;
            DestroyImmediate(quad);
        }
        else
        {
            renderer.sharedMaterial = new Material(shader);
        }
        renderer.rootBone = bones[0];
        renderer.bones = bones;
        renderer.sharedMesh = mesh;
    }

    private void OnEnable()
    {
        this.Init();
    }

    private void Start()
    {
        var path = "Bone0/Bone1";
        this.boneZ0 = transform.Find(path);
        path += "/Bone2";
        this.boneX = transform.Find(path);
        path += "/Bone3";
        this.boneZ1 = transform.Find(path);
    }
    private void Update()
    {
        // Folding     ...四つ折りの進行度。0で広げた状態、1で四つ折り状態。
        // Completeness...どれだけしっかり折るか。1で完全に折り畳まれますが、ポリゴンが重なって
        //                Zファイティング現象が起こるので、1より少しだけ小さくするといいでしょう。
        // Overwrap    ...「広げた状態→二つ折り」と「二つ折り→四つ折り」の動きの重なり。
        //                0.5にすると二つの動きが同時に起こりますが、大きなひずみが生じて
        //                ポリゴンが引きのばされてしまいます。
        var foldingZ = Mathf.SmoothStep(0.0f, 1.0f, Mathf.InverseLerp(0.0f, 0.5f + this.Overwrap, this.Folding));
        var foldingX = Mathf.SmoothStep(0.0f, 1.0f, Mathf.InverseLerp(0.5f - this.Overwrap, 1.0f, this.Folding));
        var angleZ = foldingZ * this.Completeness * 180.0f;
        var angleX = foldingX * this.Completeness * 180.0f;
        this.boneZ0.localEulerAngles = new Vector3(0.0f, 0.0f, angleZ);
        this.boneZ1.localEulerAngles = new Vector3(0.0f, 0.0f, -angleZ);
        this.boneX.localEulerAngles = new Vector3(angleX, 0.0f, 0.0f);
    }
}
