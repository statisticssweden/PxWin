using Newtonsoft.Json.Linq;
using PCAxis.Paxiom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace PX.Api.Client
{
    public class ValueProxy : Value
    {
        public ValueProxy(string code, string text)
            : base(text)
        {
            SetCode(code);
        }
    }

    public class VariableProxy : Variable
    {
        public VariableProxy(string code, string text, PlacementType placment)
            : base(text, placment)
        {
            SetCode(code);
        }
    }


    public class ApiModelBuilder : PXModelBuilderAdapter
    {
        //private PXModel _model;
        //private ModelBuilderStateType _state;
        //private string _path;

        public ApiModelBuilder()
        {
            m_builderState = ModelBuilderStateType.Created;
        }

        protected override Value FindValue(Variable variable, string findId)
        {
            throw new NotImplementedException();
        }

        protected override Variable FindVariable(PXMeta meta, string findId)
        {
            Variable variable = null;

            if (findId.Equals(PCAxis.Paxiom.PXConstant.SORTVARIABLE))
            {
                findId = m_model.Meta.GetLocalizedSortVariableName(m_selectedLanguage);
            }

            variable = meta.Variables.GetByName(findId, meta.CurrentLanguageIndex);

            if (variable == null)
            {
                if (m_model.Meta.Variables.Count == 0)
                {
                    throw new PXModelParserException(ErrorCodes.STUB_AND_HEADING_MISSING);
                }
            }

            return variable;
        }

        protected override Variable FindVariable(PXMeta meta, string findId, int lang)
        {
            throw new NotImplementedException();
        }

        public override void ApplyGrouping(string variableCode, GroupingInfo groupingInfo, GroupingIncludesType include)
        {

        }

        public override void ApplyValueSet(string subTable)
        {

        }

        public override void ApplyValueSet(string variableCode, ValueSetInfo valueSet)
        {

        }

        public override bool BuildForPresentation(Selection[] selection)
        {
            m_builderState = ModelBuilderStateType.BuildingForPresentation;
            JObject queryData =
                new JObject(
                    new JProperty("query",
                        new JArray(from v in selection
                                   select new JObject(
                                       new JProperty("code", v.VariableCode),
                                       new JProperty("selection",
                                           new JObject(
                                               new JProperty("filter", "item"),
                                               new JProperty("values",
                                                   new JArray(
                                                       from val in v.ValueCodes.Cast<string>().ToArray<string>()
                                                       select new JValue(val)))))))),
                     new JProperty("response",
                         new JObject(
                             new JProperty("format", "px"))));


            var queryString = queryData.ToString();
            var data = GetData(m_path, queryString);

            if (data == null)
            {
                return false;
            }

            var tempFile = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllText(tempFile, data);

            PXFileBuilder builder = new PXFileBuilder();

            if (!string.IsNullOrEmpty(m_preferredLanguage))
            { 
                builder.SetPreferredLanguage(m_preferredLanguage);
            }

            builder.SetPath(tempFile);
            builder.BuildForSelection();
            builder.BuildForPresentation(Selection.SelectAll(builder.Model.Meta));

            // Set original codes to the variables
            // (eliminated variables must be taken care of)
            for (int indexSelection = 0; indexSelection < selection.Length; indexSelection++)
            {
                if (selection[indexSelection].ValueCodes.Count > 0)
                {
                    for (int i = 0; i < builder.Model.Meta.Variables.Count; i++)
                    {
                        bool ok = true;
                        // Find variable with the same number of values
                        if (builder.Model.Meta.Variables[i].Values.Count == selection[indexSelection].ValueCodes.Count)
                        {
                            // Verify that all the value codes are the same
                            for (int j = 0; j < builder.Model.Meta.Variables[i].Values.Count; j++)
                            {
                                if (!builder.Model.Meta.Variables[i].Values[j].Code.Equals(selection[indexSelection].ValueCodes[j]))
                                {
                                    // Not the right variable - continue to the next...
                                    ok = false;
                                    break;
                                }
                            }

                            // All value codes are the same - this is the right variable --> Change it´s variable code!
                            if (ok)
                            {
                                SetVariableCode(builder.Model.Meta.Variables[i].Name, selection[indexSelection].VariableCode, builder.Model.Meta);
                                break;
                            }
                        }
                    }
                }
            }
            
            string mainTable = m_model.Meta.MainTable;
            m_model = builder.Model;
            m_model.Meta.MainTable = mainTable;

            builder.Dispose();

            System.IO.File.Delete(tempFile);

            m_builderState = ModelBuilderStateType.BuildForPresentation;
            return true;
        }

        public override bool BuildForSelection()
        {
            m_builderState = ModelBuilderStateType.BuildingForSelection;
            var meta = new PXMeta();

            var data = GetMetadata(m_path);

            if (data == null)
            {
                return false;
            }

            //meta.Language = "sv";
            meta.MainTable = m_path;

            meta.Title = (string)data["title"];

            foreach (var var in data["variables"])
            {
                VariableProxy v = new VariableProxy((string)var["code"], (string)var["text"], PlacementType.Heading);

                if (var["elimination"] != null)
                {
                    v.Elimination = (bool)var["elimination"];
                }

                if (var["time"] != null)
                {
                    v.IsTime = (bool)var["time"];
                }

                int c = 0;
                foreach (var item in var["values"])
                {
                    ValueProxy vp = new ValueProxy((string)item, (string)var["valueTexts"][c]);
                    if (v.IsTime)
                    {
                        vp.TimeValue = (string)item;
                    }
                    c++;
                    v.Values.Add(vp);
                }

                meta.Variables.Add(v);
            }

            //var variable = new VariableProxy("v1", "Test1", PlacementType.Heading);
            //var value = new ValueProxy("01", "value1");

            //variable.Values.Add(value);
            //value = new ValueProxy("02", "value2");

            //variable.Values.Add(value);

            //variable.PresentationText = 2;
            //meta.Variables.Add(variable);

            m_model = new PXModel(meta, new PXData());

            //meta.CreateTitle();
            m_builderState = ModelBuilderStateType.BuildForSelection;


            return true;

        }

        private JObject GetMetadata(string url)
        {
            var http = new HttpClient();
            try
            {
                var response = http.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return JObject.Parse(data);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        private string GetData(string url, string query)
        {
            var http = new HttpClient();

            try
            {
                var response = http.PostAsync(url, new StringContent(query)).Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return data;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        //public ModelBuilderStateType BuilderState
        //{
        //    get { return _state; }
        //}

        //public List<BuilderMessage> Errors
        //{
        //    get { return new List<BuilderMessage>(); }
        //}

        //public PXModel Model
        //{
        //    get { return _model; }
        //}

        //public string Path
        //{
        //    get { return _path; }
        //}

        //public bool ReadAllLanguages
        //{
        //    get
        //    {
        //        return false;
        //    }
        //    set
        //    {

        //    }
        //}

        public override void SetPath(string path)
        {
            m_path = path;
        }

        //public void SetPreferredLanguage(string language)
        //{
        //    //TODO Try to set perfred language
        //    //(throw new NotImplementedException();
        //}

        //public void SetUserCredentials(string userName, string password)
        //{

        //}

        //public List<BuilderMessage> Warnings
        //{
        //    get { return new List<BuilderMessage>(); }
        //}
    }
}
