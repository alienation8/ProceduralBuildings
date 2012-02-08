using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Exception = System.Exception;

public class Building
{
	// fields
	
	private float _height;
	private float _floor_height;
	private int _floor_number;
	private readonly BuildingType _type;
	private List<Vector3> _boundaries = new List<Vector3>();
	private List<Face> _faces = new List<Face>();
	
	
	// properties
	
	/// <summary>
	/// Gets the height.
	/// </summary>
	/// <value>
	/// The height.
	/// </value>
	public float Height
	{
		get { return _height; }
	}
	
	/// <summary>
	/// Gets or sets the height of the floor.
	/// </summary>
	/// <value>
	/// The height of the floor.
	/// </value>
	public float FloorHeight
	{
		get { return _floor_height; }
		protected set 
		{
			_floor_height = value;
			if (_floor_number > 0)
			{
				_height = _floor_height * _floor_number;
				if (_boundaries.Count == 4)
				{
					for (int i = 0; i < 4; ++i)
						_boundaries.Add(_boundaries[i] + new Vector3(0f, _height, 0f));
				}
			}
		}
	}
	
	/// <summary>
	/// Gets or sets the floor number.
	/// </summary>
	/// <value>
	/// The floor number.
	/// </value>
	public int FloorNumber
	{
		get { return _floor_number; }
		protected set
		{
			_floor_number = value;
			if (_floor_height > 0f)
			{
				_height = _floor_height * _floor_number;
				if (_boundaries.Count == 4)
				{
					for (int i = 0; i < 4; ++i)
						_boundaries.Add(_boundaries[i] + new Vector3(0f, _height, 0f));
				}
			}
		}
	}
	
	/// <summary>
	/// Gets the building type.
	/// </summary>
	/// <value>
	/// The building type.
	/// </value>
	public BuildingType Type
	{
		get { return _type; }
	}

	public ReadOnlyCollection<Face> Faces
	{
		get { return _faces.AsReadOnly(); }
	}

	public ReadOnlyCollection<Vector3> Boundaries
	{
		get { return _boundaries.AsReadOnly(); }
	}
	
	public Vector3[] BoundariesArray
	{
		get
		{
			Vector3[] v = new Vector3[Boundaries.Count];
			for (int i = 0; i < Boundaries.Count; ++i)
				v[i] = Boundaries[i];
			return v;
		}
	}
	
	
	// constructors
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Building"/> class.
	/// The given Vector3 points must be given in clockwise order (required
	/// for the correct calculation of the surface's normal).
	/// </summary>
	/// <param name='p1'>
	/// A point in space.
	/// </param>
	/// <param name='p2'>
	/// A point in space.
	/// </param>
	/// <param name='p3'>
	/// A point in space.
	/// </param>
	/// <param name='p4'>
	/// A point in space.
	/// </param>
	/// <param name='type'>
	/// The type of the building.
	/// </param>
	public Building (Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, BuildingType type)
	{
		_boundaries.Add(p1);
		_boundaries.Add(p2);
		_boundaries.Add(p3);
		_boundaries.Add(p4);
		_type = type;
		
		_height = 0f;
		_floor_height = 0f;
		_floor_number = 0;
	}
	
	
	// methods
	
	/// <summary>
	/// Constructs the faces of the building.
	/// </summary>
	public virtual void ConstructFaces ()
	{
		_faces.Add(new Face(this, _boundaries[0], _boundaries[1]));
		_faces.Add(new Face(this, _boundaries[1], _boundaries[2]));
		_faces.Add(new Face(this, _boundaries[2], _boundaries[3]));
		_faces.Add(new Face(this, _boundaries[3], _boundaries[0]));
	}
	

	public virtual void ConstructFaceComponents ()
	{
		throw new Exception("Building.ConstructFaceComponents() should never be called.");		
	}
	
	/// <summary>
	/// Draw the current mesh.
	/// </summary>
	/// <param name='material'>
	/// A UnityEngine.Material.
	/// </param>
	public void Draw (Material material)
	{
		material.SetPass(0);
	
		// draw roof
		GL.PushMatrix();
		GL.Begin(GL.QUADS);
		for (int i = 4; i < 8; ++i)
			GL.Vertex(_boundaries[i]);
		GL.End();
		GL.PopMatrix();	
		
		// draw faces
		foreach (Face face in _faces)
			face.Draw();
	}
}
