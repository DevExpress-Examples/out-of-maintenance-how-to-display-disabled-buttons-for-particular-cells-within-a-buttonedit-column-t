Imports Microsoft.VisualBasic
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.Registrator
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace CustomButtonEdit
	Public Delegate Sub GetButtonStateEventHandler(ByVal sender As Object, ByVal e As ButtonStateEventArgs)
	Public Class RepositoryItemCustomButtonEdit
		Inherits RepositoryItemButtonEdit
		Public Const EditName As String = "CustomButtonEdit"
		Private Shared ReadOnly buttonStateEvent As Object = New Object()

		Public Custom Event GetButtonState As GetButtonStateEventHandler
			AddHandler(ByVal value As GetButtonStateEventHandler)
				Me.Events.AddHandler(buttonStateEvent, value)
			End AddHandler
			RemoveHandler(ByVal value As GetButtonStateEventHandler)
				Me.Events.RemoveHandler(buttonStateEvent, value)
			End RemoveHandler
			RaiseEvent(ByVal sender As Object, ByVal e As ButtonStateEventArgs)
			End RaiseEvent
		End Event
		Public Sub RaiseButtonStateEvent(ByVal e As ButtonStateEventArgs)
			Dim handler As GetButtonStateEventHandler = CType(Me.Events(buttonStateEvent), GetButtonStateEventHandler)
			If handler IsNot Nothing Then
				handler(GetEventSender(), e)
			End If
		End Sub
		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return EditName
			End Get
		End Property
		Shared Sub New()
			RegisterEditor()
		End Sub
		Public Sub New()

		End Sub
		Public Shared Sub RegisterEditor()
			EditorRegistrationInfo.Default.Editors.Add(New EditorClassInfo(EditName, GetType(CustomButtonEdit), GetType(RepositoryItemCustomButtonEdit), GetType(CustomButtonEditViewInfo), New ButtonEditPainter(), True))
		End Sub
		Public Overrides Sub Assign(ByVal item As RepositoryItem)
			Dim source As RepositoryItemCustomButtonEdit = TryCast(item, RepositoryItemCustomButtonEdit)
			BeginUpdate()
			Try
				MyBase.Assign(item)
				If source Is Nothing Then
					Return
				End If
			Finally
				EndUpdate()
			End Try
			Events.AddHandler(buttonStateEvent, source.Events(buttonStateEvent))
		End Sub
	End Class
	<ToolboxItem(True)> _
	Public Class CustomButtonEdit
		Inherits ButtonEdit
		Public Overrides ReadOnly Property EditorTypeName() As String
			Get
				Return RepositoryItemCustomButtonEdit.EditName
			End Get
		End Property
		Public Shadows ReadOnly Property Properties() As RepositoryItemCustomButtonEdit
			Get
				Return TryCast(MyBase.Properties, RepositoryItemCustomButtonEdit)
			End Get
		End Property
		Shared Sub New()
			RepositoryItemCustomButtonEdit.RegisterEditor()
		End Sub
		Public Sub New()
		End Sub
	End Class
	Public Class CustomButtonEditViewInfo
		Inherits ButtonEditViewInfo
		Protected Overrides Sub OnBeginPaint()
			MyBase.OnBeginPaint()
			ApplyState()
		End Sub
		Public Sub ApplyState()
			Dim e As New ButtonStateEventArgs(Tag)
			TryCast(Me.Item, RepositoryItemCustomButtonEdit).RaiseButtonStateEvent(e)
			If (Not e.IsEnable) AndAlso Me.RightButtons.Count > 0 Then
				Me.RightButtons(0).State = DevExpress.Utils.Drawing.ObjectState.Disabled
			End If
		End Sub
		Public Sub New(ByVal item As RepositoryItem)
			MyBase.New(item)
		End Sub
	End Class

	Public Class ButtonStateEventArgs
		Inherits EventArgs
		Private privateTag As Object
		Public Property Tag() As Object
			Get
				Return privateTag
			End Get
			Set(ByVal value As Object)
				privateTag = value
			End Set
		End Property
		Private privateIsEnable As Boolean
		Public Property IsEnable() As Boolean
			Get
				Return privateIsEnable
			End Get
			Set(ByVal value As Boolean)
				privateIsEnable = value
			End Set
		End Property
		Public Sub New(ByVal tag As Object)
			MyBase.New()
			Me.Tag = tag
			Me.IsEnable = True
		End Sub
	End Class
End Namespace
