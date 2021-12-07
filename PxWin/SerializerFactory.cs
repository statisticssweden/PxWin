using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCAxis.Excel;
using PCAxis.Paxiom;
using PCAxis.Serializers.JsonStat;
using PX.Plugin.Interfaces.Attributes;
using System.Windows.Forms;

namespace PCAxis.Desktop
{
    public class SerializerFactory
    {
        //PX-file
        [Export]
        [SerializerMetadata(Id = "FileTypePX", Extension = "px")]
        public static IPXModelStreamSerializer CreatePxFileSerializer()
        {
            return new PXFileSerializer();
        }

        //Excel (*.xlsx)
        [Export]
        [SerializerMetadata(Id = "FileTypeExcelX", Extension = "xlsx")]
        public static IPXModelStreamSerializer CreateExcelX()
        {
            return new XlsxSerializer() { InformationLevel = InformationLevelType.AllInformation };
        }

        //Excel with code and text column (*.xlsx)
        [Export]
        [SerializerMetadata(Id = "FileTypeExcelXDoubleColumn", Extension = "xlsx")]
        public static IPXModelStreamSerializer CreateExcelXDoubleColumn()
        {
            return new XlsxSerializer() { InformationLevel = InformationLevelType.AllInformation, DoubleColumn = DoubleColumnType.AlwaysDoubleColumns };
        }

        //Tab delimited with heading
        [Export]
        [SerializerMetadata(Id = "FileTypeCsvWithHeadingAndTabulator", Extension = "csv")]
        public static IPXModelStreamSerializer CreateCsvTabWhithHeading()
        {
            return new CsvFileSerializer() { Delimiter =  (char)Keys.Tab, Title = true};
        }

        //Tab delimited without heading
        [Export]
        [SerializerMetadata(Id = "FileTypeCsvWithoutHeadingAndTabulator", Extension = "csv")]
        public static IPXModelStreamSerializer CreateCsvTabWithoutHeading()
        {
            return new CsvFileSerializer() { Delimiter = (char)Keys.Tab, Title = false };
        }

        //Comma delimited with heading
        [Export]
        [SerializerMetadata(Id = "FileTypeCsvWithHeadingAndComma", Extension = "csv")]
        public static IPXModelStreamSerializer CreateCsvCommaWithHeading()
        {
            return new CsvFileSerializer() { Delimiter = ',', Title = true };
        }

        //Comma delimited without heading
        [Export]
        [SerializerMetadata(Id = "FileTypeCsvWithoutHeadingAndComma", Extension = "csv")]
        public static IPXModelStreamSerializer CreateCsvCommaWithoutHeading()
        {
            return new CsvFileSerializer() { Delimiter = ',', Title = false };
        }

        //Space delimited with heading
        [Export]
        [SerializerMetadata(Id = "FileTypeCsvWithHeadingAndSpace", Extension = "csv")]
        public static IPXModelStreamSerializer CreateCsvSpaceWithHeading()
        {
            return new CsvFileSerializer() { Delimiter = ' ', Title = true };
        }

        //Space delimited without heading
        [Export]
        [SerializerMetadata(Id = "FileTypeCsvWithoutHeadingAndSpace", Extension = "csv")]
        public static IPXModelStreamSerializer CreateCsvSpaceWithoutHeading()
        {
            return new CsvFileSerializer() { Delimiter = ' ', Title = false };
        }

        //Semicolon delimited with heading
        [Export]
        [SerializerMetadata(Id = "FileTypeCsvWithHeadingAndSemiColon", Extension = "csv")]
        public static IPXModelStreamSerializer CreateCsvSemiColonWithHeading()
        {
            return new CsvFileSerializer() { Delimiter = ';', Title = true };
        }

        //Semicolon delimited without heading
        [Export]
        [SerializerMetadata(Id = "FileTypeCsvWithoutHeadingAndSemiColon", Extension = "csv")]
        public static IPXModelStreamSerializer CreateCsvSemiColonWithoutHeading()
        {
            return new CsvFileSerializer() { Delimiter = ';', Title = false };
        }

        //Semicolon delimited without heading
        [Export]
        [SerializerMetadata(Id = "Filetypejsonstat", Extension = "json")]
        public static IPXModelStreamSerializer CreateJsonstat()
        {
            return new JsonStatSerializer(); { };
        }

        //Excel workbook (*.xml)
        [Export]
        [SerializerMetadata(Id = "FileTypeExcel", Extension = "xml")]
        public static IPXModelStreamSerializer CreateExcelWorkbook()
        {
            return new ExcelFileSerializer() { InformationLevel = InformationLevelType.AllInformation };
        }

        //Excel workbook with code and text (*.xml)
        [Export]
        [SerializerMetadata(Id = "FileTypeExcelDoubleColumn", Extension = "xml")]
        public static IPXModelStreamSerializer CreateExcelWorkbookDoubleColumn()
        {
            return new ExcelFileSerializer() { InformationLevel = InformationLevelType.AllInformation, DoubleColumn = DoubleColumnType.AlwaysDoubleColumns };
        }

        //Html-file
        [Export]
        [SerializerMetadata(Id = "FileTypeHtml", Extension = "html")]
        public static IPXModelStreamSerializer CreateHtml()
        {
            return new HtmlFileSerializer();
        }

        //Relational file
        [Export]
        [SerializerMetadata(Id = "FileTypeRelational", Extension = "txt")]
        public static IPXModelStreamSerializer CreateRelationFile()
        {
            return new RelationtableFileSerializer();
        }
    }
}
