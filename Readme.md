<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128620618/13.2.9%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T100881)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [CustomButtonEdit.cs](./CS/CustomButtonEdit/CustomButtonEdit.cs) (VB: [CustomButtonEdit.vb](./VB/CustomButtonEdit/CustomButtonEdit.vb))
* [Form1.cs](./CS/Form1.cs) (VB: [Form1.vb](./VB/Form1.vb))
* [Program.cs](./CS/Program.cs) (VB: [Program.vb](./VB/Program.vb))
<!-- default file list end -->
# How to display disabled buttons for particular cells within a ButtonEdit column


<p>This example demonstrates how to display a disabled button for particular cells depending upon some condition. This task can be accomplished by creating a custom button editor. Please refer to the <a href="https://docs.devexpress.com/WindowsForms/4716/controls-and-libraries/editors-and-simple-controls/common-editor-features-and-concepts/custom-editors">Custom Editors</a> article to learn more about custom editor creation.</p>
<p>The described solution is based on overriding the OnBeginPaint() method of a custom ViewInfo class. This method is called before drawing the editor and applies a desired button state depending upon the condition. Additionally, the RepositoryItemCustomButtonEdit.GetButtonState event is implemented. Subscribing to this event allows a customer to pass any custom condition to the editor. The GetButtonState event handler has the ButtonStateEventArgs parameter, which contains a GridCellInfo class instance as the Tag property. Necessary GridControl data can be obtained from the Tag object.</p>

<br/>


