using System.Drawing;

namespace SegmentadorImagem.Filtros.Filtros
{
    public class Gaussiano : Filtro
    {
        public Gaussiano(int tamanho) : base()
        {
            this.TamanhoMascara = tamanho;
        }

        public override Bitmap Aplicar(string arquivo)
        {
            return Processar(arquivo, TamanhoMascara);
        }
        private Bitmap Processar(string arquivo, int tamanhoMascara)
        {
            return base.Convolucao(arquivo, MascaraGaussiano(tamanhoMascara));
        }

        private static double[,] MascaraGaussiano(int tipoMascara)
        {
            double[,] mascara;

            if (tipoMascara == 3)
            {
                mascara = new double[3, 3];
                mascara[0, 0] = ((double)1 / 16);
                mascara[0, 1] = ((double)1 / 8);
                mascara[0, 2] = ((double)1 / 16);
                mascara[1, 0] = ((double)1 / 8);
                mascara[1, 1] = ((double)1 / 4);
                mascara[1, 2] = ((double)1 / 8);
                mascara[2, 0] = ((double)1 / 16);
                mascara[2, 1] = ((double)1 / 8);
                mascara[2, 2] = ((double)1 / 16);
            }
            else
            {
                mascara = new double[5, 5];
                mascara[0, 0] = ((double)1 / 273);
                mascara[0, 1] = ((double)4 / 273);
                mascara[0, 2] = ((double)7 / 273);
                mascara[0, 3] = ((double)4 / 273);
                mascara[0, 4] = ((double)1 / 273);
                mascara[1, 0] = ((double)4 / 273);
                mascara[1, 1] = ((double)16 / 273);
                mascara[1, 2] = ((double)26 / 273);
                mascara[1, 3] = ((double)16 / 273);
                mascara[1, 4] = ((double)4 / 273);
                mascara[2, 0] = ((double)7 / 273);
                mascara[2, 1] = ((double)26 / 273);
                mascara[2, 2] = ((double)41 / 273);
                mascara[2, 3] = ((double)26 / 273);
                mascara[2, 4] = ((double)7 / 273);
                mascara[3, 0] = ((double)4 / 273);
                mascara[3, 1] = ((double)16 / 273);
                mascara[3, 2] = ((double)26 / 273);
                mascara[3, 3] = ((double)16 / 273);
                mascara[3, 4] = ((double)4 / 273);
                mascara[4, 0] = ((double)1 / 273);
                mascara[4, 1] = ((double)4 / 273);
                mascara[4, 2] = ((double)7 / 273);
                mascara[4, 3] = ((double)4 / 273);
                mascara[4, 4] = ((double)1 / 273);
            }

            return mascara;
        }
    }
}
