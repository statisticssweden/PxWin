using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PCAxis.Desktop
{
    public partial class GroupDialog : Form
    {
        private PCAxis.Paxiom.Variable _variable;
        private PCAxis.Paxiom.OperationsInfo _operationsInfo;
        private PCAxis.Paxiom.GroupingIncludesType _groupInclude;

        public GroupDialog()
        {
            InitializeComponent();
            this.Text = Lang.GetLocalizedString("SelectClassificationTitle");
        }

        public PCAxis.Paxiom.Variable Variable 
        {
            get
            {
                return _variable;
            }
            set
            {
                _variable = value;
                InitializeDialog();
            }
        }

        public PCAxis.Paxiom.OperationsInfo OperationsInfo
        {
            get
            {
                return _operationsInfo;
            }
        }

        public PCAxis.Paxiom.GroupingIncludesType GroupInclude
        {
            get
            {
                return _groupInclude;
            }
        }


        private void InitializeDialog()
        {
            List<PCAxis.Paxiom.OperationsInfo> lst = new List<PCAxis.Paxiom.OperationsInfo>();

            if (_variable.HasGroupings())
            {
                foreach (PCAxis.Paxiom.GroupingInfo grpInfo in _variable.Groupings)
                {
                    lst.Add(grpInfo);
                }
            }
            if (_variable.HasValuesets())
            {
                foreach (PCAxis.Paxiom.ValueSetInfo vsInfo in _variable.ValueSets)
                {
                    lst.Add(vsInfo);
                }
            }

            if (lst.Count > 0)
            {
                lstGroupings.DataSource = lst;
                lstGroupings.DisplayMember = "Name";
                lstGroupings.ValueMember = "ID";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lstGroupings.SelectedItem != null)
            {
                _operationsInfo = (PCAxis.Paxiom.OperationsInfo)lstGroupings.SelectedItem;

                //if (lstGroupings.SelectedItem is PCAxis.Paxiom.GroupingInfo)
                //{
                //    if (rbAggregated.Checked)
                //    {
                //        _groupInclude = Paxiom.GroupingIncludesType.AggregatedValues;
                //    }
                //    else if (rbSingle.Checked)
                //    {
                //        _groupInclude = Paxiom.GroupingIncludesType.SingleValues;
                //    }
                //    else
                //    {
                //        _groupInclude = Paxiom.GroupingIncludesType.All;
                //    }
                //}
                //else
                //{
                //    _groupInclude = Paxiom.GroupingIncludesType.AggregatedValues;   
                //}
            }
        }
    }
}
