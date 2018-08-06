using System;
using System.Drawing;

namespace SegmentadorImagem.Filtros.Filtros
{
    public class Prewitt : Filtro
    {
        public Prewitt(double[,] mascaraX, double[,] mascaraY) : base()
        {
            this.MascaraX = mascaraX;
            this.MascaraY = mascaraY;
        }

        public Prewitt() : base()
        {

        }

        public override Bitmap Aplicar(string arquivo)
        {
            if (MascaraX == null)
                MascaraX = MascaraPrewittX();

            if (MascaraY == null)
                MascaraY = MascaraPrewittY();

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
            int valorPonderadorPrewitt = 0;
            int valorGX = 0;
            int valorGY = 0;
            int limite = 128 * 128;

            //indice de maximo matricial
            int mascaraX = (int)Math.Floor((decimal)matrizMascaraX.GetLength(0) / 2);
            int mascaraY = (int)Math.Floor((decimal)matrizMascaraX.GetLength(1) / 2);

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
                        }
                    }
                    for (int s = (-1) * mascaraX; s <= (1) * mascaraX; s++)
                    {
                        for (int t = (-1) * mascaraY; t <= (1) * mascaraY; t++)
                        {
                            valorGY += (int)Math.Floor((matrizMascaraY[s + mascaraX, t + mascaraY] * bm.GetPixel(x + s, y + t).ToArgb()));
                        }
                    }
                    valorGX = (int)(valorGX / 64);
                    valorGY = (int)(valorGY / 64);
                    valorPonderadorPrewitt += (int)Math.Pow((int)((int)Math.Pow(valorGX, 2) + (int)Math.Pow(valorGY, 2)), 0.5);
                    if (valorPonderadorPrewitt > limite)
                    {
                        //color = Color.Transparent;
                        color = Color.FromArgb(valorPonderadorPrewitt);
                    }
                    else
                    {
                        color = Color.Black;
                    }
                    //color = Color.FromArgb(valorPonderadorSobel);
                    bmn.SetPixel(x, y, color);
                    valorPonderadorPrewitt = 0; valorGX = 0; valorGY = 0;
                }
            }

            return bmn;
        }
    
        private static double[,] MascaraPrewittY()
        {
            double[,] mascara;

            mascara = new double[3, 3];
            mascara[0, 0] = ((double)1 / 4) * ((double)-1);
            mascara[0, 1] = ((double)1 / 4) * ((double)-1);
            mascara[0, 2] = ((double)1 / 4) * ((double)-1);
            mascara[1, 0] = ((double)1 / 4) * ((double)0);
            mascara[1, 1] = ((double)1 / 4) * ((double)0);
            mascara[1, 2] = ((double)1 / 4) * ((double)0);
            mascara[2, 0] = ((double)1 / 4) * ((double)1);
            mascara[2, 1] = ((double)1 / 4) * ((double)1);
            mascara[2, 2] = ((double)1 / 4) * ((double)1);

            return mascara;
        }
        private static double[,] MascaraPrewittX()
        {
            double[,] mascara;

            mascara = new double[3, 3];
            mascara[0, 0] = ((double)1 / 4) * ((double)1);
            mascara[0, 1] = ((double)1 / 4) * ((double)0);
            mascara[0, 2] = ((double)1 / 4) * ((double)-1);
            mascara[1, 0] = ((double)1 / 4) * ((double)1);
            mascara[1, 1] = ((double)1 / 4) * ((double)0);
            mascara[1, 2] = ((double)1 / 4) * ((double)-1);
            mascara[2, 0] = ((double)1 / 4) * ((double)1);
            mascara[2, 1] = ((double)1 / 4) * ((double)0);
            mascara[2, 2] = ((double)1 / 4) * ((double)-1);

            return mascara;
        }
    }
}
