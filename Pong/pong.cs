using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace meuJogo
{
    class program : GameWindows
    {
        int XdaBola = 0;
        int YdaBola = 0;
        int TamBola = 20;
        int VelDaBolaX = 3;
        int VelDaBolaY = 3;
        int YdoJog1 = 0;
        int YdoJog2 = 0;

        int xdojog1()
        {
            return -ClientSize.Width / 2 + LargJog()/2;
        }

        int xdojog2()
        {
            return ClientSize.Width / 2 - LargJog()/2;
        }

        int LargJog()
        {

            return TamBola;
        }

        int AltJog()
        {
            return 5*TamBola;
        }

        protected override void OnUpdateFrame(FrameEventArgs_e)
        {

            XdaBola = XdaBola + VelDaBolaX;
            YdaBola = YdaBola + VelDaBolaY;

            if(XdaBola + TamBola / 2 > xdojog2() - LargJog() /2 
            && YdaBola - TamBola / 2 < YdoJog2() + AltJog() / 2
            && YdaBola + TamBola / 2 > YdoJog2() - AltJog() / 2)
            {
                VelDaBolaX = -VelDaBolaX;
            }
            if(XdaBola - TamBola / 2 < xdojog1() + LargJog() /2 
            && YdaBola + TamBola / 2 > YdoJog1() - AltJog() / 2
            && YdaBola - TamBola / 2 < YdoJog1() + AltJog() / 2)
            {
                VelDaBolaX = -VelDaBolaX;
            }
            if(YdaBola + TamBola / 2 > ClientSize.Height / 2)
            {
                VelDaBolaY = -VelDaBolaY;
            }
            if(YdaBola - TamBola / 2 < ClientSize.Height / 2)
            {
                VelDaBolaY = -VelDaBolaY;
            }
            if(XdaBola < -ClientSize.Width / 2 || XdaBola > ClientSize.Width / 2)
            {
                XdaBola = 0;
                YdaBola = 0;

            }

            if(Keyboard.GetState().IsKeyDown(Key.w))
            {
                YdoJog1 = YdoJog1 + 5;
            }
            
            if(Keyboard.GetState().IsKeyDown(Key.s))
            {
                YdoJog1 = YdoJog1 - 5;
            }
            if(Keyboard.GetState().IsKeyDown(Key.Up))
            {
                XdoJog1 = XdoJog1 + 5;
            }
            
            if(Keyboard.GetState().IsKeyDown(Key.Down))
            {
                XdoJog1 = XdoJog1 - 5;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs_e)
        {
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

            Matrix4 projection = Matrix4.CreaterOrthographic(ClientSize.Width, ClientSize.Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.projection);
            GL.LoadMatrix(ref projection);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            DesenharRetangulo(XdaBola, YdaBola, TamBola, TamBola, 1.0f, 1.0f, 0.0f);
            DesenharRetangulo(xdojog1(), YdoJog1, LargJog(), AltJog(), 1.0f, 0.0f, 0.0f);
            DesenharRetangulo(xdojog2(), YdoJog2, LargJog(), AltJog(), 0.0f, 0.0f, 1.0f);


            SwapBuffers();
            
        }

        void DesenharRetangulo(int x, int y, int largura, int altura, float r, float g, float b)
        {
            GL.Color3(r, g, b);

            GL.Begin(Primitive.Type.Quads);
            GL.Vertex2(-0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, 0.5f * altura + y);
            GL.Vertex2(-0.5f * largura + x, 0.5f * altura + y);
            GL.End();
        }

        static void main()
        {

          new program().Run();  
        }
    }
}