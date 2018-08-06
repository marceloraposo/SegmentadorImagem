using System.Drawing;

namespace SegmentadorImagem.Filtros.Filtros
{
    public class Media : Filtro
    {
        public Media(int tamanho) : base()
        {
            this.TamanhoMascara = tamanho;
        }

        public override Bitmap Aplicar(string arquivo)
        {
            return Processar(arquivo, TamanhoMascara);
        }
        private Bitmap Processar(string arquivo, int tamanhoMascara)
        {
            return base.Convolucao(arquivo, MascaraMedia(3));
        }

        private static double[,] MascaraMedia(int tamanho)
        {
            if (tamanho % 2 == 0)
                tamanho++;

            double[,] mascara = new double[tamanho, tamanho];

            //montar matriz mascara
            for (int x = 0; x < tamanho; x++)
            {
                for (int y = 0; y < tamanho; y++)
                {
                    mascara[x, y] = ((double)1 / (tamanho * tamanho));
                }
            }

            return mascara;
        }
    }
}
