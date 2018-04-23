using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Data;

namespace Example {
    public partial class Form1 : DevExpress.XtraEditors.XtraForm {
        public Form1() {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e) {
            DataTable table = CreateTable();
            CustomButtonEdit.RepositoryItemCustomButtonEdit edit = new CustomButtonEdit.RepositoryItemCustomButtonEdit();
            edit.GetButtonState += buttonConditionHandler;
            this.gridControl1.RepositoryItems.Add(edit);
            this.gridControl1.DataSource = table;
            this.gridView1.Columns[2].ColumnEdit = edit;
            this.gridView1.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
        }
        private DataTable CreateTable() {
            DataTable table = new DataTable();
            table.Columns.Add("First column");
            table.Columns.Add("Condition column", typeof(bool));
            table.Columns.Add("Result column");
            table.Rows.Add("Row One", true, "ButtonEdit1");
            table.Rows.Add("Row Two", false, "ButtonEdit2");
            table.Rows.Add("Row Three", false, "ButtonEdit3");
            table.Rows.Add("Row Four", true, "ButtonEdit4");
            table.Rows.Add("Row Five", true, "ButtonEdit5");
            return table;
        }
        private void buttonConditionHandler(object sender, CustomButtonEdit.ButtonStateEventArgs e) {
            object value = e.IsEnable;
            if (e.Tag != null) {
                GridCellInfo gci = (e.Tag as GridCellInfo);
                value = (gci.Column.View as GridView).GetRowCellValue(gci.RowHandle, (gci.Column.View as GridView).Columns[1]);
            }
            else if (gridView1.ActiveEditor == sender) {
                value = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]);
            }
            e.IsEnable = (value != null && value.GetType() == typeof(bool)) ? (bool)value : false;
        }
    }
}
