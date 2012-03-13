using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{
  private enum BuildingMode
  {
    Many,
    Big,
    Small
  }

  private List<Neoclassical> neo = new List<Neoclassical>();

	void Start ()
  {
	  CreateNeoclassical();
	}

	void Update ()
  {
    if (Input.GetKeyUp(KeyCode.Alpha1))
    {
      DestroyBuildings();
      CreateNeoclassical(BuildingMode.Many);
    }

    if (Input.GetKeyUp(KeyCode.Alpha2))
    {
      DestroyBuildings();
      CreateNeoclassical(BuildingMode.Big);
    }

    if (Input.GetKeyUp(KeyCode.Alpha3))
    {
      DestroyBuildings();
      CreateNeoclassical(BuildingMode.Small);
    }
	}

  private void CreateNeoclassical(BuildingMode mode = BuildingMode.Small)
  {
    switch (mode)
    {
      case BuildingMode.Many:
        for (int i = 0; i < 25; ++i)
        {
          float x_mod = i * 15f;
          for (int j = 0; j < 25; ++j)
          {
            float z_mod = j * 9f;
            var n = new Neoclassical(
              new Vector3(x_mod + Random.Range(0.5f, 1.5f) + 9f, 0f, z_mod + Random.Range(0.5f, 1.5f) + 3.5f),
              new Vector3(x_mod + Random.Range(0.5f, 1.5f) + 9f, 0f, z_mod - Random.Range(0.5f, 1.5f)), 
              new Vector3(x_mod - Random.Range(0.5f, 1.5f)     , 0f, z_mod - Random.Range(0.5f, 1.5f)), 
              new Vector3(x_mod - Random.Range(0.5f, 1.5f)     , 0f, z_mod + Random.Range(0.5f, 1.5f) + 3.5f));
            n.Draw();
            n.CombineWindowFrames();
            n.CombineWindowGlasses();
            n.SetActiveRecursively(true);
            neo.Add(n);
          }
        }
        break;

      case BuildingMode.Big:
        var a = new Neoclassical(
          new Vector3(Random.Range(0.5f, 1.5f) + 20f, 0f,  Random.Range(0.5f, 1.5f) + 8f), 
          new Vector3(Random.Range(0.5f, 1.5f) + 20f, 0f,  Random.Range(0.5f, 1.5f)), 
          new Vector3(Random.Range(0.5f, 1.5f)      , 0f, -Random.Range(0.5f, 1.5f)), 
          new Vector3(Random.Range(0.5f, 1.5f)      , 0f,  Random.Range(0.5f, 1.5f) + 8f));
        a.Draw();
        a.CombineWindowFrames();
        a.CombineWindowGlasses();
        a.SetActiveRecursively(true);
        neo.Add(a);
        break;

      case BuildingMode.Small:
        var b = new Neoclassical(
          new Vector3(Random.Range(0.25f, 0.75f) + 9f, 0f,  Random.Range(0.25f, 0.75f) + 3.5f), 
          new Vector3(Random.Range(0.25f, 0.75f) + 9f, 0f,  Random.Range(0.25f, 0.75f)), 
          new Vector3(Random.Range(0.25f, 0.75f)     , 0f, -Random.Range(0.25f, 0.75f)), 
          new Vector3(Random.Range(0.25f, 0.75f)     , 0f,  Random.Range(0.25f, 0.75f) + 3.5f));
        b.Draw();
        b.CombineWindowFrames();
        b.CombineWindowGlasses();
        b.SetActiveRecursively(true);
        neo.Add(b);
        break;
    }
  }

  public void DestroyBuildings ()
  {
    foreach (Neoclassical n in neo)
      Destroy(n.gameObject);
    neo.Clear();
  }
}