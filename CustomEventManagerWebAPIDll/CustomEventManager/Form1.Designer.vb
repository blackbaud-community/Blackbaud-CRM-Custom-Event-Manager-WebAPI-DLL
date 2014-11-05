<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.btnGetEvents = New System.Windows.Forms.Button()
        Me.lvEvents = New System.Windows.Forms.ListView()
        Me.lvEventExpenses = New System.Windows.Forms.ListView()
        Me.lblEventExpenses = New System.Windows.Forms.Label()
        Me.lblEvents = New System.Windows.Forms.Label()
        Me.btnAddExpense = New System.Windows.Forms.Button()
        Me.lblAvailableDatabases = New System.Windows.Forms.Label()
        Me.cbo_Databases = New System.Windows.Forms.ComboBox()
        Me.btnEditExpense = New System.Windows.Forms.Button()
        Me.btnDeleteExpense = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnGetEvents
        '
        Me.btnGetEvents.Enabled = False
        Me.btnGetEvents.Location = New System.Drawing.Point(770, 27)
        Me.btnGetEvents.Name = "btnGetEvents"
        Me.btnGetEvents.Size = New System.Drawing.Size(75, 23)
        Me.btnGetEvents.TabIndex = 0
        Me.btnGetEvents.Text = "Get Events"
        Me.btnGetEvents.UseVisualStyleBackColor = True
        '
        'lvEvents
        '
        Me.lvEvents.Location = New System.Drawing.Point(15, 53)
        Me.lvEvents.MultiSelect = False
        Me.lvEvents.Name = "lvEvents"
        Me.lvEvents.Size = New System.Drawing.Size(830, 121)
        Me.lvEvents.TabIndex = 1
        Me.lvEvents.UseCompatibleStateImageBehavior = False
        '
        'lvEventExpenses
        '
        Me.lvEventExpenses.Location = New System.Drawing.Point(15, 207)
        Me.lvEventExpenses.Name = "lvEventExpenses"
        Me.lvEventExpenses.Size = New System.Drawing.Size(830, 164)
        Me.lvEventExpenses.TabIndex = 2
        Me.lvEventExpenses.UseCompatibleStateImageBehavior = False
        '
        'lblEventExpenses
        '
        Me.lblEventExpenses.AutoSize = True
        Me.lblEventExpenses.Location = New System.Drawing.Point(12, 177)
        Me.lblEventExpenses.Name = "lblEventExpenses"
        Me.lblEventExpenses.Size = New System.Drawing.Size(71, 13)
        Me.lblEventExpenses.TabIndex = 3
        Me.lblEventExpenses.Text = "Expenses for "
        '
        'lblEvents
        '
        Me.lblEvents.AutoSize = True
        Me.lblEvents.Location = New System.Drawing.Point(12, 37)
        Me.lblEvents.Name = "lblEvents"
        Me.lblEvents.Size = New System.Drawing.Size(166, 13)
        Me.lblEvents.TabIndex = 4
        Me.lblEvents.Text = "Select Event to Display Expenses"
        '
        'btnAddExpense
        '
        Me.btnAddExpense.Enabled = False
        Me.btnAddExpense.Location = New System.Drawing.Point(576, 180)
        Me.btnAddExpense.Name = "btnAddExpense"
        Me.btnAddExpense.Size = New System.Drawing.Size(80, 25)
        Me.btnAddExpense.TabIndex = 5
        Me.btnAddExpense.Text = "Add Expense"
        Me.btnAddExpense.UseVisualStyleBackColor = True
        '
        'lblAvailableDatabases
        '
        Me.lblAvailableDatabases.AutoSize = True
        Me.lblAvailableDatabases.Location = New System.Drawing.Point(12, 9)
        Me.lblAvailableDatabases.Name = "lblAvailableDatabases"
        Me.lblAvailableDatabases.Size = New System.Drawing.Size(89, 13)
        Me.lblAvailableDatabases.TabIndex = 6
        Me.lblAvailableDatabases.Text = "Select Database:"
        '
        'cbo_Databases
        '
        Me.cbo_Databases.FormattingEnabled = True
        Me.cbo_Databases.Location = New System.Drawing.Point(107, 6)
        Me.cbo_Databases.Name = "cbo_Databases"
        Me.cbo_Databases.Size = New System.Drawing.Size(224, 21)
        Me.cbo_Databases.TabIndex = 7
        '
        'btnEditExpense
        '
        Me.btnEditExpense.Enabled = False
        Me.btnEditExpense.Location = New System.Drawing.Point(662, 180)
        Me.btnEditExpense.Name = "btnEditExpense"
        Me.btnEditExpense.Size = New System.Drawing.Size(80, 25)
        Me.btnEditExpense.TabIndex = 8
        Me.btnEditExpense.Text = "Edit Expense"
        Me.btnEditExpense.UseVisualStyleBackColor = True
        '
        'btnDeleteExpense
        '
        Me.btnDeleteExpense.Enabled = False
        Me.btnDeleteExpense.Location = New System.Drawing.Point(748, 180)
        Me.btnDeleteExpense.Name = "btnDeleteExpense"
        Me.btnDeleteExpense.Size = New System.Drawing.Size(97, 25)
        Me.btnDeleteExpense.TabIndex = 9
        Me.btnDeleteExpense.Text = "Delete Expense"
        Me.btnDeleteExpense.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(850, 378)
        Me.Controls.Add(Me.btnDeleteExpense)
        Me.Controls.Add(Me.btnEditExpense)
        Me.Controls.Add(Me.cbo_Databases)
        Me.Controls.Add(Me.lblAvailableDatabases)
        Me.Controls.Add(Me.btnAddExpense)
        Me.Controls.Add(Me.lblEvents)
        Me.Controls.Add(Me.lblEventExpenses)
        Me.Controls.Add(Me.lvEventExpenses)
        Me.Controls.Add(Me.lvEvents)
        Me.Controls.Add(Me.btnGetEvents)
        Me.Name = "Form1"
        Me.Text = "AppFxWebService Custom Event Manager"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnGetEvents As System.Windows.Forms.Button
    Friend WithEvents lvEvents As System.Windows.Forms.ListView
    Friend WithEvents lvEventExpenses As System.Windows.Forms.ListView
    Friend WithEvents lblEventExpenses As System.Windows.Forms.Label
    Friend WithEvents lblEvents As System.Windows.Forms.Label
    Friend WithEvents btnAddExpense As System.Windows.Forms.Button
    Friend WithEvents lblAvailableDatabases As System.Windows.Forms.Label
    Friend WithEvents cbo_Databases As System.Windows.Forms.ComboBox
    Friend WithEvents btnEditExpense As System.Windows.Forms.Button
    Friend WithEvents btnDeleteExpense As System.Windows.Forms.Button

End Class
