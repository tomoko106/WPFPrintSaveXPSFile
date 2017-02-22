using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Livet;
using Livet.Commands;
using Reactive;
using System.Windows.Documents;
using System.Windows.Markup;
using System.IO.Packaging;
using System.Windows.Xps.Packaging;

namespace WPFPrintSaveXPSFile
{
    class MainWindowViewModel : ViewModel
    {

        public ReactiveProperty<decimal> Amount { get; set; }
        public ReactiveProperty<string> Name { get; set; }


        // SelectedDocument
        private IDocumentPaginatorSource _fixedDocumentSequence;
        public IDocumentPaginatorSource FixedDocumentSequence
        {
            get { return _fixedDocumentSequence; }
            private set
            {
                if (_fixedDocumentSequence == value) return;

                _fixedDocumentSequence = value;
                RaisePropertyChanged("FixedDocumentSequence");
            }
        }


        public ReactiveProperty<IDocumentPaginatorSource> PagenatorSource { get; set; }



        

        public MainWindowViewModel()
        {
            Amount = new ReactiveProperty<decimal>();
            Name = new ReactiveProperty<string>();
            PagenatorSource = new ReactiveProperty<IDocumentPaginatorSource>();
             

        }

        /// <summary>
        /// Print Document
        /// </summary>
        public void Print()
        {


            Printer.Print(this);

        }


        /// <summary>
        /// Set up Document
        /// </summary>
        /// <remarks>
        /// if you use this class both @mainview & @reportcontrol for datacontext
        /// do not call this function at intitialize method.
        /// </remarks>
        public void SetDoc()
        {


            // Set the ReportTemplateContorl to fixedpage
            FixedDocument fixedDoc = new FixedDocument();
            // ReportCtrl report = new ReportCtrl();

            ReportCtrl myReport = new ReportCtrl();
            myReport.DataContext = this;

            PageContent pageContent = new PageContent();
            FixedPage fixedPage = new FixedPage();

            fixedPage.Children.Add(myReport);
            ((IAddChild)pageContent).AddChild(fixedPage);
            fixedDoc.Pages.Add(pageContent);


            // this.PagenatorSource = fixedDoc.DocumentPaginator.Source;
            this.FixedDocumentSequence = fixedDoc.DocumentPaginator.Source;
            
        }

    }
}
