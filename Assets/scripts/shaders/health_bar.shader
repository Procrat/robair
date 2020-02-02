// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader HealthBar_BW use for Black and White gradient mask texture
Shader "Custom/HealthBar"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        [HideInInspector]_Percent ("Percent", Range(0, 1)) = 1
    }
    SubShader
    {
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float _Percent;
            float y_pos;
            float _height;

            struct vertexInput
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct vertexOutput
            {
                float4 pos : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            vertexOutput vert (vertexInput i)
            {
                vertexOutput o;
                UNITY_INITIALIZE_OUTPUT(vertexOutput, o);

                o.pos = UnityObjectToClipPos(i.vertex);
                //_height = height_coords.y;
                o.texcoord = i.texcoord;

                return o;
            }

            fixed4 frag (vertexOutput i) : COLOR
            {
                fixed4 mainTex = tex2D(_MainTex, i.texcoord);

                if(i.texcoord.y <= _Percent)
                {
                    if(_Percent <= 0.65)
                    {
                        mainTex.r = 1;
                        mainTex.g = 0.949;
                        mainTex.b = 0;
                    }
                    if(_Percent <= 0.45)
                    {
                        mainTex.r = 1;
                        mainTex.g = 0.698;
                        mainTex.b = 0.4;
                    }
                    if(_Percent <= 0.25)
                    {
                        mainTex.r = 1;
                        mainTex.g = 0;
                        mainTex.b = 0;
                    }
                }
                else
                {
                    mainTex.r = 0.109;
                    mainTex.g = 0.168;
                    mainTex.b = 0.2;
                }
                return mainTex;
            }

            ENDCG
        }
    }
}
