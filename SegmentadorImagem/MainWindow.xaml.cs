using Microsoft.Win32;
using SegmentadorImagem.Controles;
using SegmentadorImagem.Filtros.Filtros;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SegmentadorImagem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region atributos
        private string arquivo;
        private TipoFiltro tipoFiltroSelecionado;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #region campos
        public string Arquivo
        {
            get { return arquivo; }
            set { arquivo = value; }
        }
        public TipoFiltro TipoFiltroSelecionado
        {
            get { return tipoFiltroSelecionado; }
            set { tipoFiltroSelecionado = value; }
        }
        #endregion

        #region metodos

        private void btnProcurar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files|*.png";
            openFileDialog1.Title = "Selecione uma Imagem";

            var result = openFileDialog1.ShowDialog();

            if (result.HasValue && result.Value)
            {
                //seleciona arquivo
                this.Arquivo = openFileDialog1.FileName;

                //colocar a imagem no panel
                this.ImagePanel.Source = new BitmapImage(new Uri(this.Arquivo));
            }
        }

        private void btnProcessar_Click(object sender, RoutedEventArgs e)
        {
            this.ImagePanelBase.Source = ProcessarQuadro(this.Arquivo, this.TipoFiltroSelecionado);
        }

        private System.Windows.Media.ImageSource ProcessarQuadro(string arquivo, TipoFiltro tipoFiltro)
        {
            System.Windows.Media.ImageSource imageSource = new BitmapImage();
            if (!string.IsNullOrEmpty(arquivo))
            {
                if (tipoFiltro == TipoFiltro.Limiarizacao)
                {
                    imageSource = ToBitmapImage(new Limiarizacao().Aplicar(arquivo));
                }
                else if (tipoFiltro == TipoFiltro.Vizinhanca)
                {
                    imageSource = ToBitmapImage(new Vizinhanca().Aplicar(arquivo));
                }
                else if (tipoFiltro == TipoFiltro.Gaussiano)
                {
                    imageSource = ToBitmapImage(new Gaussiano(3).Aplicar(arquivo));
                }
                else if (tipoFiltro == TipoFiltro.Media)
                {
                    imageSource = ToBitmapImage(new Media(3).Aplicar(arquivo));
                }
                else if (tipoFiltro == TipoFiltro.Mediana)
                {
                    imageSource = ToBitmapImage(new Mediana(7).Aplicar(arquivo));
                }
                else if (tipoFiltro == TipoFiltro.Laplaciano)
                {
                    imageSource = ToBitmapImage(new Laplaciano().Aplicar(arquivo));
                }
                else if (tipoFiltro == TipoFiltro.Sobel)
                {
                    imageSource = ToBitmapImage(new Sobel().Aplicar(arquivo));
                }
                else if (tipoFiltro == TipoFiltro.Prewitt)
                {
                    imageSource = ToBitmapImage(new Prewitt().Aplicar(arquivo));
                }
                else if (tipoFiltro == TipoFiltro.Canny)
                {
                    imageSource = ToBitmapImage(new Canny().Aplicar(arquivo));
                }

                }
            return imageSource;
        }

        public BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        #endregion
    }
}
