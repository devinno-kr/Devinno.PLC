using Devinno.Forms.Dialogs;
using Devinno.PLC.Ladder;
using LadderEditor.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LadderEditor.Forms
{
    public partial class FormSearch : DvForm
    {
        LadderEditorControl editor;
        
        int SearchIndex = 0;
        string SearchText = "";
        List<FindItem> ls = new List<FindItem>();

        public FormSearch()
        {
            InitializeComponent();

            inSearch.OriginalTextBox.KeyUp += (o, s) => { if (s.KeyCode == Keys.Enter) Search(); };
            btnSearch.ButtonClick += (o, s) => Search();
            btnClose.ButtonClick += (o, s) => this.Close();
        }

        protected override void OnShown(EventArgs e)
        {
            inSearch.OriginalTextBox.Focus();
            inSearch.OriginalTextBox.SelectAll();
            base.OnShown(e);
        }

        private void Search()
        {
            if (!string.IsNullOrWhiteSpace(inSearch.Value))
            {
                if (SearchText != inSearch.Value)
                {
                    SearchText = inSearch.Value;
                    SearchIndex = 0;

                    var items = editor.Ladders.Select(x => new FindItem { Item = x }).ToList();
                    foreach (var v in items)
                    {
                        var vi = Program.MainForm.CurrentDocument.Symbols.Where(x => x.Address == v.Item.Code).FirstOrDefault();
                        if (vi != null) v.Alias = vi.SymbolName;
                    }

                    ls = items.Where(x =>(x.Item.Code != null && x.Item.Code.IndexOf(SearchText) != -1) ||(x.Alias != null && x.Alias.IndexOf(SearchText) != -1)).ToList();
                }

                if (ls.Count > 0 && SearchIndex >= 0 && SearchIndex < ls.Count)
                {
                    editor.CurX = ls[SearchIndex].Item.Col;
                    editor.CurY = ls[SearchIndex].Item.Row;

                    SearchIndex++;
                    if (SearchIndex >= ls.Count) SearchIndex = 0;
                }
            }
        }

        public void ShowSearch(LadderEditorControl editor)
        {
            this.editor = editor;
            this.SearchIndex = 0;
            this.SearchText = "";

            this.Show();
        }
    }

    public class FindItem
    {
        public LadderItem Item { get; set; }
        public string Alias { get; set; }
    }
}
