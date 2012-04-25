using UnityEngine;

namespace Thesis {

public sealed class NeoWindow : Base.Window
{
  /*************** CONSTRUCTORS ***************/

  public NeoWindow (Base.Face parent, Vector3 dr, Vector3 dl, ComponentCoordinate position)
    : base (parent, position)
  {
    height = ((NeoBuildingMesh) parentBuilding).windowHeight;
    depth = 0.2f;
    float height_modifier = parentBuilding.floorHeight / 2 - height / 2;

    boundaries = new Vector3[4];
    boundaries[0] = new Vector3(dr.x, dr.y + height_modifier, dr.z);
    boundaries[1] = new Vector3(dl.x, dl.y + height_modifier, dl.z);
    boundaries[2] = new Vector3(dl.x, dl.y + height + height_modifier, dl.z);
    boundaries[3] = new Vector3(dr.x, dr.y + height + height_modifier, dr.z);

    frame = new Base.WindowFrame(this);
    frame.name = "neo_window_frame";
    frame.materialName = "ComponentFrame";

    body = new Base.Glass(this);
  }

  /*************** METHODS ***************/

  public override void Draw ()
  {
    //base.Draw();

    frame.FindVertices();
    frame.FindTriangles();
    frame.Draw();

    body.FindVertices();
    body.FindTriangles();
    body.Draw();
  }
}

} // namespace Thesis