using System.Drawing;

namespace SegmentadorImagem.Filtros.Filtros
{
    public class Vizinhanca : Filtro
    {
        public override Bitmap Aplicar(string arquivo)
        {
            //carregando a imagem
            Bitmap bm = new Bitmap(arquivo);

            //novo bitmap
            Bitmap bmr = new Bitmap(bm);

            //lendo a imagem na horizontal e marcando a linha
            for (int x = 0; x < bm.Width; x++)
            {
                for (int y = 0; y < bm.Height; y++)
                {
                    bmr = ProcessaVizinhanca(bm, x, y, bmr);
                }
            }

            return bmr;
        }
        private Bitmap ProcessaVizinhanca(Bitmap bme, int x, int y, Bitmap bmr)
        {
            Color color;
            //Bitmap bmr = bme;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (((x + i) > 0 && (x + i) < bme.Width) && ((y + j) > 0 && (y + j) < bme.Height) && ((i) != 0 && (j) != 0))
                    {
                        if (bme.GetPixel(x, y).GetBrightness() <= bme.GetPixel(x + i, y + j).GetBrightness() + LIMITE && bme.GetPixel(x, y).GetBrightness() >= bme.GetPixel(x + i, y + j).GetBrightness() - LIMITE)
                        {
                            color = Color.FromName("White");
                        }
                        else
                        {
                            color = Color.FromName("Black");
                        }
                        bmr.SetPixel(x + i, y + j, color);
                    }
                    else
                    {
                        color = Color.FromName("Black");
                        bmr.SetPixel(x, y, color);
                    }
                }
            }
            return bmr;
        }
    }
}
