using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace _360eval2pdf
{
  class Program
  {
    public static string dir = "./results";
    public static string image = "./yawn.jpg";
    public static string[] _list = new string[] { "1", "2", "3", "4", "5", "T" };
    public static string csvFile = "./NCHS_-_Leading_Causes_of_Death__United_States.csv";

    static void Main(string[] args)
    {

      Mkdir();
      SimpleWrite();
      ListWrite();
      ImageWrite();
      //WriteCSV();
    }
    /// <summary> method <c>Mkdir</c> checks for directory within
    /// current repository. If it does not exist, it is created
    /// </summary>
    public static void Mkdir()
    {
      try
      {
        if (Directory.Exists(dir))
        {
          Console.WriteLine("Directory already exists");
          return;
        }
        // create directory
        DirectoryInfo d = Directory.CreateDirectory(dir);
        Console.WriteLine("Directory was created successfully.");

      }
      catch (Exception e)
      {
        Console.WriteLine("Failed: {0}", e.ToString());
      }
    }
    /// <summary> method <c>SimpleWrite</c> writes a paragraph
    /// to a pdf document
    /// </summary>
    public static void SimpleWrite()
    {
      string dest = dir + "/hello_world.pdf";
      if (!File.Exists(dest))
      {
        PdfWriter writer = new PdfWriter(dest);
        PdfDocument pdf = new PdfDocument(writer);
        Document document = new Document(pdf);
        document.Add(new Paragraph("Hello World!"));
        document.Close();
      }
      else
      {
        Console.WriteLine(dest + " exists");
        return;
        // ask to delete and rewrite with new objs
      }
      Console.WriteLine("Simple write successful");
    }
    /// <summary> method <c>ListWrite</c> writes a list passed in
    /// to a pdf document as list items. Alternate font is also featured
    /// <param name="list">the list to be looped through</param>
    /// </summary>
    public static void ListWrite()
    {
      // pass in list, loop through write items
      string dest = dir + "/list.pdf";
      if (!File.Exists(dest))
      {
        PdfWriter writer = new PdfWriter(dest);
        PdfDocument pdf = new PdfDocument(writer);
        Document document = new Document(pdf);
        PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
        document.Add(new Paragraph("List: ").SetFont(font));
        List list = new List() // PDF list object
          .SetFont(font)
          .SetListSymbol("\u2022")
          .SetSymbolIndent(12);
        foreach (string listItem in _list)
        {
          list.Add(new ListItem(listItem));
        }
        document.Add(list);
        document.Close();
      }
      else
      {
        Console.WriteLine(dest + " exists");
        return;
      }
      Console.WriteLine("List write successful");
    }
    /// <summary>method <c>ImageWrite</c> demonstrates adding
    /// an image from the local directory to the pdf document using the 
    /// Image object from iText</summary>
    public static void ImageWrite()
    {
      string dest = dir + "/image.pdf";
      if (!File.Exists(dest))
      {
        PdfWriter writer = new PdfWriter(dest);
        PdfDocument pdf = new PdfDocument(writer);
        Document document = new Document(pdf);

        Image yawn = new Image(ImageDataFactory.Create(image));
        yawn.ScaleToFit(400, 400); // should not handle watermarks, logos (etc.) in this way, should scale prior to compiling in alternate software to save processing time.
        Paragraph paragraph = new Paragraph("Yawn...").Add(yawn);
        document.Add(paragraph);
        document.Close();
      }
      else
      {
        Console.WriteLine(dest + " exists");
        return;
      }
      Console.WriteLine("Image write successful");
    }
    ///
    ///
    ///
    public static void WriteCSV()
    {
      string dest = dir + "/csv.pdf";
      if (!File.Exists(dest))
      {
        // PdfWriter writer = new PdfWriter(dest);
        // PdfDocument pdf = new PdfDocument(writer);
        // Document document = new Document(pdf, PageSize.A4.Rotate());
        // document.SetMargins(20, 20, 20, 20);

        // PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
        // PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

        // Table table = new Table(new float[] { 4, 1, 3, 4, 3, 3, 3, 3, 1 });
        // table.SetWidth(UnitValue.CreatePercentValue(100));

        List<string> _list = LoadCSVFile(csvFile);

      }
      else
      {
        Console.WriteLine(dest + " exists");
        return;
      }
      Console.WriteLine("CSV write successful");
    }
    ///
    ///
    ///
    public static List<string> LoadCSVFile(string csvPath)
    {
      var reader = new StreamReader(File.OpenRead(csvPath));
      List<string> list = new List<string>();

      while (!reader.EndOfStream)
      {
        var line = reader.ReadLine();
        list.Add(line);
      }

      return list;
    }
    ///
    ///
    ///
    public static CauseOfDeath[] SplitCSVList(List<string> list)
    {
      CauseOfDeath[] causeOfDeaths = new CauseOfDeath[] { };
      return causeOfDeaths;
    }
  }
}
