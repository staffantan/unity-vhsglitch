using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class VHSPostProcessEffect : PostEffectsBase {
	static Material m_Material = null;
	protected Material material {
		get {
			if (m_Material == null) {
				m_Material = new Material(shader);
				m_Material.SetTexture("_VHSTex", VHS);
				m_Material.hideFlags = HideFlags.DontSave;
			}
			return m_Material;
		} 
	}

	public Shader shader;
	public MovieTexture VHS;

	float yScanline, xScanline;

	public override void Start() {
		//m = new Material(shader);
		//m.SetTexture("_VHSTex", VHS);
		//m.hideFlags = HideFlags.DontSave;
		VHS.loop = true;
		VHS.Play();
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination){
		yScanline += Time.deltaTime * 0.01f;
		xScanline -= Time.deltaTime * 0.1f;

		if(yScanline >= 1){
			yScanline = Random.value;
		}
		if(xScanline <= 0 || Random.value < 0.05){
			xScanline = Random.value;
		}
		material.SetFloat("_yScanline", yScanline);
		material.SetFloat("_xScanline", xScanline);
		Graphics.Blit(source, destination, material);
	}

	protected void OnDisable() {
		if( m_Material ) {
			DestroyImmediate( m_Material );
		}
	}	
}