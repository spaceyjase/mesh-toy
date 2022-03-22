using Godot;

public class CubeBuilder : MeshInstance
{
  public override void _Ready()
  {
    base._Ready();
    
    var meshBuilder = new MeshBuilder(6); // 6 faces

    var cubeSize = Vector3.One * 0.5f; // half the size passed in

    var t0 = new Vector3(cubeSize.x, cubeSize.y, -cubeSize.z); // top-left point
    var t1 = new Vector3(-cubeSize.x, cubeSize.y, -cubeSize.z); // top-right point
    var t2 = new Vector3(-cubeSize.x, cubeSize.y, cubeSize.z);
    var t3 = new Vector3(cubeSize.x, cubeSize.y, cubeSize.z);

    var b0 = new Vector3(cubeSize.x, -cubeSize.y, -cubeSize.z); // bottom-left point
    var b1 = new Vector3(-cubeSize.x, -cubeSize.y, -cubeSize.z); // bottom-right point
    var b2 = new Vector3(-cubeSize.x, -cubeSize.y, cubeSize.z);
    var b3 = new Vector3(cubeSize.x, -cubeSize.y, cubeSize.z);

    // top square
    meshBuilder.BuildTriangle(t0, t1, t2, 0);
    meshBuilder.BuildTriangle(t0, t2, t3, 0);

    // bottom square - note winding order (so it faces the right way)
    meshBuilder.BuildTriangle(b2, b1, b0, 1);
    meshBuilder.BuildTriangle(b3, b2, b0, 1);

    // back
    meshBuilder.BuildTriangle(b0, t1, t0, 2);
    meshBuilder.BuildTriangle(b0, b1, t1, 2);

    // sides
    meshBuilder.BuildTriangle(b1, t2, t1, 3);
    meshBuilder.BuildTriangle(b1, b2, t2, 3);

    meshBuilder.BuildTriangle(b2, t3, t2, 4);
    meshBuilder.BuildTriangle(b2, b3, t3, 4);

    meshBuilder.BuildTriangle(b3, t0, t3, 5);
    meshBuilder.BuildTriangle(b3, b0, t0, 5);

    Mesh = meshBuilder.CreateMesh();
    
    Mesh.SurfaceSetMaterial(0, ResourceLoader.Load<Material>("res://Materials/0.tres"));
    Mesh.SurfaceSetMaterial(1, ResourceLoader.Load<Material>("res://Materials/1.tres"));
    Mesh.SurfaceSetMaterial(2, ResourceLoader.Load<Material>("res://Materials/2.tres"));
    Mesh.SurfaceSetMaterial(3, ResourceLoader.Load<Material>("res://Materials/3.tres"));
    Mesh.SurfaceSetMaterial(4, ResourceLoader.Load<Material>("res://Materials/4.tres"));
    Mesh.SurfaceSetMaterial(5, ResourceLoader.Load<Material>("res://Materials/5.tres"));
  }

  public override void _Process(float delta)
  {
    base._Process(delta);
    
    // Rotate the cube around the Y axis
    RotateY(Mathf.Deg2Rad(25) * delta);
  }
}