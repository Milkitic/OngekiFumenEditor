﻿using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngekiFumenEditor.Modules.FumenPreviewer.Graphics.Drawing.Shaders
{
    public class CommonLineShader : Shader
    {
        private static CommonLineShader _shared;

        public CommonLineShader()
        {
            VertexProgram = @"
                #version 330

out vec4 varying_color;

uniform mat4 Model;
uniform mat4 ViewProjection;

layout(location=0) in vec2 in_pos;
layout(location=1) in vec4 in_color;

void main(){
	gl_Position = ViewProjection * Model * vec4(in_pos.x,in_pos.y,0.0,1.0);
	varying_color=in_color;
}
                ";
            FragmentProgram = @"
                #version 330

in vec4 varying_color;
out vec4 out_color;

void main(){
	out_color = varying_color;
}
                ";
        }

        public static CommonLineShader Shared => _shared ??= _createShared();

        private static CommonLineShader _createShared()
        {
            var shader = new CommonLineShader();
            shader.Compile();
            var error = GL.GetError();
            if (error != ErrorCode.NoError)
                throw new Exception($"Create shared CommonSpriteShader object failed ,OGL Error : {error}");
            return shader;
        }
    }
}
