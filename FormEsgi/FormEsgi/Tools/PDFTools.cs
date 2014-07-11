using FormEsgi.Data;
using FormEsgi.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FormEsgi.Tools
{
    public class PDFTools
    {
        public static void generatePDF(Form form, Dictionary<string, int> dict)
        {
            iTextSharp.text.Document doc1 = null;
            try
            {
                // Initialize the PDF document 
                doc1 = new Document();
                PdfWriter.GetInstance(doc1, new FileStream("C:\\Projets\\StatistiquesFormulaire.pdf", FileMode.Create));
                doc1.Open();

                // Récupération du nombre de réponses apportées au formulaire
                int numberAnswers = InterventionData.getNumberInterventionByForm(form);

                // Paragraphe du pdf
                string paragraph = "Le "+form.title+" a généré un total de "
                    +numberAnswers+"  interventions depuis sa mise en ligne le " + form.date_creation + ".";
                doc1.Add(new Paragraph(paragraph));
                // Ajouter une image
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance("C:\\Projets\\chartImage.jpeg");
                //Ajouter Image au fichier pdf
                doc1.Add(img);
                // fermer le document
                doc1.Close();

                // Ajouter les informations dans le pdf

                // Tableau 
                PdfPTable table = new PdfPTable(3);
                PdfPCell cell = new PdfPCell(new Phrase(form.title));
                cell.Colspan = 3;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(cell);
                table.AddCell("Col 1 Row 1");
                table.AddCell("Col 2 Row 1");
                table.AddCell("Col 3 Row 1");
                table.AddCell("Col 1 Row 2");
                table.AddCell("Col 2 Row 2");
                table.AddCell("Col 3 Row 2");
                doc1.Add(table);
                
            }
            catch (iTextSharp.text.DocumentException dex)
            {
                // Handle iTextSharp errors 
                System.Diagnostics.Debug.WriteLine("Error Itext : " + dex);
            }
            finally
            {
                // Clean up 
                doc1.Close();
                doc1 = null;
            }
        }
    }


}