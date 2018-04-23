Imports Microsoft.VisualBasic
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System
Imports System.Data

Namespace Example
	Partial Public Class Form1
		Inherits DevExpress.XtraEditors.XtraForm
		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Dim table As DataTable = CreateTable()
			Dim edit As New CustomButtonEdit.RepositoryItemCustomButtonEdit()
			AddHandler edit.GetButtonState, AddressOf buttonConditionHandler
			Me.gridControl1.RepositoryItems.Add(edit)
			Me.gridControl1.DataSource = table
			Me.gridView1.Columns(2).ColumnEdit = edit
			Me.gridView1.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
		End Sub
		Private Function CreateTable() As DataTable
			Dim table As New DataTable()
			table.Columns.Add("First column")
			table.Columns.Add("Condition column", GetType(Boolean))
			table.Columns.Add("Result column")
			table.Rows.Add("Row One", True, "ButtonEdit1")
			table.Rows.Add("Row Two", False, "ButtonEdit2")
			table.Rows.Add("Row Three", False, "ButtonEdit3")
			table.Rows.Add("Row Four", True, "ButtonEdit4")
			table.Rows.Add("Row Five", True, "ButtonEdit5")
			Return table
		End Function
		Private Sub buttonConditionHandler(ByVal sender As Object, ByVal e As CustomButtonEdit.ButtonStateEventArgs)
			Dim value As Object = e.IsEnable
			If e.Tag IsNot Nothing Then
				Dim gci As GridCellInfo = (TryCast(e.Tag, GridCellInfo))
				value = (TryCast(gci.Column.View, GridView)).GetRowCellValue(gci.RowHandle, (TryCast(gci.Column.View, GridView)).Columns(1))
			ElseIf gridView1.ActiveEditor Is sender Then
				value = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns(1))
			End If
			e.IsEnable = If((value IsNot Nothing AndAlso value.GetType() Is GetType(Boolean)), CBool(value), False)
		End Sub
	End Class
End Namespace
