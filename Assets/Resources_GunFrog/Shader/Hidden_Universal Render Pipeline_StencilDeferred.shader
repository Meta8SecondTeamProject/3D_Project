//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Hidden/Universal Render Pipeline/StencilDeferred" {
Properties {
_StencilRef ("StencilRef", Float) = 0
_StencilReadMask ("StencilReadMask", Float) = 0
_StencilWriteMask ("StencilWriteMask", Float) = 0
_LitPunctualStencilRef ("LitPunctualStencilWriteMask", Float) = 0
_LitPunctualStencilReadMask ("LitPunctualStencilReadMask", Float) = 0
_LitPunctualStencilWriteMask ("LitPunctualStencilWriteMask", Float) = 0
_SimpleLitPunctualStencilRef ("SimpleLitPunctualStencilWriteMask", Float) = 0
_SimpleLitPunctualStencilReadMask ("SimpleLitPunctualStencilReadMask", Float) = 0
_SimpleLitPunctualStencilWriteMask ("SimpleLitPunctualStencilWriteMask", Float) = 0
_LitDirStencilRef ("LitDirStencilRef", Float) = 0
_LitDirStencilReadMask ("LitDirStencilReadMask", Float) = 0
_LitDirStencilWriteMask ("LitDirStencilWriteMask", Float) = 0
_SimpleLitDirStencilRef ("SimpleLitDirStencilRef", Float) = 0
_SimpleLitDirStencilReadMask ("SimpleLitDirStencilReadMask", Float) = 0
_SimpleLitDirStencilWriteMask ("SimpleLitDirStencilWriteMask", Float) = 0
_ClearStencilRef ("ClearStencilRef", Float) = 0
_ClearStencilReadMask ("ClearStencilReadMask", Float) = 0
_ClearStencilWriteMask ("ClearStencilWriteMask", Float) = 0
}
SubShader {
 Tags { "RenderPipeline" = "UniversalPipeline" "RenderType" = "Opaque" }
}
Fallback "Hidden/Universal Render Pipeline/FallbackError"
}