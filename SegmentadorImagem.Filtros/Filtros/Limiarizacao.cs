using System.Drawing;

namespace SegmentadorImagem.Filtros.Filtros
{
    public class Limiarizacao : Filtro
    {
        public override Bitmap Aplicar(string arquivo)
        {
            //calcular valor T
            float valorT = 0;
            float valorP = 0;

            //carregando a imagem
            Bitmap bm = new Bitmap(arquivo);

            //lendo a imagem na horizontal e marcando a linha
            for (int x = 0; x < bm.Width; x++)
            {
                for (int y = 0; y < bm.Height; y++)
                {
                    //acumulando peso dos pontos
                    valorP += bm.GetPixel(x, y).GetBrightness();
                }
            }

            //achando valorT
            valorT = valorP / (bm.Width * bm.Height);

            //novo bitmap
            Bitmap bmn = new Bitmap(bm); //new Bitmap(bm.Width,bm.Height);

            //definindor perfil
            Color color;

            //criando novo bitmap
            for (int x = 0; x < bm.Width; x++)
            {
                for (int y = 0; y < bm.Height; y++)
                {

                    if (bm.GetPixel(x, y).GetBrightness() > valorT)
                    {
                        color = Color.FromName("White");
                    }
                    else
                    {
                        color = Color.FromName("Black");
                    }
                    bmn.SetPixel(x, y, color);
                }
            }

            return bmn;
        }
    }
}
