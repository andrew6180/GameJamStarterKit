Shader "GameJamStarterKit/Lit - DevGrid"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Metallic ("Metallic", Range(0, 1)) = 0
        _Smoothness ("Smoothness", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
        };
        sampler2D _MainTex;
        fixed4 _Color;
        half _Metallic;
        half _Smoothness;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
        
        void vert(inout appdata_full v)
        {
            // Get the worldspace normal after transformation, and ensure it's unit length.
            float3 n = normalize(mul(unity_ObjectToWorld, v.normal).xyz);
    
            // Pick a direction for our texture's vertical "v" axis. 
            // Default for floors/ceilings:
            float3 vDirection = float3(0, 0, 1);
    
            // For non-horizontal planes, we'll choose
            // the closest vector in the polygon's plane to world up.
            if(abs(n.y) < 1.0f) {
                 vDirection = normalize(float3(0, 1, 0) - n.y * n);
            }
    
            // Get the perpendicular in-plane vector to use as our "u" direction.
            float3 uDirection = normalize(cross(n, vDirection));
    
            // Get the position of the vertex in worldspace.
            float3 worldSpace = mul(unity_ObjectToWorld, v.vertex).xyz;
    
            // Project the worldspace position of the vertex into our texturing plane,
            // and use this result as the primary texture coordinate.
            v.texcoord.xy = float2(dot(worldSpace, uDirection), dot(worldSpace, vDirection));
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex * 2) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
