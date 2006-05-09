
'
' This is an example application to demonstrate the usage of the MIL.Html library.
'
' The only sub of real relevance is the ProcessHTML sub. However, you may be interested
' in the reading the code to see how to iterate through the DOM tree.
'
' If decide to have a look at the Microsoft homepage's HTML, you will see loads of "text"
' items that appear empty. This is due to carriage returns being treated as text items.

Imports dk.hob.Windows.Forms.Html
Imports dk.hob.Windows.Forms.Html.Parser
Imports System.Windows.Forms

Public Class TestForm
    Inherits System.Windows.Forms.Form
    Dim mDocument As HtmlDocument

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents tvwDOM As System.Windows.Forms.TreeView
    Friend WithEvents objSlider As System.Windows.Forms.PictureBox
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents mnuFile As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOpenFile As System.Windows.Forms.MenuItem
    Friend WithEvents mnuExit As System.Windows.Forms.MenuItem
    Friend WithEvents OpenHtmlFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents TreeNodeMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuViewHTML As System.Windows.Forms.MenuItem
    Friend WithEvents mnuViewXHTML As System.Windows.Forms.MenuItem
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents txtHTML As System.Windows.Forms.TextBox
    Friend WithEvents objSliderBottom As System.Windows.Forms.PictureBox
    Friend WithEvents grdProperties As System.Windows.Forms.PropertyGrid
    Friend WithEvents mnuFileSaveAs As System.Windows.Forms.MenuItem
    Friend WithEvents SaveHtmlFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents HtmlView1 As HtmlView
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TestForm))
        Me.tvwDOM = New System.Windows.Forms.TreeView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.objSlider = New System.Windows.Forms.PictureBox
        Me.MainMenu1 = New System.Windows.Forms.MainMenu
        Me.mnuFile = New System.Windows.Forms.MenuItem
        Me.mnuOpenFile = New System.Windows.Forms.MenuItem
        Me.mnuFileSaveAs = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mnuExit = New System.Windows.Forms.MenuItem
        Me.OpenHtmlFileDialog = New System.Windows.Forms.OpenFileDialog
        Me.TreeNodeMenu = New System.Windows.Forms.ContextMenu
        Me.mnuViewHTML = New System.Windows.Forms.MenuItem
        Me.mnuViewXHTML = New System.Windows.Forms.MenuItem
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.grdProperties = New System.Windows.Forms.PropertyGrid
        Me.objSliderBottom = New System.Windows.Forms.PictureBox
        Me.txtHTML = New System.Windows.Forms.TextBox
        Me.SaveHtmlFileDialog = New System.Windows.Forms.SaveFileDialog
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.HtmlView1 = New HtmlView
        Me.pnlBottom.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tvwDOM
        '
        Me.tvwDOM.Dock = System.Windows.Forms.DockStyle.Top
        Me.tvwDOM.HideSelection = False
        Me.tvwDOM.ImageList = Me.ImageList1
        Me.tvwDOM.Location = New System.Drawing.Point(0, 0)
        Me.tvwDOM.Name = "tvwDOM"
        Me.tvwDOM.Size = New System.Drawing.Size(544, 192)
        Me.tvwDOM.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'objSlider
        '
        Me.objSlider.BackColor = System.Drawing.SystemColors.Control
        Me.objSlider.Cursor = System.Windows.Forms.Cursors.SizeNS
        Me.objSlider.Dock = System.Windows.Forms.DockStyle.Top
        Me.objSlider.Location = New System.Drawing.Point(0, 192)
        Me.objSlider.Name = "objSlider"
        Me.objSlider.Size = New System.Drawing.Size(544, 8)
        Me.objSlider.TabIndex = 1
        Me.objSlider.TabStop = False
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFile})
        '
        'mnuFile
        '
        Me.mnuFile.Index = 0
        Me.mnuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuOpenFile, Me.mnuFileSaveAs, Me.MenuItem1, Me.mnuExit})
        Me.mnuFile.Text = "&File"
        '
        'mnuOpenFile
        '
        Me.mnuOpenFile.Index = 0
        Me.mnuOpenFile.Text = "&Open HTML file..."
        '
        'mnuFileSaveAs
        '
        Me.mnuFileSaveAs.Index = 1
        Me.mnuFileSaveAs.Text = "&Save As..."
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 2
        Me.MenuItem1.Text = "-"
        '
        'mnuExit
        '
        Me.mnuExit.Index = 3
        Me.mnuExit.Text = "E&xit"
        '
        'OpenHtmlFileDialog
        '
        Me.OpenHtmlFileDialog.Filter = "HTML Files|*.html;*.htm"
        '
        'TreeNodeMenu
        '
        Me.TreeNodeMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuViewHTML, Me.mnuViewXHTML})
        '
        'mnuViewHTML
        '
        Me.mnuViewHTML.Index = 0
        Me.mnuViewHTML.Text = "View HTML"
        '
        'mnuViewXHTML
        '
        Me.mnuViewXHTML.Index = 1
        Me.mnuViewXHTML.Text = "View XHTML"
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.grdProperties)
        Me.pnlBottom.Controls.Add(Me.objSliderBottom)
        Me.pnlBottom.Controls.Add(Me.txtHTML)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBottom.Location = New System.Drawing.Point(0, 200)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(544, 103)
        Me.pnlBottom.TabIndex = 2
        '
        'grdProperties
        '
        Me.grdProperties.CommandsVisibleIfAvailable = True
        Me.grdProperties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdProperties.HelpVisible = False
        Me.grdProperties.LargeButtons = False
        Me.grdProperties.LineColor = System.Drawing.SystemColors.ScrollBar
        Me.grdProperties.Location = New System.Drawing.Point(248, 0)
        Me.grdProperties.Name = "grdProperties"
        Me.grdProperties.Size = New System.Drawing.Size(296, 103)
        Me.grdProperties.TabIndex = 2
        Me.grdProperties.Text = "PropertyGrid1"
        Me.grdProperties.ToolbarVisible = False
        Me.grdProperties.ViewBackColor = System.Drawing.SystemColors.Window
        Me.grdProperties.ViewForeColor = System.Drawing.SystemColors.WindowText
        '
        'objSliderBottom
        '
        Me.objSliderBottom.Cursor = System.Windows.Forms.Cursors.SizeWE
        Me.objSliderBottom.Dock = System.Windows.Forms.DockStyle.Left
        Me.objSliderBottom.Location = New System.Drawing.Point(240, 0)
        Me.objSliderBottom.Name = "objSliderBottom"
        Me.objSliderBottom.Size = New System.Drawing.Size(8, 103)
        Me.objSliderBottom.TabIndex = 1
        Me.objSliderBottom.TabStop = False
        '
        'txtHTML
        '
        Me.txtHTML.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtHTML.Location = New System.Drawing.Point(0, 0)
        Me.txtHTML.Multiline = True
        Me.txtHTML.Name = "txtHTML"
        Me.txtHTML.ReadOnly = True
        Me.txtHTML.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtHTML.Size = New System.Drawing.Size(240, 103)
        Me.txtHTML.TabIndex = 0
        Me.txtHTML.Text = ""
        '
        'SaveHtmlFileDialog
        '
        Me.SaveHtmlFileDialog.FileName = "doc1"
        Me.SaveHtmlFileDialog.Filter = "HTML Files|*.html;*.htm|XHTML Files|*.xml"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(552, 329)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.pnlBottom)
        Me.TabPage1.Controls.Add(Me.objSlider)
        Me.TabPage1.Controls.Add(Me.tvwDOM)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(544, 303)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "HTML"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.HtmlView1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(544, 303)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Layout"
        '
        'HtmlView1
        '
        Me.HtmlView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HtmlView1.Html = Nothing
        Me.HtmlView1.Location = New System.Drawing.Point(0, 0)
        Me.HtmlView1.Name = "HtmlView1"
        Me.HtmlView1.Size = New System.Drawing.Size(544, 303)
        Me.HtmlView1.TabIndex = 0
        '
        'TestForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(552, 329)
        Me.Controls.Add(Me.TabControl1)
        Me.Menu = Me.MainMenu1
        Me.Name = "TestForm"
        Me.Text = "Test Form"
        Me.pnlBottom.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    ' ProcessHTML(html)
    '
    ' This will process the given HTML string, and will populate the treeview. This
    ' method is called after the user has clicked the "Open HTML file..." menu item.

    Private Sub ProcessHTML(ByVal html As String)

        ' Clear the treeview

        tvwDOM.Nodes.Clear()

        ' Create an HtmlDocument (which parses the html)

        mDocument = HtmlDocument.Create(html, False)

        ' Populate the treeview with the document nodes

        BuildTree(mDocument.Nodes, tvwDOM.Nodes)

        ' Show the document

        HtmlView1.Html = html

    End Sub

    ' BuildTree(nodes,treeNodes)
    '
    ' This is used by the ProcessHTML method to iterate through the HtmlNodes and populate
    ' the treeview

    Private Function BuildTree(ByVal nodes As HtmlNodeCollection, ByVal treeNodes As TreeNodeCollection)
        Dim value As String = ""

        Dim node As HtmlNode
        For Each node In nodes
            Dim treeNode As New TreeNode(node.ToString())
            treeNode.Tag = node ' Keep the HtmlNode object in the tag (for when the user clicks on it)
            treeNodes.Add(treeNode)
            If TypeOf (node) Is HtmlElement Then
                treeNode.SelectedImageIndex = 0
                treeNode.ImageIndex = 0
                Me.BuildTree(CType(node, HtmlElement).Nodes, treeNode.Nodes)
            Else
                treeNode.Text = "(text)" ' This probably has carriage returns in, so don't render the actual HTML here
                treeNode.SelectedImageIndex = 1
                treeNode.ImageIndex = 1
            End If
        Next
    End Function

    ' objSlider_MouseMove(sender,e)
    '
    ' This is so the user can resize the top and bottom sections

    Private Sub objSlider_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles objSlider.MouseMove
        If e.Button And MouseButtons.Left Then
            tvwDOM.Height = e.Y + objSlider.Top
        End If
    End Sub

    ' objSliderBottom_MouseMove(sender,e)
    '
    ' This is so the user can resize the left and right controls in the bottom section

    Private Sub objSliderBottom_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles objSliderBottom.MouseMove
        If e.Button And MouseButtons.Left Then
            txtHTML.Width = txtHTML.Width + e.X '+ objSlider.Left
        End If
    End Sub

    ' mnuExit_Click(sender,e)
    '
    ' Quit this application

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        End
    End Sub

    ' mnuOpenFile_Click(sender,e)
    '
    ' This will open the file dialog and load the required HTML file

    Private Sub mnuOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOpenFile.Click
        If OpenHtmlFileDialog.ShowDialog(Me) = DialogResult.OK Then
            Try
                Dim sr As System.IO.StreamReader = New System.IO.StreamReader(OpenHtmlFileDialog.FileName, System.Text.Encoding.Default)
                Dim html As String = sr.ReadToEnd()
                sr.Close()
                ProcessHTML(html)
            Catch ex As Exception
                MsgBox("Sorry, I couldn't open that file for some reason.. try another one!")
            End Try
        End If
    End Sub

    ' tvwDOM_AfterSelect(sender,e)
    '
    ' This is called whenever the user selects a different node on the tree

    Private Sub tvwDOM_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwDOM.AfterSelect
        If tvwDOM.SelectedNode Is Nothing Then Exit Sub

        ' I'm just converting the HtmlNode to a string here, but you could easily
        ' use Typeof to determine its type, and then cast it to either HtmlText or
        ' HtmlElement. That way, you could present each of the attributes in a 
        ' properties control, for example.

        Dim node As HtmlNode = CType(tvwDOM.SelectedNode.Tag, HtmlNode)
        txtHTML.Text = node.HTML
        grdProperties.SelectedObject = node
        grdProperties.Text = node.GetType.ToString
    End Sub

    Private Sub mnuViewHTML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuViewHTML.Click
        If tvwDOM.SelectedNode Is Nothing Then Exit Sub
        Dim node As HtmlNode = CType(tvwDOM.SelectedNode.Tag, HtmlNode)
        ViewForm.ShowText(node.HTML)
    End Sub

    Private Sub mnuViewXHTML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuViewXHTML.Click
        If tvwDOM.SelectedNode Is Nothing Then Exit Sub
        Dim node As HtmlNode = CType(tvwDOM.SelectedNode.Tag, HtmlNode)
        ViewForm.ShowText(node.XHTML)
    End Sub

    Private Sub tvwDOM_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvwDOM.MouseDown
        Dim node As TreeNode = tvwDOM.GetNodeAt(New Point(e.X, e.Y))
        If node Is Nothing Then
            Exit Sub
        End If
        tvwDOM.SelectedNode = node
        If e.Button And MouseButtons.Right Then
            tvwDOM.ContextMenu = TreeNodeMenu
        Else
            tvwDOM.ContextMenu = Nothing
        End If
    End Sub

    Private Sub grdProperties_PropertyValueChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles grdProperties.PropertyValueChanged
        Dim node As HtmlNode
        node = grdProperties.SelectedObject
        If node Is Nothing Then Exit Sub
        If tvwDOM.SelectedNode Is Nothing Then Exit Sub
        tvwDOM.SelectedNode.Nodes.Clear()
        If TypeOf node Is HtmlElement Then
            tvwDOM.SelectedNode.Text = node.ToString
            BuildTree(CType(node, HtmlElement).Nodes, tvwDOM.SelectedNode.Nodes)
        Else
            tvwDOM.SelectedNode.Text = (Text)
        End If
    End Sub

    Private Sub mnuFileSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileSaveAs.Click
        If mDocument Is Nothing Then Exit Sub
        If SaveHtmlFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                Dim sw As System.IO.StreamWriter = System.IO.File.CreateText(SaveHtmlFileDialog.FileName)
                If SaveHtmlFileDialog.FilterIndex = 0 Then
                    sw.Write(mDocument.HTML)
                Else
                    sw.Write(mDocument.XHTML)
                End If
                sw.Close()
            Catch ex As Exception
                MsgBox("Sorry, I couldn't save that file for some reason.. try another one!")
            End Try
        End If
    End Sub

    Private Sub TestForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
