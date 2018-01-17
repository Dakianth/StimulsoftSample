using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Stimulsoft.Report;
using Stimulsoft.Report.Export;

namespace StimulsoftSample.XmlExportSample
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            StiOptions.Export.Xml.ConvertTagsToUpperCase = false;
            StiOptions.Export.Xml.UseAliases = true;

            var reportPath = "Reports\\Report.mrt";
            StiReport report = new StiReport();
            report.Load(reportPath);

            var dataPath = "Data\\Demo";
            var data = new DataSet("Demo");
            data.ReadXmlSchema($"{dataPath}.xsd");
            data.ReadXml($"{dataPath}.xml");
            report.RegData(data);

            //report.Design();
            report.Render(false);

            var pathTemp = "Temp\\generated.xml";
            string tableName = (string)report["TableName"];
            var settings = new StiXmlExportSettings();
            settings.TableName = tableName;
            report.ExportDocument(StiExportFormat.Xml, pathTemp, settings);

            Console.WriteLine($"TableName: {tableName}");

            Process.Start("Temp\\expected.xml");
            Thread.Sleep(1500); // increase time if the second xml is not shown
            Process.Start(pathTemp);

            Console.WriteLine("Precione qualquer tecla para encerrar...");
            Console.ReadKey();
        }
    }
}