using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShaderGraphTest
{
    [ExecuteInEditMode]
    public class UpdateShaderProperties : MonoBehaviour
    {
        private void Update()
        {
            if (gameObject.transform.hasChanged)
            {
                Renderer[] renderers = GameObject.FindObjectsOfType<Renderer>();
                foreach (var r in renderers)
                {
                    Material m;
#if UNITY_EDITOR
                    m = r.sharedMaterial;
#else
                    m = r.material;
#endif
                    if (string.Compare(m.shader.name, "Shader Graphs/ToonRamp_Texture") == 0 || string.Compare(m.shader.name, "Shader Graphs/ToonRamp_Color") == 0)
                    {
                        m.SetVector("_LightDir", transform.forward);
                    }
                }
            }
        }
    }
}
