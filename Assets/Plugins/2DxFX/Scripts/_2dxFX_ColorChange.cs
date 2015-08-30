﻿//////////////////////////////////////////////
/// 2DxFX - 2D SPRITE FX - by VETASOFT 2015 //
//////////////////////////////////////////////

using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[AddComponentMenu ("2DxFX/Standard/ColorChange")]
[System.Serializable]
public class _2dxFX_ColorChange : MonoBehaviour
{
	[HideInInspector] public Material ForceMaterial;
	[HideInInspector] public bool ActiveChange=true;

	private string shader = "2DxFX/Standard/ColorChange";

	[HideInInspector] [Range(0, 1)] public float _Alpha = 1f;
	[HideInInspector] [Range(0, 1)] public float _Tolerance = 1f;
	[HideInInspector] [Range(0, 360)] public float _HueShift = 180f;
	[HideInInspector] [Range(-2, 2)] public float _Saturation = 1f;
	[HideInInspector] [Range(-2, 2)] public float _ValueBrightness = 1f;
	[HideInInspector] public Color _Color = new Color (0f, 1f, 1f, 1f);

	[HideInInspector] public int ShaderChange=0;
	Material tempMaterial;
	Material defaultMaterial;

	
	void Start ()
	{ 
		ShaderChange = 0;
	}

 	public void CallUpdate()
	{
		Update ();
	}

	void Update()
	{	
		if ((ShaderChange == 0) && (ForceMaterial != null)) 
		{
			ShaderChange=1;
			if (tempMaterial!=null) DestroyImmediate(tempMaterial);
			GetComponent<Renderer>().sharedMaterial = ForceMaterial;
			ForceMaterial.hideFlags = HideFlags.DontSave;
			ForceMaterial.shader=Shader.Find(shader);
			ActiveChange=false;

		}
		if ((ForceMaterial == null) && (ShaderChange==1))
		{
			if (tempMaterial!=null) DestroyImmediate(tempMaterial);
			tempMaterial = new Material(Shader.Find(shader));
			tempMaterial.hideFlags = HideFlags.DontSave;
			GetComponent<Renderer>().sharedMaterial = tempMaterial;
			ShaderChange=0;
		}
		
		#if UNITY_EDITOR
		if (GetComponent<Renderer>().sharedMaterial.shader.name == "Sprites/Default")
		{
			ForceMaterial.shader=Shader.Find(shader);
			ForceMaterial.hideFlags = HideFlags.DontSave;
			GetComponent<Renderer>().sharedMaterial = ForceMaterial;
		}
		#endif
		if (ActiveChange)
		{
			GetComponent<Renderer>().sharedMaterial.SetFloat("_Alpha", 1-_Alpha);
			GetComponent<Renderer>().sharedMaterial.SetColor("_ColorX", _Color);
			GetComponent<Renderer>().sharedMaterial.SetFloat("_Tolerance", _Tolerance);
			GetComponent<Renderer>().sharedMaterial.SetFloat("_HueShift", _HueShift);
			GetComponent<Renderer>().sharedMaterial.SetFloat("_Sat", _Saturation);
			GetComponent<Renderer>().sharedMaterial.SetFloat("_Val", _ValueBrightness);

		}
		
	}
	
	void OnDestroy()
	{
		if ((Application.isPlaying == false) && (Application.isEditor == true) && (Application.isLoadingLevel == false)) {
			
			if (tempMaterial!=null) DestroyImmediate(tempMaterial);
			
			if (gameObject.activeSelf && defaultMaterial!=null) {
				GetComponent<Renderer>().sharedMaterial = defaultMaterial;
				GetComponent<Renderer>().sharedMaterial.hideFlags = HideFlags.None;
			}
		}
	}
	void OnDisable()
	{ 
		if (gameObject.activeSelf && defaultMaterial!=null) {
			GetComponent<Renderer>().sharedMaterial = defaultMaterial;
			GetComponent<Renderer>().sharedMaterial.hideFlags = HideFlags.None;
		}		
	}
	
	void OnEnable()
	{
		if (defaultMaterial == null) {
			defaultMaterial = new Material(Shader.Find("Sprites/Default"));
			 
			
		}
		if (ForceMaterial==null)
		{
			ActiveChange=true;
			tempMaterial = new Material(Shader.Find(shader));
			tempMaterial.hideFlags = HideFlags.DontSave;
			GetComponent<Renderer>().sharedMaterial = tempMaterial;
		}
		else
		{
			ForceMaterial.shader=Shader.Find(shader);
			ForceMaterial.hideFlags = HideFlags.DontSave;
			GetComponent<Renderer>().sharedMaterial = ForceMaterial;
		}
		
	}
}




#if UNITY_EDITOR
[CustomEditor(typeof(_2dxFX_ColorChange)),CanEditMultipleObjects]
public class _2dxFX_ColorChange_Editor : Editor
{
	private SerializedObject m_object;
	
	public void OnEnable()
	{
		m_object = new SerializedObject(targets);
	}
	
	public override void OnInspectorGUI()
	{
		m_object.Update();
		DrawDefaultInspector();
		
		_2dxFX_ColorChange _2dxScript = (_2dxFX_ColorChange)target;
	
		Texture2D icon = Resources.Load ("2dxfxinspector") as Texture2D;
		if (icon)
		{
			Rect r;
			float ih=icon.height;
			float iw=icon.width;
			float result=ih/iw;
			float w=Screen.width;
			result=result*w;
			r = GUILayoutUtility.GetRect(ih, result);
			EditorGUI.DrawTextureTransparent(r,icon);
		}

		EditorGUILayout.PropertyField(m_object.FindProperty("ForceMaterial"), new GUIContent("Shared Material", "Use a unique material, reduce drastically the use of draw call"));
		
		if (_2dxScript.ForceMaterial == null)
		{
			_2dxScript.ActiveChange = true;
		}
		else
		{
			if(GUILayout.Button("Remove Shared Material"))
			{
				_2dxScript.ForceMaterial= null;
				_2dxScript.ShaderChange = 1;
				_2dxScript.ActiveChange = true;
				_2dxScript.CallUpdate();
			}
		
			EditorGUILayout.PropertyField (m_object.FindProperty ("ActiveChange"), new GUIContent ("Change Material Property", "Change The Material Property"));
		}

		if (_2dxScript.ActiveChange)
		{

			EditorGUILayout.BeginVertical("Box");

		
			Texture2D icone = Resources.Load ("2dxfx-icon-color") as Texture2D;
			EditorGUILayout.PropertyField(m_object.FindProperty("_HueShift"), new GUIContent("Change Hue", icone, "Change the color from the Selected Color"));
			icone = Resources.Load ("2dxfx-icon-color") as Texture2D;
			EditorGUILayout.PropertyField(m_object.FindProperty("_Color"), new GUIContent("Selected Color", icone, "Pick up a color"));
			icone = Resources.Load ("2dxfx-icon-color") as Texture2D;
			EditorGUILayout.PropertyField(m_object.FindProperty("_Tolerance"), new GUIContent("Tolerance", icone, "the tolerance of the selected color"));
			icone = Resources.Load ("2dxfx-icon-contrast") as Texture2D;
			EditorGUILayout.PropertyField(m_object.FindProperty("_Saturation"), new GUIContent("Color Saturation", icone, "Change the saturation"));
			icone = Resources.Load ("2dxfx-icon-brightness") as Texture2D;
			EditorGUILayout.PropertyField(m_object.FindProperty("_ValueBrightness"), new GUIContent("Brighntess", icone, "Change the brightness"));

		

			EditorGUILayout.BeginVertical("Box");

			icone = Resources.Load ("2dxfx-icon-fade") as Texture2D;
			EditorGUILayout.PropertyField(m_object.FindProperty("_Alpha"), new GUIContent("Fading", icone, "Fade from nothing to showing"));

			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
	

		}
		
		m_object.ApplyModifiedProperties();
		
	}
}
#endif