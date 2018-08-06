using System;
using System.Drawing;

namespace SegmentadorImagem.Filtros.Filtros
{
    public class Laplaciano : Filtro
    {
        public Laplaciano(double[,] mascara) : base()
        {
            this.Mascara = mascara;
        }

        public Laplaciano() : base()
        {
        }

        public override Bitmap Aplicar(string arquivo)
        {
            if (Mascara == null)
                Mascara = MascaraLaplaciano();

            return Processar(arquivo, Mascara);
        }
        private Bitmap Processar(string arquivo, double[,] mascara)
        {
            //carregando a imagem
            Bitmap bm = new Bitmap(arquivo);

            //novo bitmap
            Bitmap bmn = new Bitmap(bm);

            //definindo a cor
            Color color;

            //posição da mediana
            int valorMediaPonderada = 0;
            int valorPonderador = 0;

            //base para calculo
            int[,] mascaraCalculo = new int[mascara.GetLength(0), Mascara.GetLength(1)];

            //indice de maximo matricial
            int mascaraX = (int)Math.Floor((decimal)mascara.GetLength(0) / 2);
            int mascaraY = (int)Math.Floor((decimal)mascara.GetLength(1) / 2);

            //criando novo bitmap
            for (int x = mascaraX; x < bm.Width - (mascaraX + 1); x++)
            {
                for (int y = mascaraY; y < bm.Height - (mascaraY + 1); y++)
                {
                    for (int s = (-1) * mascaraX; s <= (1) * mascaraX; s++)
                    {
                        for (int t = (-1) * mascaraY; t <= (1) * mascaraY; t++)
                        {
                            mascaraCalculo[s + mascaraX, t + mascaraY] = (int)Math.Floor((mascara[s + mascaraX, t + mascaraY] * bm.GetPixel(x - s, y - t).ToArgb()));
                            valorPonderador += bm.GetPixel(x - s, y - t).ToArgb();
                            //Console.WriteLine(string.Format("({0},{1}) => {2}",(s + mascaraX),(t + mascaraY), mascara[s + mascaraX, t + mascaraY]));
                        }
                    }
                    valorMediaPonderada = (int)Math.Pow(MascaraMediaPonderada(mascaraCalculo, valorPonderador), 2);
                    if (valorMediaPonderada < valorPonderador)
                    {
                        //color = Color.Transparent;
                        color = Color.FromArgb(valorMediaPonderada);
                    }
                    else
                    {
                        color = Color.Black;
                    }
                    //color = Color.FromArgb(valorMediaPonderada);
                    bmn.SetPixel(x, y, color);
                    valorPonderador = 0;
                }
            }

            return bmn;
        }

        private static int MascaraMediaPonderada(int[,] mascaraChegada, int valorPonderador)
        {
            int valorMediaPonderada = 0;
            int[,] mascara = (int[,])mascaraChegada.Clone();
            int tamanho = mascara.GetLength(0);

            if (tamanho % 2 == 0)
                tamanho++;

            //montar matriz mascara
            for (int x = 0; x < tamanho; x++)
            {
                for (int y = 0; y < tamanho; y++)
                {
                    valorMediaPonderada += mascara[x, y];
                }
            }

            valorMediaPonderada = valorMediaPonderada / (tamanho * tamanho);//((-1)*valorPonderador);

            return valorMediaPonderada;
        }
        private static double[,] MascaraLaplaciano()
        {
            double[,] mascara;

            mascara = new double[3, 3];
            mascara[0, 0] = (1) * ((double)0);
            mascara[0, 1] = (1) * ((double)-1);
            mascara[0, 2] = (1) * ((double)0);
            mascara[1, 0] = (1) * ((double)-1);
            mascara[1, 1] = (1) * ((double)4);
            mascara[1, 2] = (1) * ((double)-1);
            mascara[2, 0] = (1) * ((double)0);
            mascara[2, 1] = (1) * ((double)-1);
            mascara[2, 2] = (1) * ((double)0);

            return mascara;
        }
    }
}
