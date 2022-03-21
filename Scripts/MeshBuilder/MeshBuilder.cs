using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Godot;
using Godot.Collections;

public class MeshBuilder : Resource
{
  private readonly System.Collections.Generic.Dictionary<int, List<Vector3>> vertices = new();
  private readonly System.Collections.Generic.Dictionary<int, List<int>> indices = new();

  private readonly System.Collections.Generic.Dictionary<int, List<Vector3>> normals = new(); 
  private readonly System.Collections.Generic.Dictionary<int, List<Vector2>> uvs = new();
  
  private readonly int _submeshCount;

  public MeshBuilder() { }    // Required by godot

  public MeshBuilder(int submeshCount)
  {
    _submeshCount = submeshCount;
  }

  public void BuildTriangle(Vector3 p0, Vector3 p1, Vector3 p2, int submesh)
  {
    var normal = (p1 - p0).Cross(p2 - p0).Normalized();
    BuildTriangle(p0, p1, p2, normal, submesh);
  }

  private void BuildTriangle(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 normal, int submesh)
  {
    if (!vertices.ContainsKey(submesh))
    {
      vertices[submesh] = new List<Vector3>();
      indices[submesh] = new List<int>();
      normals[submesh] = new List<Vector3>();
      uvs[submesh] = new List<Vector2>();
    }
    
    var p0Index = vertices[submesh].Count;
    var p1Index = vertices[submesh].Count + 1;
    var p2Index = vertices[submesh].Count + 2;

    indices[submesh].Add(p2Index);
    indices[submesh].Add(p1Index);
    indices[submesh].Add(p0Index);

    vertices[submesh].Add(p0);
    vertices[submesh].Add(p1);
    vertices[submesh].Add(p2);

    normals[submesh].Add(normal);
    normals[submesh].Add(normal);
    normals[submesh].Add(normal);

    uvs[submesh].Add(new Vector2(1, 1));
    uvs[submesh].Add(new Vector2(0, 1));
    uvs[submesh].Add(new Vector2(0, 0));
  }

  public Mesh CreateMesh()
  {
    var mesh = new ArrayMesh();
    for (var i = 0; i < _submeshCount; ++i)
    {
      var arr = new Array();
      arr.Resize((int)Mesh.ArrayType.Max);
      arr[(int)Mesh.ArrayType.Vertex] = vertices[i].ToArray();
      arr[(int)Mesh.ArrayType.TexUv] = uvs[i].ToArray();
      arr[(int)Mesh.ArrayType.Normal] = normals[i].ToArray();
      arr[(int)Mesh.ArrayType.Index] = indices[i].ToArray();
      
      mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, arr);
    }

    return mesh;
  }
}
