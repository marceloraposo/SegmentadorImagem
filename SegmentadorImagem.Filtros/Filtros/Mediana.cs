using System;
using System.Drawing;

namespace SegmentadorImagem.Filtros.Filtros
{
    public class Mediana : Filtro
    {
        public Mediana(int tamanho) : base()
        {
            this.TamanhoMascara = tamanho;
        }

        public override Bitmap Aplicar(string arquivo)
        {
            return Processar(arquivo, TamanhoMascara);
        }
        private Bitmap Processar(string arquivo, int tamanhoMascara)
        {
            //carregando a imagem
            Bitmap bm = new Bitmap(arquivo);

            //novo bitmap
            Bitmap bmn = new Bitmap(bm);

            //definindo a cor
            Color color;

            //array mascara
            string posicaoMediana = string.Empty;
            int[,] mascara = new int[tamanhoMascara, tamanhoMascara];
            //indice de maximo matricial
            int mascaraX = (int)Math.Floor((decimal)tamanhoMascara / 2);
            int mascaraY = (int)Math.Floor((decimal)tamanhoMascara / 2);

            //criando novo bitmap
            for (int x = mascaraX; x < bm.Width - (mascaraX + 1); x++)
            {
                for (int y = mascaraY; y < bm.Height - (mascaraY + 1); y++)
                {
                    for (int s = (-1) * mascaraX; s <= (1) * mascaraX; s++)
                    {
                        for (int t = (-1) * mascaraY; t <= (1) * mascaraY; t++)
                        {
                            mascara[s + mascaraX, t + mascaraY] = bm.GetPixel(x + s, y + t).ToArgb();
                        }
                    }
                    posicaoMediana = MascaraMediana(mascara);
                    color = Color.FromArgb(mascara[Convert.ToInt32(posicaoMediana.Split(',')[0]), Convert.ToInt32(posicaoMediana.Split(',')[1])]);
                    bmn.SetPixel(x, y, color);
                }
            }

            return bmn;
        }

        private static string MascaraMediana(int[,] mascaraChegada)
        {
            int[,] mascara = (int[,])mascaraChegada.Clone();
            int tamanho = mascara.GetLength(0);

            if (tamanho % 2 == 0)
                tamanho++;

            bool achouMediana = false;
            int indiceApoio = 0;
            int[] apoio = new int[tamanho * tamanho];
            int[,] original = new int[tamanho, tamanho];
            int medianoX = (int)Math.Floor((decimal)mascara.GetLength(0) / 2);
            int medianoY = (int)Math.Floor((decimal)mascara.GetLength(1) / 2);
            int posicaoX = 0, posicaoY = 0;

            //montar matriz mascara
            for (int x = 0; x < tamanho; x++)
            {
                for (int y = 0; y < tamanho; y++)
                {
                    //mascara[x, y] = randomize.Next(0,9);
                    apoio[indiceApoio] = mascara[x, y];
                    indiceApoio++;
                }
            }

            original = (int[,])mascara.Clone();
            Array.Sort(apoio);
            indiceApoio = 0;
            for (int x = 0; x < tamanho; x++)
            {
                for (int y = 0; y < tamanho; y++)
                {
                    mascara[x, y] = apoio[indiceApoio];
                    indiceApoio++;
                }
            }

            //escolhendo o valor mediano
            for (int x = 0; x < tamanho; x++)
            {
                if (achouMediana)
                    break;
                for (int y = 0; y < tamanho; y++)
                {
                    if (original[x, y] == mascara[medianoX, medianoY])
                    {
                        posicaoX = x;
                        posicaoY = y;
                        achouMediana = true;
                        break;
                    }
                }
            }


            return string.Format("{0},{1}", posicaoX, posicaoY);
        }
    }
}
