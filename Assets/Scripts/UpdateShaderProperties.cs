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
                    m = r.material;
#else
                    m = r.sharedMaterial;
#endif
                    if (string.Compare(m.shader.name, "Shader Graphs/ToonRamp") == 0)
                    {
                        m.SetVector("_LightDir", transform.forward);
                    }
                }
            }
        }
    }
}
