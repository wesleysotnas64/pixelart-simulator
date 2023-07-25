Shader "Unlit/Phong"
{
    Properties
    {
        _Albedo("Albedo", Color) = (1.0, 1.0, 1.0, 1.0) //cor do objeto

        //Atributos Luz Ambiente
        _AmbientStrength("Ambient Strength", Range(0,1)) = 0.1
        _AmbientStrengthReflectivity("Ambient Strength Reflectivity", Range(0,1)) = 0.5

        //Intencidade da fonte de luz. Utilizada na luz difusa e especular.
        _LightStrength("Light Strength", Range(0,1)) = 0.1

        //Atributos Luz difusa
        _DifuseStrengthReflectivity("Difuse Strength Reflectivity", Range(0,1)) = 0.1

        //Atributos Luz especular
        _SpecularStrengthReflectivity("Specular Strength Reflectivity", Range(0,1)) = 0.5
        _Shininess("Shininess", Range(1,128)) = 32
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex     : SV_POSITION;
                float3 worldNormal: TEXCOORD0;
                float3 worldView  : TEXCOORD1;
            };

            float4 _Albedo;
            float  _AmbientStrength;
            float  _AmbientStrengthReflectivity;
            float  _LightStrength;
            float  _DifuseStrengthReflectivity;
            float  _SpecularStrengthReflectivity;
            int    _Shininess;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldView = WorldSpaceViewDir(v.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //Propriedades Externas
                float3 objectColor = _Albedo.xyz;
                float3 ligthColor  = _LightColor0.xyz; // #include "Lighting.cginc"

                //Vetores
                float3 viewDirection   = normalize(i.worldView);
                float3 normalDirection = normalize(i.worldNormal);
                float3 ligthDirection  = normalize(_WorldSpaceLightPos0.xyz);
                float3 reflectDirection = normalize(reflect(-ligthDirection, normalDirection));

                //Calculando ilumina��o ambiente
                float3 ambient = _AmbientStrength * _AmbientStrengthReflectivity * ligthColor;

                //Calculando ilumina��o difusa
                float diff = dot
                (
                    normalDirection,
                    ligthDirection
                );

                diff = max(diff, 0.0);
                float3 difuse = _LightStrength * _DifuseStrengthReflectivity * diff * ligthColor;

                //Calculando ilumina��o especular (reflexo)
                float spec = pow(max(dot(viewDirection, reflectDirection), 0.0), _Shininess);
                float3 specular = _LightStrength * _SpecularStrengthReflectivity * spec * ligthColor;
                
                //Cor final
                float3 finalColor = (ambient + difuse + specular) * objectColor;

                return fixed4(finalColor, 1.0);
            }
            ENDCG
        }
    }
}
