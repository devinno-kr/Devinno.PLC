using Devinno.Forms;
using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Icons;
using Devinno.PLC.Ladder;
using LadderEditor.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LadderEditor.Forms
{
    public partial class FormLibrary : DvForm
    {
        #region Member Variable
        List<LadderReference> Libs = new List<LadderReference>();
        #endregion

        #region Constructor
        public FormLibrary()
        {
            InitializeComponent();

            #region Icon
            StartPosition = FormStartPosition.CenterParent;
            Icon = IconTool.GetIcon(new DvIcon(TitleIconString, Convert.ToInt32(TitleIconSize)), Program.ICO_WH, Program.ICO_WH, Color.White);
            #endregion

            #region Set
            #region dragDLL
            dragDLL.AllowDrop = true;
            #endregion
            #region dgLibrary
            dgLibrary.SelectionMode = DvDataGridSelectionMode.Selector;
            dgLibrary.Columns.Add(new DvDataGridColumn(dgLibrary) { Name = "Name", HeaderText = "라이브러리", Width = 50M, SizeMode = DvSizeMode.Percent });
            dgLibrary.Columns.Add(new DvDataGridColumn(dgLibrary) { Name = "DllPath", HeaderText = "DLL", Width = 50M, SizeMode = DvSizeMode.Percent });
            #endregion
            #region dgReference
            dgReference.SelectionMode = DvDataGridSelectionMode.Selector;
            dgReference.Columns.Add(new DvDataGridColumn(dgReference) { Name = "Name", HeaderText = "라이브러리", Width = 40M, SizeMode = DvSizeMode.Percent });
            dgReference.Columns.Add(new DvDataGridColumn(dgReference) { Name = "DllPath", HeaderText = "DLL", Width = 30M, SizeMode = DvSizeMode.Percent });
            dgReference.Columns.Add(new DvDataGridEditTextColumn(dgReference) { Name = "InstanceName", HeaderText = "변수명", Width = 30M, SizeMode = DvSizeMode.Percent });
            #endregion
            #endregion

            #region Event
            #region btn[OK/Cancel].ButtonClick
            btnOK.ButtonClick += (o, s) => DialogResult = DialogResult.OK;
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;
            #endregion
            #region dragDLL.DragEnter
            dragDLL.DragEnter += (o, s) =>
            {
                if (s.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    var files = (string[])s.Data.GetData(DataFormats.FileDrop);
                    var dlls = files.Where(x => Path.GetExtension(x).ToLower() == ".dll").ToList();

                    if (files.Length == 1 && dlls.Count == 1)
                    {
                        var vls = DllTool.Load(dlls[0], (tp, v) => { });
                        if (vls.Count > 0) s.Effect = DragDropEffects.Copy;
                    }
                }
            };
            #endregion
            #region dragDLL.DragDrop
            dragDLL.DragDrop += (o, s) =>
            {
                var files = (string[])s.Data.GetData(DataFormats.FileDrop);
                var dlls = files.Where(x => Path.GetExtension(x).ToLower() == ".dll").ToList();

                if (files.Length == 1 && dlls.Count == 1)
                {
                    Program.LibMgr.UploadLibrary(dlls[0]);
                    RefreshLibrary();
                }
            };
            #endregion
            #region btnReg.ButtonClick 
            btnReg.ButtonClick += (o, s) =>
            {
                var sels = dgLibrary.Rows.Where(x => x.Selected).ToList();
                if(sels.Count > 0)
                {
                    Libs.AddRange(sels.Select(x => (LadderReference)x.Source));
                    RefreshLibrary();
                }
            };
            #endregion
            #region btnUnreg.ButtonClick
            btnUnreg.ButtonClick += (o, s) =>
            {
                var sels = dgReference.Rows.Where(x => x.Selected).ToList();
                if (sels.Count > 0)
                {
                    foreach (var v in sels) Libs.Remove((LadderReference)v.Source);
                    RefreshLibrary();
                }
            };
            #endregion
            #endregion
        }
        #endregion

        #region Method
        #region RefreshLibrary
        void RefreshLibrary()
        {
            dgLibrary.SetDataSource<LadderReference>(Program.LibMgr.Libraries);
            dgLibrary.Invalidate();

            dgReference.SetDataSource<LadderReference>(Libs);
            dgReference.Invalidate();
        }
        #endregion
        #region ShowLibrary
        public List<LadderReference> ShowLibrary(List<LadderReference> libs)
        {
            List<LadderReference> ret = null;
            
            Libs.Clear();
            Libs.AddRange(libs);

            RefreshLibrary();

            if (this.ShowDialog() == DialogResult.OK)
            {
                ret = Libs;
            }

            return ret;
        }
        #endregion
        #endregion

    }
}
