using SegmentadorImagem.Controles;
using System;
using System.Drawing;

namespace SegmentadorImagem.Filtros
{
    public abstract class Filtro
    {
        #region atributos
        private TipoFiltro tipoFiltro;
        public const float LIMITE = (float)0.1;
        public const float DESVIO = (float)1;
        private int tamanhoMascara;
        private double[,] mascara;
        private double[,] mascaraX;
        private double[,] mascaraY;
        #endregion

        #region campos
        public TipoFiltro TipoFiltro
        {
            get { return tipoFiltro; }
            set { tipoFiltro = value; }
        }
        public int TamanhoMascara
        {
            get;
            set;
        }
        public double[,] Mascara
        {
            get;
            set;
        }
        public double[,] MascaraX
        {
            get;
            set;
        }
        public double[,] MascaraY
        {
            get;
            set;
        }
        #endregion

        public abstract Bitmap Aplicar(string arquivo);

        public Bitmap Convolucao(string arquivo, double[,] mascara)
        {

            //carregando a imagem
            Bitmap bm = new Bitmap(arquivo);

            //https://webserver2.tecgraf.puc-rio.br/~mgattass/fcg/trb13/RaquelGuilhon/trabalho1.html
            //http://www2.ic.uff.br/~aconci/G-LoG.PDF
            //https://www.ime.usp.br/~reverbel/mac115-IF-08/eps/ep4.pdf
            //http://www.inf.ufrgs.br/~cwaraujo/inf01046/laboratorio1.html
            //http://cis.k.hosei.ac.jp/~wakahara/sobel.c
            //http://www.dpi.inpe.br/~carlos/Academicos/Cursos/Pdi/pdi_filtros.htm
            //https://www.codeproject.com/Articles/93642/Canny-Edge-Detection-in-C
            //https://github.com/PriscylaSantos/FiltrosDeImagem
            //https://www.ppgia.pucpr.br/~facon/ComputerVisionBooks/2009ProcessamentoImagensComJava.pdf

            //novo bitmap
            Bitmap bmn = new Bitmap(bm);

            //definindo a cor
            Color color;
            int A, R, G, B;

            //indice de maximo matricial
            int mascaraX = (int)Math.Floor((decimal)mascara.GetLength(0) / 2);
            int mascaraY = (int)Math.Floor((decimal)mascara.GetLength(1) / 2);

            //criando novo bitmap
            for (int x = mascaraX; x < bm.Width - (mascaraX + 1); x++)
            {
                for (int y = mascaraY; y < bm.Height - (mascaraY + 1); y++)
                {
                    A = 0;
                    R = 0;
                    G = 0;
                    B = 0;
                    for (int s = (-1) * mascaraX; s <= (1) * mascaraX; s++)
                    {
                        for (int t = (-1) * mascaraY; t <= (1) * mascaraY; t++)
                        {
                            A += (int)Math.Floor((mascara[s + mascaraX, t + mascaraY] * bm.GetPixel(x - s, y - t).A));
                            R += (int)Math.Floor((mascara[s + mascaraX, t + mascaraY] * bm.GetPixel(x - s, y - t).R));
                            G += (int)Math.Floor((mascara[s + mascaraX, t + mascaraY] * bm.GetPixel(x - s, y - t).G));
                            B += (int)Math.Floor((mascara[s + mascaraX, t + mascaraY] * bm.GetPixel(x - s, y - t).B));
                            color = Color.FromArgb(A, R, G, B);
                            bmn.SetPixel(x, y, color);
                        }
                    }
                }
            }

            return bmn;
        }
    }
}
