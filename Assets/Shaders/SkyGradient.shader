// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.35 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.35;sub:START;pass:START;ps:flbk:,iptp:2,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:False,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:1,fgcb:0.9172411,fgca:1,fgde:0.01,fgrn:0,fgrf:1000,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3554,x:32855,y:32465,varname:node_3554,prsc:2|emission-2361-OUT;n:type:ShaderForge.SFN_TexCoord,id:4405,x:31702,y:32557,varname:node_4405,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Color,id:3767,x:32262,y:32315,ptovrint:False,ptlb:Sky Color,ptin:_SkyColor,varname:_SkyColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:3459,x:32091,y:32435,ptovrint:False,ptlb:Horizon,ptin:_Horizon,varname:_Horizon,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:2361,x:32613,y:32431,varname:node_2361,prsc:2|A-3459-RGB,B-3767-RGB,T-2603-OUT;n:type:ShaderForge.SFN_Slider,id:3033,x:31929,y:33007,ptovrint:False,ptlb:Height,ptin:_Height,varname:_Height,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:5.213675,max:10;n:type:ShaderForge.SFN_Multiply,id:2603,x:32343,y:32669,varname:node_2603,prsc:2|A-5453-OUT,B-3033-OUT,C-3942-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:5453,x:32135,y:32621,varname:node_5453,prsc:2|IN-1942-OUT,IMIN-8156-OUT,IMAX-5818-OUT,OMIN-1455-OUT,OMAX-3616-OUT;n:type:ShaderForge.SFN_Vector1,id:8156,x:31903,y:32723,varname:node_8156,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:5818,x:31885,y:32800,varname:node_5818,prsc:2,v1:1;n:type:ShaderForge.SFN_Slider,id:3616,x:32262,y:33018,ptovrint:False,ptlb:Horizon Curve Pos,ptin:_HorizonCurvePos,varname:_HorizonCurvePos,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3418804,max:4;n:type:ShaderForge.SFN_Vector1,id:1455,x:31979,y:32900,varname:node_1455,prsc:2,v1:0;n:type:ShaderForge.SFN_Slider,id:3942,x:32628,y:33069,ptovrint:False,ptlb:Beam Width,ptin:_BeamWidth,varname:_BeamWidth,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:2;n:type:ShaderForge.SFN_ComponentMask,id:1942,x:31869,y:32557,varname:node_1942,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-6947-OUT;n:type:ShaderForge.SFN_ViewVector,id:6947,x:31678,y:32751,varname:node_6947,prsc:2;proporder:3767-3459-3033-3616-3942;pass:END;sub:END;*/

Shader "JRW/SkyGradient" {
    Properties {
        _SkyColor ("Sky Color", Color) = (0,0,0,1)
        _Horizon ("Horizon", Color) = (1,1,1,1)
        _Height ("Height", Range(0, 10)) = 5.213675
        _HorizonCurvePos ("Horizon Curve Pos", Range(0, 4)) = 0.3418804
        _BeamWidth ("Beam Width", Range(0, 2)) = 2
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
            "PreviewType"="Skybox"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 2.0
            uniform float4 _SkyColor;
            uniform float4 _Horizon;
            uniform float _Height;
            uniform float _HorizonCurvePos;
            uniform float _BeamWidth;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
////// Lighting:
////// Emissive:
                float node_8156 = 0.0;
                float node_1455 = 0.0;
                float3 emissive = lerp(_Horizon.rgb,_SkyColor.rgb,((node_1455 + ( (viewDirection.g - node_8156) * (_HorizonCurvePos - node_1455) ) / (1.0 - node_8156))*_Height*_BeamWidth));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
