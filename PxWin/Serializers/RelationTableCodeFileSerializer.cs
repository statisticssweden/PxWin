using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCAxis.Paxiom;
using PCAxis.Paxiom.Operations;
using System.ComponentModel.Composition;
using PX.Plugin.Interfaces.Attributes;

namespace PCAxis.Desktop.Serializers
{
	/// <summary>
	/// Writes a PXModel to file or a stream in txt format with codes.
	/// This class i based on code from RelationtableFileSerializer and CsvFileSerializer
	/// </summary>
	/// <remarks></remarks>
	public class RelationTableCodeFileSerializer : IPXModelStreamSerializer
	{

		#region "Private fields"
		private PXModel _model;
		private char _delimiter = ',';
		private char _decimalSeparator = '.';

	    #endregion
		private bool _wrapTextWithQuote = true;

	    public RelationTableCodeFileSerializer()
	    {
	        ThousandSeparator = false;
	        Title = false;
	        DoubleColumn = false;
	    }

	    private const char Quote = '"';

		#region "IPXModelStreamSerializer Interface members"
		/// <summary>
		/// Write a PXModel to a file.
		/// </summary>
		/// <param name="model">The PXModel to write.</param>
		/// <param name="path">The complete file path to write to. <I>path</I> can be a file name.</param>
		/// <remarks></remarks>
        public void Serialize(PXModel model, string path)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            // Let the StreamWriter verify the path argument
		    var encoding = EncodingUtil.GetEncoding(model.Meta.CodePage);
            using (var writer = new System.IO.StreamWriter(path, false, encoding))
            {
                DoSerialize(model, writer);
            }

        }

		/// <summary>
		/// Write a PXModel to a stream.
		/// </summary>
		/// <param name="model">The PXModel to write.</param>
		/// <param name="stream">The stream to write to.</param>
		/// <remarks>The caller is responsible of disposing the stream.</remarks>
		public void Serialize(PXModel model, System.IO.Stream stream)
		{
			if (model == null)
				throw new ArgumentNullException("model");
			if (stream == null)
				throw new ArgumentNullException("stream");

			if (!stream.CanWrite)
				throw new ArgumentException("The stream does not support writing");

            var encoding = EncodingUtil.GetEncoding(model.Meta.CodePage);
			var writer = new System.IO.StreamWriter(stream, encoding);
            DoSerialize(model, writer);
			writer.Flush();
		}
		#endregion

		/// <summary>
		/// Serializes the model to the stream in the csv format.
		/// </summary>
		/// <param name="model">The model to serialize</param>
		/// <param name="wr">The stream to serialize to</param>
		/// <remarks></remarks>
		private void DoSerialize(PXModel model, System.IO.StreamWriter wr)
		{
			_model = PrepareModel(model);
		    if (ShowHeading)
		    {
                WriteHeading(wr);
		    }		    
			WriteTable(wr);
		}

		/// <summary>
		/// Writes the heading (the column namnes separated by comma) to a stream
		/// </summary>
		/// <param name="wr">A StreamWriter that encapsulates the stream</param>
		/// <remarks></remarks>
		private void WriteHeading(System.IO.StreamWriter wr)
		{
			// Write stub variable names 
			for (int i = 0; i <= _model.Meta.Stub.Count - 1; i++) {
				if (i > 0) {
					wr.Write(Delimiter);
				}

				if (DoubleColumn) {
					if (_model.Meta.Stub[i].DoubleColumn) {
						if (WrapTextWithQuote)
							wr.Write(Quote);
						wr.Write(_model.Meta.Stub[i].Code);
						if (WrapTextWithQuote)
							wr.Write(Quote);
						wr.Write(Delimiter);
					}
				}
				if (WrapTextWithQuote)
					wr.Write(Quote);
                //Renames the variable Tid to Time
				wr.Write(_model.Meta.Stub[i].IsTime ? "Time" : _model.Meta.Stub[i].Code);
				
                if (WrapTextWithQuote)
					wr.Write(Quote);
			}

            //Write header for the data
            wr.Write(Delimiter);
            wr.Write("Data");

			//Write concatenated heading variable values
			if (_model.Meta.Heading.Count > 0) {
			    wr.Write(Delimiter);

				var sc = ConcatHeadingValues(0);
				for (int i = 0; i <= sc.Count - 1; i++) {
					if (i > 0) {
						wr.Write(Delimiter);
					}

					if (WrapTextWithQuote)
						wr.Write(Quote);
					wr.Write(sc[i]);
					if (WrapTextWithQuote)
						wr.Write(Quote);
				}
				wr.WriteLine();
			} else {
				//All parameters are in the Stub				
				//We still need a new line
				wr.WriteLine();
			}

		}

		/// <summary>
		/// Creates the heading texts by finding all the possible combinations of the heading variables.
		/// </summary>
		/// <param name="headingIndex">The index of the heading variable</param>
		/// <returns>A stringcollection representing all the concatenated heading texts for the given index</returns>
		/// <remarks></remarks>
		private StringCollection ConcatHeadingValues(int headingIndex)
		{
			var sc = new StringCollection();
			var sc2 = new StringCollection();

			if (headingIndex < _model.Meta.Heading.Count - 1) {
				//Call recursivly to get the combinations
				sc2 = ConcatHeadingValues(headingIndex + 1);
				for (int valueIndex = 0; valueIndex <= _model.Meta.Heading[headingIndex].Values.Count - 1; valueIndex++) {
					for (int j = 0; j <= sc2.Count - 1; j++) {
						sc.Add(_model.Meta.Heading[headingIndex].Values[valueIndex].Text + " " + sc2[j]);
					}
				}
			} else {
				for (int valueIndex = 0; valueIndex <= _model.Meta.Heading[headingIndex].Values.Count - 1; valueIndex++) {
					sc.Add(_model.Meta.Heading[headingIndex].Values[valueIndex].Text);
				}
			}

			return sc;
		}

		/// <summary>
		/// Writes the data to a stream
		/// </summary>
		/// <param name="wr">The stream to write to</param>
		/// <remarks></remarks>
		private void WriteTable(System.IO.StreamWriter wr)
		{
			//If _model.Meta.Stub.Count > 0 Then
		    var df = new DataFormatter(_model);
			var value = "";

			df.DecimalSeparator = DecimalSeparator.ToString();

			if (!ThousandSeparator) {
				df.ThousandSeparator = "";
			}


			if (_model.Meta.Stub.Count > 0) {
				var sc = ConcatStubValues(0);

				//There should be exactly as many items in the stringcollection as 
				//the number of rows in the data.
				if (sc.Count != _model.Data.MatrixRowCount) {
					//TODO: Errorcode
					throw new PXSerializationException("Stubvalues does not match the data", "");
				}

				for (int i = 0; i <= sc.Count - 1; i++) {
					wr.Write(sc[i]);
					for (int c = 0; c <= _model.Data.MatrixColumnCount - 1; c++) {
						value = df.ReadElement(i, c);
						wr.Write(Delimiter);
						wr.Write(value);
					}
					wr.WriteLine();
				}
			} else if (_model.Meta.Heading.Count > 0) {
				for (int c = 0; c <= _model.Data.MatrixColumnCount - 1; c++) {
					value = df.ReadElement(0, c);
					wr.Write(Delimiter);
					wr.Write(value);
				}
			}
			
		}

		/// <summary>
		/// Concatenates the stubvales 
		/// </summary>
		/// <param name="stubIndex">The index of the stub variable</param>
		/// <returns>Strincollection with all the concatenated stubcalues for the given index</returns>
		/// <remarks></remarks>
		private StringCollection ConcatStubValues(int stubIndex)
		{
			StringCollection sc = new StringCollection();

		    if (stubIndex < _model.Meta.Stub.Count - 1)
			{
			    //Call recursivly to get the combinations
			    var sc2 = ConcatStubValues(stubIndex + 1);
			    for (int valueIndex = 0; valueIndex <= _model.Meta.Stub[stubIndex].Values.Count - 1; valueIndex++) {
					for (int j = 0; j <= sc2.Count - 1; j++) {
						sc.Add(TableStub(stubIndex, valueIndex) + Delimiter + sc2[j]);
					}
				}
			}
			else {
				for (int valueIndex = 0; valueIndex <= _model.Meta.Stub[stubIndex].Values.Count - 1; valueIndex++) {
					sc.Add(TableStub(stubIndex, valueIndex));
				}
			}

			return sc;
		}

		/// <summary>
		/// Get the stub value and code
		/// </summary>
		/// <param name="stubIndex">Index of the stubvariable</param>
		/// <param name="valueIndex">Index of the value</param>
		/// <returns>
		/// Returns the value. If the variable has code and doublecolumn is true both code
		/// and value are returned separated by the delimiter.
		/// </returns>
		/// <remarks></remarks>
		private string TableStub(int stubIndex, int valueIndex)
		{
			var sb = new StringBuilder();

			if (DoubleColumn) {
				if (_model.Meta.Stub[stubIndex].DoubleColumn) {
					if (_model.Meta.Stub[stubIndex].Values[valueIndex].HasCode()) {
						if (WrapTextWithQuote)
							sb.Append(Quote);
						sb.Append(_model.Meta.Stub[stubIndex].Values[valueIndex].Code);
						if (WrapTextWithQuote)
							sb.Append(Quote);
						sb.Append(Delimiter);
					}
				}
			}
			if (WrapTextWithQuote)
				sb.Append(Quote);
			sb.Append(_model.Meta.Stub[stubIndex].Values[valueIndex].Code);
			if (WrapTextWithQuote)
				sb.Append(Quote);

			return sb.ToString();
		}

        /// <summary>
        /// Prepare a model for export by pivoting the model to have all variables in the Stub
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static PXModel PrepareModel(PXModel model)
        {
            var pivotOperation = new Pivot();

            var pivotDescriptions = new PivotDescription[model.Meta.Variables.Count];

            // Loop Stub and Heading variables and add to pivotDescription
            // All variables are placed in the Stub to match the export format to use.

            //Check number of ContentCode variables. They should always be at the start of the array
            var startIndex = model.Meta.Variables.Count(variable => variable.IsContentVariable);
            var contentsCodeIndex = 0;

            //Pivot all variables to stub
            for (int i = 0; i <= model.Meta.Variables.Count - 1; i++)
            {
                var pv = new PivotDescription
                {
                    VariableName = model.Meta.Variables[i].Name,
                    VariablePlacement = PlacementType.Stub
                };

                if (model.Meta.Variables[i].IsContentVariable) //ContentCodes goes to start of array
                {
                    pivotDescriptions[contentsCodeIndex] = pv;
                    contentsCodeIndex++;
                }
                else if (model.Meta.Variables[i].IsTime) //Time variable goes to end of array
                {
                    pivotDescriptions[pivotDescriptions.Length - 1] = pv;
                }
                else
                {
                    pivotDescriptions[startIndex] = pv;
                    startIndex++;
                }
            }

            var preparedModel = pivotOperation.Execute(model, pivotDescriptions);

            // Also ensure that rounding is in the std format
            preparedModel.Meta.Rounding = RoundingType.BankersRounding;

            return preparedModel;
        }

		#region "Public properties"
		public char Delimiter {
			get { return _delimiter; }
			set { _delimiter = value; }
		}

		public char DecimalSeparator {
			get { return _decimalSeparator; }
			set { _decimalSeparator = value; }
		}

		public bool DoubleColumn { get; set; }

	    public bool Title { get; set; }

	    public bool ThousandSeparator { get; set; }


	    public bool WrapTextWithQuote {
			get { return _wrapTextWithQuote; }
			set { _wrapTextWithQuote = value; }
		}

	    public bool ShowHeading { get; set; }

		#endregion

        //Relational code file with heading 
        [Export]
        [SerializerMetadata(Id = "FileTypeRelationalCodeWithHeading", Extension = "txt")]
	    public static IPXModelStreamSerializer CreateWithHeading()
	    {
            return  new RelationTableCodeFileSerializer()
                    {
                        Delimiter = (char)Keys.Tab,
                        DoubleColumn = false,
                        WrapTextWithQuote = false,
                        ThousandSeparator = false,
                        ShowHeading = true
                    };
	    }

        //Relational code file without heading
        [Export]
        [SerializerMetadata(Id = "FileTypeRelationalCodeWithoutHeading", Extension = "txt")]
        public static IPXModelStreamSerializer CreateWithoutHeading()
        {
            return new RelationTableCodeFileSerializer()
            {
                Delimiter = (char)Keys.Tab,
                DoubleColumn = false,
                WrapTextWithQuote = false,
                ThousandSeparator = false,
                ShowHeading = false
            };
        }

	}

}



