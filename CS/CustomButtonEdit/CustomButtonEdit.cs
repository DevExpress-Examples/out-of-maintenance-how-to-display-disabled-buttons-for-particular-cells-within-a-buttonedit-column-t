using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomButtonEdit {
    public delegate void GetButtonStateEventHandler(object sender, ButtonStateEventArgs e);
    public class RepositoryItemCustomButtonEdit : RepositoryItemButtonEdit {
        public const string EditName = "CustomButtonEdit";
        private static readonly object buttonStateEvent = new object();      

        public event GetButtonStateEventHandler GetButtonState {
            add { this.Events.AddHandler(buttonStateEvent, value); }
            remove { this.Events.RemoveHandler(buttonStateEvent, value); }
        } 
        public void RaiseButtonStateEvent(ButtonStateEventArgs e) {
            GetButtonStateEventHandler handler = (GetButtonStateEventHandler)this.Events[buttonStateEvent];
            if (handler != null) handler(GetEventSender(), e);
        }
        public override string EditorTypeName {
            get {
                return EditName;
            }
        }
        static RepositoryItemCustomButtonEdit() {
            RegisterEditor();
        }
        public RepositoryItemCustomButtonEdit() {

        }
        public static void RegisterEditor() {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditName, typeof(CustomButtonEdit), typeof(RepositoryItemCustomButtonEdit), typeof(CustomButtonEditViewInfo), new ButtonEditPainter(), true));
        }
        public override void Assign(RepositoryItem item) {
            RepositoryItemCustomButtonEdit source = item as RepositoryItemCustomButtonEdit;
            BeginUpdate();
            try {
                base.Assign(item);
                if (source == null) return;
            }
            finally {
                EndUpdate();
            }
            Events.AddHandler(buttonStateEvent, source.Events[buttonStateEvent]);
        }
    }
    [ToolboxItem(true)]
    public class CustomButtonEdit : ButtonEdit {
        public override string EditorTypeName {
            get {
                return RepositoryItemCustomButtonEdit.EditName;
            }
        }
        public new RepositoryItemCustomButtonEdit Properties {
            get {
                return base.Properties as RepositoryItemCustomButtonEdit;
            }
        }
        static CustomButtonEdit() {
            RepositoryItemCustomButtonEdit.RegisterEditor();
        }
        public CustomButtonEdit() {
        }
    }
    public class CustomButtonEditViewInfo : ButtonEditViewInfo {
        protected override void OnBeginPaint() {
            base.OnBeginPaint();
            ApplyState();
        }
        public void ApplyState() {
            ButtonStateEventArgs e = new ButtonStateEventArgs(Tag);
            (this.Item as RepositoryItemCustomButtonEdit).RaiseButtonStateEvent(e);
            if (!e.IsEnable && this.RightButtons.Count > 0) {
                this.RightButtons[0].State = DevExpress.Utils.Drawing.ObjectState.Disabled;
            }
        }
        public CustomButtonEditViewInfo(RepositoryItem item)
            : base(item) {
        }
    }    

    public class ButtonStateEventArgs : EventArgs {
        public object Tag { get; set; }
        public bool IsEnable { get; set; }
        public ButtonStateEventArgs(object tag)
            : base() {
            this.Tag = tag;
            this.IsEnable = true;
        }
    }
}
