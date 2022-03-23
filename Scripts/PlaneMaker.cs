using Godot;

public class PlaneMaker : MeshInstance
{
  [Export] private float cellSize = 1f;
  [Export] private int width = 24;
  [Export] private int height = 24;
  [Export] private uint submeshCount = 6;

  public override void _Ready()
  {
    base._Ready();
    
    var meshBuilder = new MeshBuilder(submeshCount);
    
    // points for our plane
    var points = new Vector3[width, height];
    for (var x = 0; x < width; x++)
    {
      for (var z = 0; z < height; z++)
      {
        points[x, z] = new Vector3(x * cellSize,
          0f,//Mathf.PingPong(x * cellSize, 1f),
          z * cellSize);
      }
    }

    var submesh = 0u;
    for (var x = 0; x < width - 1; x++)
    {
      for (var z = 0; z < height - 1; z++)
      {
        var br = points[x,     z];
        var bl = points[x + 1, z];
        var tr = points[x,     z + 1];
        var tl = points[x + 1, z + 1];

        meshBuilder.BuildTriangle(bl, tr, tl, submesh % submeshCount);
        meshBuilder.BuildTriangle(bl, br, tr, submesh % submeshCount);
      }

      submesh++;
    }

    Mesh = meshBuilder.CreateMesh();
    
    for (var i = 0u; i < submeshCount; i++)
    {
      Mesh.SurfaceSetMaterial(0, ResourceLoader.Load<Material>($"res://Materials/{i}.tres"));
    }
  }
}
