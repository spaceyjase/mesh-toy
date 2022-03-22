using Godot;

public class ChessBoardMaker : MeshInstance
{
  [Export] private float cellSize = 1f;
  [Export] private int width = 8;
  [Export] private int height = 8;
  [Export] private uint submeshCount = 2;

  public override void _Ready()
  {
    base._Ready();

    var meshBuilder = new MeshBuilder(submeshCount);
    
    CreateChessBoard(meshBuilder);

    Mesh = meshBuilder.CreateMesh();
    
    Mesh.SurfaceSetMaterial(0, ResourceLoader.Load<Material>("res://Materials/3.tres"));
    Mesh.SurfaceSetMaterial(1, ResourceLoader.Load<Material>("res://Materials/6.tres"));
  }

  private void CreateChessBoard(MeshBuilder meshBuilder)
  {
    var points = new Vector3[width, height];
    var halfCellSize = cellSize / 2f;
    var halfWidth = width / 2;
    var halfHeight = height / 2;
    
    for (var x = -halfWidth; x < halfWidth; x++)
    {
      for (var z = -halfHeight; z < halfHeight; z++)
      {
        points[x + halfWidth, z + halfHeight] = new Vector3(
          (x * cellSize) + halfCellSize,
          0f,
          (z * cellSize) + halfCellSize);
      }
    }

    var submesh = 0u;
    for (var x = 0; x < width - 1; x++)
    {
      for (var z = 0; z < height - 1; z++)
      {
        var br = points[x, z];
        var bl = points[x + 1, z];
        var tr = points[x, z + 1];
        var tl = points[x + 1, z + 1];

        meshBuilder.BuildTriangle(bl, tr, tl, submesh % submeshCount);
        meshBuilder.BuildTriangle(bl, br, tr, submesh % submeshCount);
        
        submesh++;
      }
    }
  }
}
