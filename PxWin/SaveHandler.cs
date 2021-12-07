using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCAxis.Desktop.Serializers;
using PCAxis.Excel;
using PCAxis.Paxiom;

namespace PCAxis.Desktop
{
    class SaveHandler
    {
        public static IPXModelStreamSerializer GetSerializer(int format)
        {
            IPXModelStreamSerializer serializer;
            switch (format)
            {
                case 1: //PX file
                    serializer = new PXFileSerializer();
                    break;
                case 2: //Excel workbook
                case 3:
                    var exSer = new ExcelFileSerializer { InformationLevel = InformationLevelType.AllInformation };
                    if (format == 3)
                    {
                        exSer.DoubleColumn = DoubleColumnType.AlwaysDoubleColumns;
                    }
                    serializer = exSer;
                    break;
                case 4: //CSV
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    var csvSer = new CsvFileSerializer();
                    //Set title for even numbers
                    csvSer.Title = format % 2 == 0;

                    //Tab
                    if (format == 4 || format == 5)
                    {
                        csvSer.Delimiter = (char)Keys.Tab;
                    }
                    //Comma
                    else if (format == 6 || format == 7)
                    {
                        csvSer.Delimiter = ',';
                    }
                    //Space
                    else if (format == 8 || format == 9)
                    {
                        csvSer.Delimiter = ' ';
                    }
                    //Semicolon
                    else
                    {
                        csvSer.Delimiter = ';';
                    }
                    serializer = csvSer;
                    break;
                case 12: //HTML
                    serializer = new HtmlFileSerializer();
                    break;
                case 13: //Relational file
                    serializer = new RelationtableFileSerializer();
                    break;
                case 14: //Excel (xlsx)
                case 15:
                    var elsxSer = new XlsxSerializer() { InformationLevel = InformationLevelType.AllInformation };
                    elsxSer.DoubleColumn = format == 14 ? Settings.Files.DoubleColumnFile : DoubleColumnType.AlwaysDoubleColumns;
                    serializer = elsxSer;
                    break;
                case 16: //Relational tab with codes
                case 17:
                    var ser = new RelationTableCodeFileSerializer()
                    {
                        Delimiter = (char)Keys.Tab,
                        DoubleColumn = false,
                        WrapTextWithQuote = false,
                        ThousandSeparator = false,
                        ShowHeading = format == 16 //else 17 and therefore no heading
                    };
                    serializer = ser;
                    break;
                default:
                    serializer = new PXFileSerializer();
                    break;
            }
            return serializer;
        }

        /// <summary>
        /// returns a list with fileformat strings
        /// </summary>
        /// <returns></returns>
        public static List<string> GetFileFormatTexts()
        {
            return new List<string>
            {
                "FileTypePX",
                "FileTypeExcel",
                "FileTypeExcelDoubleColumn",
                "FileTypeCsvWithHeadingAndTabulator",
                "FileTypeCsvWithoutHeadingAndTabulator",
                "FileTypeCsvWithHeadingAndComma",
                "FileTypeCsvWithoutHeadingAndComma",
                "FileTypeCsvWithHeadingAndSpace",
                "FileTypeCsvWithoutHeadingAndSpace",
                "FileTypeCsvWithHeadingAndSemiColon",
                "FileTypeCsvWithoutHeadingAndSemiColon",
                "FileTypeHtml",
                "FileTypeRelational",
                "FileTypeExcelX",
                "FileTypeExcelXDoubleColumn",
                "FileTypeRelationalCodeWithHeading",
                "FileTypeRelationalCodeWithoutHeading"
            };
        }
        /// <summary>
        /// Returns a list with fileformats used in comboboxes etc. Ex: Text = PX-file (.px), Value = FileTypePX
        /// </summary>
        /// <returns></returns>
        public static List<FileFormat> GetFileFormats()
        {            
            return GetFileFormatTexts().Select(fileFormat => new FileFormat() {Text = Lang.GetLocalizedString(fileFormat), Value = fileFormat}).ToList();
        }


    }
}
