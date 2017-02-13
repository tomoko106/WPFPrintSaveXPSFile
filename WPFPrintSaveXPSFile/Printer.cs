using System.Collections.Generic;
using System.Windows.Controls;
using System.Printing;
using System.Windows.Xps;
using System.Windows.Documents;
using System.Windows.Markup;
using System.IO.Packaging;
using System.Windows.Xps.Packaging;
using System;
using System.IO;

namespace WPFPrintSaveXPSFile
{
    class Printer
    {

        public static void Print(MainWindowViewModel vm)
        {

            // Set the ReportTemplateContorl to fixedpage
            FixedDocument fixedDoc = new FixedDocument();
            ReportCtrl report = new ReportCtrl();

            UserControl ReportToPrint = report as UserControl;
            ReportToPrint.DataContext = vm;

            PageContent pageContent = new PageContent();
            FixedPage fixedPage = new FixedPage();

            fixedPage.Children.Add(ReportToPrint);
            ((IAddChild)pageContent).AddChild(fixedPage);
            fixedDoc.Pages.Add(pageContent);

            // Print page
            if (!SendFixedDocumentToPrinter(fixedDoc))
            {
                return;
            }

            // Save fixeddocument as xps file
            string fileName = @"C:\temp\sample_report.xps";

            // Set up package
            Package package = Package.Open(fileName, System.IO.FileMode.Create);

            // Set up XpsDocument object 
            XpsDocument xpsDoc = new XpsDocument(package);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDoc);

            // write to xps file wpt fixed doc
            writer.Write(fixedDoc.DocumentPaginator);

            
            xpsDoc.Close();
            package.Close();

        }

        private static bool SendFixedDocumentToPrinter(FixedDocument fixedDocument)
        {

            XpsDocumentWriter xpsdw;

            PrintDocumentImageableArea imgArea = null;
            xpsdw = PrintQueue.CreateXpsDocumentWriter(ref imgArea);

            // cancel on printdialog
            if (xpsdw == null)
            {
                return false;
            }

            // out put to the printer
            xpsdw.Write(fixedDocument);

            return true;
        }

    }
}
