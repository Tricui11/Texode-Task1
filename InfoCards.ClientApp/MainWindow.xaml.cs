using System;
using System.IO;
using System.Windows;
using System.Xml;
using InfoCards.ClientApp.ViewModels;
using Microsoft.Win32;

namespace InfoCards.ClientApp {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {

    private const string _filePath = "C:/Users/Furer/Downloads/11.xml";
    OpenFileDialog openFileDialog1 = new OpenFileDialog();

    public MainWindow(MainWindowViewModel mainWindowViewModel) {
      DataContext = mainWindowViewModel;
      InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e) {

    }

    private void F1_Button_Click(object sender, RoutedEventArgs e) {
      try {


        //using (XmlWriter writer = XmlWriter.Create(_filePath)) {
        //  writer.WriteStartElement("book");
        //  writer.WriteElementString("title", "Graphics Programming using GDI+");
        //  writer.WriteElementString("author", "Mahesh Chand");
        //  writer.WriteElementString("publisher", "Addison-Wesley");
        //  writer.WriteElementString("price", "64.95");
        //  writer.WriteEndElement();
        //  writer.Flush();
        //}


        XmlDocument myxmldoc = new XmlDocument();
        myxmldoc.Load(_filePath);
        XmlElement elem = myxmldoc.CreateElement("image");
        //  Open the picture file and use the picture to construct a file stream
        FileStream fs = new FileStream("C:/Users/Furer/Downloads/11.jpg", FileMode.Open);
        //  Use the file stream to construct a binary reader to read primitive data as binary values
        BinaryReader br = new BinaryReader(fs);
        byte[] imagebuffer = new byte[br.BaseStream.Length];
        br.Read(imagebuffer, 0, Convert.ToInt32(br.BaseStream.Length));
        string textstring = Convert.ToBase64String(imagebuffer);
        fs.Close();
        br.Close();
        XmlText text = myxmldoc.CreateTextNode(textstring);
        myxmldoc.DocumentElement.AppendChild(elem);
        myxmldoc.DocumentElement.LastChild.AppendChild(text);

        myxmldoc.Save(_filePath);
        MessageBox.Show("End of reading and writing!");
      }
      catch (Exception ex) {
        MessageBox.Show(ex.ToString());
      }
    }

    // открытие файла
    private void F3_Button_Click(object sender, RoutedEventArgs e) {
      if (openFileDialog1.ShowDialog() == false)
        return;
      // получаем выбранный файл
      string filename = openFileDialog1.FileName;

    }
  }
}
