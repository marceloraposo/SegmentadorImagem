using System;
using System.Drawing;

namespace SegmentadorImagem.Filtros.Filtros
{
    public class Canny : Filtro
    {
        public Canny(double[,] mascara) : base()
        {
            this.Mascara = mascara;
        }

        public Canny() : base()
        {

        }

        public override Bitmap Aplicar(string arquivo)
        {
            if (Mascara == null)
                Mascara = MascaraCanny();

            return Processar(arquivo,Mascara);
        }
        private Bitmap Processar(string arquivo, double[,] matrizMascara)
        {
            //carregando a imagem
            Bitmap bm = new Bitmap(arquivo);

            //novo bitmap
            Bitmap bmn = new Bitmap(bm);

            //definindo a cor
            Color color;

            //posição da mediana
            int valorPonderador = 0;
            int limite = 64 * 256;

            //indice de maximo matricial
            int mascaraX = (int)Math.Floor((decimal)matrizMascara.GetLength(0) / 2);
            int mascaraY = (int)Math.Floor((decimal)matrizMascara.GetLength(1) / 2);

            //criando novo bitmap
            for (int x = mascaraX; x < bm.Width - (mascaraX + 1); x++)
            {
                for (int y = mascaraY; y < bm.Height - (mascaraY + 1); y++)
                {
                    for (int s = (-1) * mascaraX; s <= (1) * mascaraX; s++)
                    {
                        for (int t = (-1) * mascaraY; t <= (1) * mascaraY; t++)
                        {
                            valorPonderador += (int)Math.Floor((matrizMascara[s + mascaraX, t + mascaraY] * bm.GetPixel(x + s, y + t).ToArgb()));
                        }
                    }
                    valorPonderador = (int)(valorPonderador / 256);
                    // color = Color.FromArgb(valorPonderador);
                    if (valorPonderador > limite)
                    {
                        color = Color.FromArgb(valorPonderador);
                    }
                    else
                    {
                        color = Color.Black;
                    }
                    bmn.SetPixel(x, y, color);
                    valorPonderador = 0;
                }
            }

            return bmn;
        }
    
        private static double[,] MascaraCanny()
        {
            double[,] mascara;

            mascara = new double[3, 3];
            mascara[0, 0] = ((double)1 / 1) * ((double)0);
            mascara[0, 1] = ((double)1 / 1) * ((double)1);
            mascara[0, 2] = ((double)1 / 1) * ((double)0);
            mascara[1, 0] = ((double)1 / 1) * ((double)1);
            mascara[1, 1] = ((double)1 / 1) * ((double)-4);
            mascara[1, 2] = ((double)1 / 1) * ((double)1);
            mascara[2, 0] = ((double)1 / 1) * ((double)0);
            mascara[2, 1] = ((double)1 / 1) * ((double)1);
            mascara[2, 2] = ((double)1 / 1) * ((double)0);

            return mascara;
        }
    }
}
