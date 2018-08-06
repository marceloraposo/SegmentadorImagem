using System;
using System.Drawing;

namespace SegmentadorImagem.Filtros.Filtros
{
    public class Sobel : Filtro
    {
        public Sobel(double[,] mascaraX, double[,] mascaraY) : base()
        {
            this.MascaraX = mascaraX;
            this.MascaraY = mascaraY;
        }

        public Sobel() : base()
        {
        }

        public override Bitmap Aplicar(string arquivo)
        {
            if (MascaraX == null)
                MascaraX = MascaraSobelX();

            if (MascaraY == null)
                MascaraY = MascaraSobelY();

            return Processar(arquivo,MascaraX, MascaraY);
        }
        private Bitmap Processar(string arquivo, double[,] matrizMascaraX, double[,] matrizMascaraY)
        {
            //carregando a imagem
            Bitmap bm = new Bitmap(arquivo);

            //novo bitmap
            Bitmap bmn = new Bitmap(bm);

            //definindo a cor
            Color color;

            //posição da mediana
            int valorPonderadorSobel = 0;
            int valorGX = 0;
            int valorGY = 0;
            int limite = 128 * 128; //0 até 255

            //indice de maximo matricial
            int mascaraX = (int)Math.Floor((decimal)matrizMascaraX.GetLength(0) / 2);
            int mascaraY = (int)Math.Floor((decimal)matrizMascaraY.GetLength(1) / 2);

            //criando novo bitmap
            for (int x = mascaraX; x < bm.Width - (mascaraX + 1); x++)
            {
                for (int y = mascaraY; y < bm.Height - (mascaraY + 1); y++)
                {
                    for (int s = (-1) * mascaraX; s <= (1) * mascaraX; s++)
                    {
                        for (int t = (-1) * mascaraY; t <= (1) * mascaraY; t++)
                        {
                            valorGX += (int)Math.Floor((matrizMascaraX[s + mascaraX, t + mascaraY] * bm.GetPixel(x + s, y + t).ToArgb()));
                            //Console.WriteLine(string.Format("({0}) ==> ({1},{2})", matrizMascaraX[s + mascaraX, t + mascaraY], x - s, y - t));
                        }
                    }
                    for (int s = (-1) * mascaraX; s <= (1) * mascaraX; s++)
                    {
                        for (int t = (-1) * mascaraY; t <= (1) * mascaraY; t++)
                        {
                            valorGY += (int)Math.Floor((matrizMascaraY[s + mascaraX, t + mascaraY] * bm.GetPixel(x + s, y + t).ToArgb()));
                            //Console.WriteLine(string.Format("({0}) ==> ({1},{2})", matrizMascaraY[s + mascaraX, t + mascaraY], x - s, y - t));
                        }
                    }
                    valorGX = (int)(valorGX / 64);
                    valorGY = (int)(valorGY / 64);
                    valorPonderadorSobel += (int)Math.Pow((int)((int)Math.Pow(valorGX, 2) + (int)Math.Pow(valorGY, 2)), 0.5);
                    if (valorPonderadorSobel >= limite)
                    {
                        //color = Color.Transparent;
                        color = Color.FromArgb(valorPonderadorSobel);
                    }
                    else
                    {
                        color = Color.Black;
                    }
                    //color = Color.FromArgb(valorPonderadorSobel);
                    bmn.SetPixel(x, y, color);
                    valorPonderadorSobel = 0; valorGX = 0; valorGY = 0;
                }
            }

            return bmn;
        }

        private static double[,] MascaraSobelY()
        {
            double[,] mascara;

            mascara = new double[3, 3];
            mascara[0, 0] = ((double)1 / 4) * ((double)-1);
            mascara[0, 1] = ((double)1 / 4) * ((double)-2);
            mascara[0, 2] = ((double)1 / 4) * ((double)-1);
            mascara[1, 0] = ((double)1 / 4) * ((double)0);
            mascara[1, 1] = ((double)1 / 4) * ((double)0);
            mascara[1, 2] = ((double)1 / 4) * ((double)0);
            mascara[2, 0] = ((double)1 / 4) * ((double)1);
            mascara[2, 1] = ((double)1 / 4) * ((double)2);
            mascara[2, 2] = ((double)1 / 4) * ((double)1);

            return mascara;
        }
        private static double[,] MascaraSobelX()
        {
            double[,] mascara;

            mascara = new double[3, 3];
            mascara[0, 0] = ((double)1 / 4) * ((double)1);
            mascara[0, 1] = ((double)1 / 4) * ((double)0);
            mascara[0, 2] = ((double)1 / 4) * ((double)-1);
            mascara[1, 0] = ((double)1 / 4) * ((double)2);
            mascara[1, 1] = ((double)1 / 4) * ((double)0);
            mascara[1, 2] = ((double)1 / 4) * ((double)-2);
            mascara[2, 0] = ((double)1 / 4) * ((double)1);
            mascara[2, 1] = ((double)1 / 4) * ((double)0);
            mascara[2, 2] = ((double)1 / 4) * ((double)-1);

            return mascara;
        }
    }
}
