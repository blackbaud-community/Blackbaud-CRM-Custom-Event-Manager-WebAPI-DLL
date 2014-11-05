<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddEditExpense
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddEditExpense))
        Me.cboEventExpenseTypes = New System.Windows.Forms.ComboBox()
        Me.lblEventExpenseTypes = New System.Windows.Forms.Label()
        Me.lblEventID = New System.Windows.Forms.Label()
        Me.lblEventName = New System.Windows.Forms.Label()
        Me.txtConstituentSearch = New System.Windows.Forms.TextBox()
        Me.picVendorSearch = New System.Windows.Forms.PictureBox()
        Me.lblVendor = New System.Windows.Forms.Label()
        Me.lblBudgetedAmt = New System.Windows.Forms.Label()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.lblComment = New System.Windows.Forms.Label()
        Me.lblAmountPaid = New System.Windows.Forms.Label()
        Me.dtmAmountPaid = New System.Windows.Forms.DateTimePicker()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.txtBudgetedAmt = New System.Windows.Forms.TextBox()
        Me.txtAmountPaid = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblConstituentSearchID = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblEventExpenseID = New System.Windows.Forms.Label()
        CType(Me.picVendorSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboEventExpenseTypes
        '
        Me.cboEventExpenseTypes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboEventExpenseTypes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboEventExpenseTypes.FormattingEnabled = True
        Me.cboEventExpenseTypes.Location = New System.Drawing.Point(117, 38)
        Me.cboEventExpenseTypes.Name = "cboEventExpenseTypes"
        Me.cboEventExpenseTypes.Size = New System.Drawing.Size(229, 21)
        Me.cboEventExpenseTypes.TabIndex = 0
        '
        'lblEventExpenseTypes
        '
        Me.lblEventExpenseTypes.AutoSize = True
        Me.lblEventExpenseTypes.Location = New System.Drawing.Point(80, 41)
        Me.lblEventExpenseTypes.Name = "lblEventExpenseTypes"
        Me.lblEventExpenseTypes.Size = New System.Drawing.Size(34, 13)
        Me.lblEventExpenseTypes.TabIndex = 1
        Me.lblEventExpenseTypes.Text = "Type:"
        '
        'lblEventID
        '
        Me.lblEventID.AutoSize = True
        Me.lblEventID.Location = New System.Drawing.Point(367, 6)
        Me.lblEventID.Name = "lblEventID"
        Me.lblEventID.Size = New System.Drawing.Size(46, 13)
        Me.lblEventID.TabIndex = 2
        Me.lblEventID.Text = "EventID"
        Me.lblEventID.Visible = False
        '
        'lblEventName
        '
        Me.lblEventName.AutoSize = True
        Me.lblEventName.Location = New System.Drawing.Point(123, 6)
        Me.lblEventName.Name = "lblEventName"
        Me.lblEventName.Size = New System.Drawing.Size(63, 13)
        Me.lblEventName.TabIndex = 3
        Me.lblEventName.Text = "EventName"
        Me.lblEventName.Visible = False
        '
        'txtConstituentSearch
        '
        Me.txtConstituentSearch.Location = New System.Drawing.Point(117, 65)
        Me.txtConstituentSearch.Name = "txtConstituentSearch"
        Me.txtConstituentSearch.Size = New System.Drawing.Size(213, 20)
        Me.txtConstituentSearch.TabIndex = 5
        '
        'picVendorSearch
        '
        Me.picVendorSearch.Image = CType(resources.GetObject("picVendorSearch.Image"), System.Drawing.Image)
        Me.picVendorSearch.Location = New System.Drawing.Point(329, 65)
        Me.picVendorSearch.Name = "picVendorSearch"
        Me.picVendorSearch.Size = New System.Drawing.Size(17, 20)
        Me.picVendorSearch.TabIndex = 6
        Me.picVendorSearch.TabStop = False
        '
        'lblVendor
        '
        Me.lblVendor.AutoSize = True
        Me.lblVendor.Location = New System.Drawing.Point(70, 68)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(44, 13)
        Me.lblVendor.TabIndex = 7
        Me.lblVendor.Text = "Vendor:"
        '
        'lblBudgetedAmt
        '
        Me.lblBudgetedAmt.AutoSize = True
        Me.lblBudgetedAmt.Location = New System.Drawing.Point(19, 94)
        Me.lblBudgetedAmt.Name = "lblBudgetedAmt"
        Me.lblBudgetedAmt.Size = New System.Drawing.Size(95, 13)
        Me.lblBudgetedAmt.TabIndex = 9
        Me.lblBudgetedAmt.Text = "Budgeted Amount:"
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(117, 139)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(229, 64)
        Me.txtComment.TabIndex = 25
        Me.txtComment.Text = "This is a sample comment."
        '
        'lblComment
        '
        Me.lblComment.AutoSize = True
        Me.lblComment.Location = New System.Drawing.Point(60, 142)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(54, 13)
        Me.lblComment.TabIndex = 12
        Me.lblComment.Text = "Comment:"
        '
        'lblAmountPaid
        '
        Me.lblAmountPaid.AutoSize = True
        Me.lblAmountPaid.Location = New System.Drawing.Point(41, 120)
        Me.lblAmountPaid.Name = "lblAmountPaid"
        Me.lblAmountPaid.Size = New System.Drawing.Size(70, 13)
        Me.lblAmountPaid.TabIndex = 13
        Me.lblAmountPaid.Text = "Amount Paid:"
        '
        'dtmAmountPaid
        '
        Me.dtmAmountPaid.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtmAmountPaid.Location = New System.Drawing.Point(183, 116)
        Me.dtmAmountPaid.Name = "dtmAmountPaid"
        Me.dtmAmountPaid.Size = New System.Drawing.Size(100, 20)
        Me.dtmAmountPaid.TabIndex = 20
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(352, 180)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 30
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'txtBudgetedAmt
        '
        Me.txtBudgetedAmt.Location = New System.Drawing.Point(117, 91)
        Me.txtBudgetedAmt.Name = "txtBudgetedAmt"
        Me.txtBudgetedAmt.Size = New System.Drawing.Size(60, 20)
        Me.txtBudgetedAmt.TabIndex = 10
        '
        'txtAmountPaid
        '
        Me.txtAmountPaid.Location = New System.Drawing.Point(117, 116)
        Me.txtAmountPaid.Name = "txtAmountPaid"
        Me.txtAmountPaid.Size = New System.Drawing.Size(60, 20)
        Me.txtAmountPaid.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(309, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Event ID: "
        Me.Label1.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Constituent Vendor ID:"
        Me.Label2.Visible = False
        '
        'lblConstituentSearchID
        '
        Me.lblConstituentSearchID.AutoSize = True
        Me.lblConstituentSearchID.Location = New System.Drawing.Point(123, 19)
        Me.lblConstituentSearchID.Name = "lblConstituentSearchID"
        Me.lblConstituentSearchID.Size = New System.Drawing.Size(71, 13)
        Me.lblConstituentSearchID.TabIndex = 29
        Me.lblConstituentSearchID.Text = "ConstituentID"
        Me.lblConstituentSearchID.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(48, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 13)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Event Name: "
        Me.Label3.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(247, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 13)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Event Expense ID: "
        Me.Label4.Visible = False
        '
        'lblEventExpenseID
        '
        Me.lblEventExpenseID.AutoSize = True
        Me.lblEventExpenseID.Location = New System.Drawing.Point(349, 19)
        Me.lblEventExpenseID.Name = "lblEventExpenseID"
        Me.lblEventExpenseID.Size = New System.Drawing.Size(87, 13)
        Me.lblEventExpenseID.TabIndex = 32
        Me.lblEventExpenseID.Text = "EventExpenseID"
        Me.lblEventExpenseID.Visible = False
        '
        'AddEditExpense
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(443, 215)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblEventExpenseID)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblConstituentSearchID)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtAmountPaid)
        Me.Controls.Add(Me.txtBudgetedAmt)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.dtmAmountPaid)
        Me.Controls.Add(Me.lblAmountPaid)
        Me.Controls.Add(Me.lblComment)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.lblBudgetedAmt)
        Me.Controls.Add(Me.lblVendor)
        Me.Controls.Add(Me.picVendorSearch)
        Me.Controls.Add(Me.txtConstituentSearch)
        Me.Controls.Add(Me.lblEventName)
        Me.Controls.Add(Me.lblEventID)
        Me.Controls.Add(Me.lblEventExpenseTypes)
        Me.Controls.Add(Me.cboEventExpenseTypes)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "AddEditExpense"
        Me.Text = "Add Expense"
        CType(Me.picVendorSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboEventExpenseTypes As System.Windows.Forms.ComboBox
    Friend WithEvents lblEventExpenseTypes As System.Windows.Forms.Label
    Friend WithEvents lblEventID As System.Windows.Forms.Label
    Friend WithEvents lblEventName As System.Windows.Forms.Label
    Friend WithEvents txtConstituentSearch As System.Windows.Forms.TextBox
    Friend WithEvents picVendorSearch As System.Windows.Forms.PictureBox
    Friend WithEvents lblVendor As System.Windows.Forms.Label
    Friend WithEvents lblBudgetedAmt As System.Windows.Forms.Label
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents lblComment As System.Windows.Forms.Label
    Friend WithEvents lblAmountPaid As System.Windows.Forms.Label
    Friend WithEvents dtmAmountPaid As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents txtBudgetedAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtAmountPaid As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblConstituentSearchID As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblEventExpenseID As System.Windows.Forms.Label
End Class
