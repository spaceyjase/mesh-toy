using System.Collections.Generic;
using Godot;
using Godot.Collections;

public class MeshBuilder : Resource
{
  private readonly List<Vector3> vertices = new List<Vector3>();
  private readonly List<int> indices = new List<int>();

  private readonly List<Vector3> normals = new List<Vector3>();
  private readonly List<Vector2> uvs = new List<Vector2>();

  private readonly List<int>[] submeshIndices;

  public MeshBuilder()
  {
    
  }

  public MeshBuilder(int submeshCount)
  {
    submeshIndices = new List<int>[submeshCount];
    for (var i = 0; i < submeshCount; i++)
    {
      submeshIndices[i] = new List<int>();
    }
  }

  public void BuildTriangle(Vector3 p0, Vector3 p1, Vector3 p2, int submesh)
  {
    var normal = (p1 - p0).Cross(p2 - p0).Normalized();
    BuildTriangle(p0, p1, p2, normal, submesh);
  }

  private void BuildTriangle(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 normal, int submesh)
  {
    var p0Index = vertices.Count;
    var p1Index = vertices.Count + 1;
    var p2Index = vertices.Count + 2;

    indices.Add(p0Index);
    indices.Add(p1Index);
    indices.Add(p2Index);

    submeshIndices[submesh].Add(p0Index);
    submeshIndices[submesh].Add(p1Index);
    submeshIndices[submesh].Add(p2Index);

    vertices.Add(p0);
    vertices.Add(p1);
    vertices.Add(p2);

    normals.Add(normal);
    normals.Add(normal);
    normals.Add(normal);

    uvs.Add(new Vector2(0, 0));
    uvs.Add(new Vector2(0, 1));
    uvs.Add(new Vector2(1, 1));
  }

  public Mesh CreateMesh()
  {
    var mesh = new ArrayMesh();

    var arr = new Array();
    arr.Resize((int)Mesh.ArrayType.Max);
    arr[(int)Mesh.ArrayType.Vertex] = vertices.ToArray();
    arr[(int)Mesh.ArrayType.TexUv] = uvs.ToArray();
    arr[(int)Mesh.ArrayType.Normal] = normals.ToArray();
    arr[(int)Mesh.ArrayType.Index] = indices.ToArray();

    mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, arr);
    
    return mesh;
  }
}
