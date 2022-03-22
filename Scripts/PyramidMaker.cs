using Godot;
using System;

public class PyramidMaker : MeshInstance
{
  [Export] private float pyramidSize = 5f;

  public override void _Ready()
  {
    base._Ready();
    
    var meshBuilder = new MeshBuilder(4);

    var top = new Vector3(0, pyramidSize, 0);

    var base0 = (Vector3.Forward * pyramidSize).Rotated(Vector3.Up, Mathf.Deg2Rad(0f));
    var base1 = (Vector3.Forward * pyramidSize).Rotated(Vector3.Up, Mathf.Deg2Rad(240f));
    var base2 = (Vector3.Forward * pyramidSize).Rotated(Vector3.Up, Mathf.Deg2Rad(120f));

    meshBuilder.BuildTriangle(base0, base1, base2, 0);
    meshBuilder.BuildTriangle(base1, base0, top, 1);
    meshBuilder.BuildTriangle(base2, top, base0, 2);
    meshBuilder.BuildTriangle(top, base2, base1, 3);

    Mesh = meshBuilder.CreateMesh();
    
    Mesh.SurfaceSetMaterial(0, ResourceLoader.Load<Material>("res://Materials/0.tres"));
    Mesh.SurfaceSetMaterial(1, ResourceLoader.Load<Material>("res://Materials/1.tres"));
    Mesh.SurfaceSetMaterial(2, ResourceLoader.Load<Material>("res://Materials/2.tres"));
    Mesh.SurfaceSetMaterial(3, ResourceLoader.Load<Material>("res://Materials/5.tres"));
  }
  
  public override void _Process(float delta)
  {
    base._Process(delta);
    
    // Rotate the cube around the Y axis
    RotateY(-Mathf.Deg2Rad(15) * delta);
  }
}
