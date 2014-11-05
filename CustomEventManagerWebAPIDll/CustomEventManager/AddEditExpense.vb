Public Class AddEditExpense

    'The Blackbaud.AppFx.WebAPI.dll is used to access the Infinity application such as 
    ' Blackbaud Enterprise CRM (BBEC), Altru, and Research Point (RP) and handles communication with the 
    ' AppFxWebService.asmx on your behalf.  If you use this dll to access the Infinity application 
    ' you do NOT need a web service reference to AppFxWebService.asmx in your visual studio project. 
    'Declare a variable of type Blackbaud.AppFx.WebAPI.ServiceProxy.AppFxWebService to represent
    ' your proxy for the communication mechanism to Infinity application.
    Private _appFx As Blackbaud.AppFx.WebAPI.ServiceProxy.AppFxWebService

    'You will need to set credentials for the web service.
    Private _myCred As System.Net.ICredentials

    'ClientAppInfoHeader will be used to hold the client application name that 
    ' identifies your custom client software for auditing purposes within the Infinty database.  It also holds
    ' a database identifier to point to the correct database.
    Private _clientAppInfoHeader As Blackbaud.AppFx.WebAPI.ServiceProxy.ClientAppInfoHeader

    Private _eventID, _eventExpenseID As String
    Private _eventName As String
    Private _vendorID As String
    Private _searchConstituentName As String = ""
    Private _searchTextChanged As Boolean = False
    Private _addEditMode As AddEditMode = AddEditMode.Add

    Enum AddEditMode
        Add = 1
        Edit = 2
    End Enum


    Public Sub New(EventID As String, EventName As String, AppFx As Blackbaud.AppFx.WebAPI.ServiceProxy.AppFxWebService, MyCred As System.Net.ICredentials, ClientAppInfoHeader As Blackbaud.AppFx.WebAPI.ServiceProxy.ClientAppInfoHeader, AddEditMode As AddEditMode, Optional EventExpenseID As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _appFx = AppFx
        _myCred = MyCred
        _clientAppInfoHeader = ClientAppInfoHeader

        _eventID = EventID
        _eventName = EventName
        _addEditMode = AddEditMode

        If _addEditMode = AddEditExpense.AddEditMode.Edit And EventExpenseID = "" Then
            Throw New Exception("Placing the AddEditExpense winform into edit mode via the AddEditMode parameter requires a value for the EventExpenseID")
        Else
            _eventExpenseID = EventExpenseID
        End If

        InitializeAddEditExpenseForm()
    End Sub

    Private Sub InitializeAddEditExpenseForm()

        If _addEditMode = AddEditExpense.AddEditMode.Edit Then
            Me.Text = "Edit Expense"
        ElseIf _addEditMode = AddEditExpense.AddEditMode.Add Then
            Me.Text = "Add Expense"
        End If

        'Review the feature metadata verify if a data form's form fields use uses any code tables to populate
        ' a drop down list box.  If so, grab the DATABASE table name for the code table. 
        ' Note: THIS IS NOT THE friendly code table name but the DATABASE TABLE NAME
        'Code Table: Event Expense Type
        'DATABASE tabe name:  EVENTEXPENSETYPECODE
        'Description:  Stores translations of event expense types.
        'System Record ID: be73844b-0990-49ed-bb4e-4011eee23e4a
        DisplayCodeTableEntryGetListReplyinComboBox(RetrieveCodeTable("EVENTEXPENSETYPECODE"), _
                                                    cboEventExpenseTypes)

        Me.lblEventID.Text = _eventID
        Me.lblEventName.Text = _eventName
        Me.lblEventExpenseID.Text = _eventExpenseID

        'If the form is in edit mode then we need to call an edit data form "load" to retrieve the
        ' expense for the provided expense id.  
        'When save is selected, we will call the edit data form "save" to update (not add) the expense row within
        ' the database.  
        If _addEditMode = AddEditMode.Edit Then
            'Let's automate the appropriate edit data form feature which is used to update an event expense row for an event.
            ' Feature Name:         Event Expense Edit Form
            ' System Record ID:     7e655536-771c-4249-b366-0f647db170df 
            'The feature's metadata can be obtained by placing the click once shell into design mode via Tools\Design Mode.
            '  Once the shell is in design mode, you can select the "Go to data form instance" button just above the feature.
            '  The system will display the metadata page for the feature.  From the metadata page, you can view the feature's
            '  name within the caption or the system record id near the top of the page's summary section.  
            'Alternatively, you can search for the feature via Administration > Application > Features > Data Form Search.
            '  Searching for the feature involves an additional step of verifying the feature is the same feature used by
            '  end users.  Once you have searched for the feature, use the "Page References" and "Task References" tabs to 
            '  verify that the dataform is referenced by a page or task that the end user is using. 

            'We will call 
            Try
                'Display hourglass during appfx web service calls
                Cursor.Current = Cursors.WaitCursor
                Cursor.Show()

                Dim Req As New Blackbaud.AppFx.WebAPI.ServiceProxy.DataFormLoadRequest
                Dim Reply As Blackbaud.AppFx.WebAPI.ServiceProxy.DataFormLoadReply

                'When editing a record via the dataformloadrequest you should supply the unique
                ' identifier for the row you want to edit. 
                'Use the RecordID property to identify the row.
                Req.RecordID = _eventExpenseID
                Req.FormName = "Event Expense Edit Form"
                'Req.FormID = New System.Guid(" 7e655536-771c-4249-b366-0f647db170df ")

                Req.ClientAppInfo = _clientAppInfoHeader
                Reply = _appFx.DataFormLoad(Req)

                DisplayFormFieldsForEdit(Reply.DataFormItem.Values)

            Catch exSoap As System.Web.Services.Protocols.SoapException
                'If an error occurs we attach a SOAP fault error header.
                'You can check the proxy.ResponseErrorHeaderValue to get rich
                'error information including a nicer message copared to the raw exception message.
                Dim wsMsg As String
                If _appFx.ResponseErrorHeaderValue IsNot Nothing Then
                    wsMsg = _appFx.ResponseErrorHeaderValue.ErrorText
                Else
                    wsMsg = exSoap.Message
                End If

            Catch ex As Exception
                MsgBox(ex.Message.ToString)

            Finally
                'Hide hourglass after api call
                Cursor.Current = Cursors.Default
                Cursor.Show()
            End Try
        End If
    End Sub

    Private Sub DisplayFormFieldsForEdit(ByVal DataFormFieldValueSet As Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValueSet)

        cboEventExpenseTypes.Text = DataFormFieldValueSet.Item("EVENTEXPENSETYPECODEID").ValueTranslation
        txtConstituentSearch.Text = DataFormFieldValueSet.Item("VENDORID").ValueTranslation
        _searchConstituentName = DataFormFieldValueSet.Item("VENDORID").ValueTranslation

        If IsNothing(DataFormFieldValueSet.Item("VENDORID").Value) Then
            _vendorID = ""
        Else
            _vendorID = DataFormFieldValueSet.Item("VENDORID").Value.ToString
        End If


        txtBudgetedAmt.Text = DataFormFieldValueSet.Item("BUDGETEDAMOUNT").Value
        txtAmountPaid.Text = DataFormFieldValueSet.Item("AMOUNTPAID").Value
        dtmAmountPaid.Text = DataFormFieldValueSet.Item("DATEPAID").Value
        txtComment.Text = DataFormFieldValueSet.Item("COMMENT").Value
    End Sub

    Private Function RetrieveCodeTable(DatabaseCodeTableName As String) As Blackbaud.AppFx.WebAPI.ServiceProxy.CodeTableEntryGetListReply
        Try
            'Display hourglass during appfx web service calls
            Cursor.Current = Cursors.WaitCursor
            Cursor.Show()

            Dim Req As New Blackbaud.AppFx.WebAPI.ServiceProxy.CodeTableEntryGetListRequest
            Dim Reply As Blackbaud.AppFx.WebAPI.ServiceProxy.CodeTableEntryGetListReply

            Req.ClientAppInfo = _clientAppInfoHeader
            Req.ReturnListSortMethod = True

            'Review the feature metadata verify if a data form's form fields use uses any code tables to populate
            ' a drop down list box.  If so, grab the DATABASE table name for the code table. 
            ' Note: THIS IS NOT THE friendly code table name but the DATABASE TABLE NAME
            Req.CodeTableName = DatabaseCodeTableName


            'The API utilizes a Request-Response pattern.  The pattern consists of request-response pairs.
            ' An operation is called on the proxy, such as CodeTableEntryGetList.  The request is passed to the operation.
            'A reply is received from the proxy.  Each request and reply type is tailored to the operation.
            ' So, the proxy's CodeTableEntryGetList operation will require a  Blackbaud.AppFx.WebAPI.ServiceProxy.CodeTableEntryGetListRequest and 
            ' will return a Blackbaud.AppFx.WebAPI.ServiceProxy.CodeTableEntryGetListReply.
            'Since we need to retrieve a code table's values we will call the proxy's CodeTableEntryGetList operation
            'Following the Request-Response pattern, we package up a request (CodeTableEntryGetListRequest),
            'pass the request variable to the DataListLoad operation and receive a response (CodeTableEntryGetListReply)
            Reply = _appFx.CodeTableEntryGetList(Req)

            Return Reply

        Catch exSoap As System.Web.Services.Protocols.SoapException
            'If an error occurs we attach a SOAP fault error header.
            'You can check the proxy.ResponseErrorHeaderValue to get rich
            'error information including a nicer message copared to the raw exception message.
            Dim wsMsg As String
            If _appFx.ResponseErrorHeaderValue IsNot Nothing Then
                wsMsg = _appFx.ResponseErrorHeaderValue.ErrorText
            Else
                wsMsg = exSoap.Message
            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            Return Nothing
        Finally
            'Hide hourglass after api call
            Cursor.Current = Cursors.Default
            Cursor.Show()
        End Try

    End Function


    Private Sub DisplayCodeTableEntryGetListReplyinComboBox(Reply As Blackbaud.AppFx.WebAPI.ServiceProxy.CodeTableEntryGetListReply, cbo As Windows.Forms.ComboBox)
        cbo.DisplayMember = "Display"
        cbo.ValueMember = "Value"

        Dim CodeTableItems As System.Collections.ArrayList = New System.Collections.ArrayList

        For Each Row As Blackbaud.AppFx.WebAPI.ServiceProxy.TableEntryListOutputRow In Reply.Rows
            CodeTableItems.Add(New CodeTableItem(Row.ID.ToString, Row.Code))
        Next

        Me.cboEventExpenseTypes.DataSource = CodeTableItems
    End Sub

    Private Class CodeTableItem
        Dim _value, _display As String

        Public Sub New(Value As String, Display As String)

            _value = Value
            _display = Display

        End Sub

        Public ReadOnly Property Value As String
            Get
                Return _value
            End Get
        End Property

        Public ReadOnly Property Display As String
            Get
                Return _display
            End Get
        End Property

    End Class

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        SaveExpense()
    End Sub

    Private Sub SaveExpense()
        Try
            'Display hourglass during appfx web service calls
            Cursor.Current = Cursors.WaitCursor
            Cursor.Show()

            Dim Req As New Blackbaud.AppFx.WebAPI.ServiceProxy.DataFormSaveRequest
            Dim Reply As Blackbaud.AppFx.WebAPI.ServiceProxy.DataFormSaveReply
            Dim fvSet As New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValueSet
            Dim dfi As New Blackbaud.AppFx.XmlTypes.DataForms.DataFormItem

            If _addEditMode = AddEditMode.Add Then
                'A data form may require a context value. Think of a context value as a "parent id".
                ' In the example below we are adding an expense FOR AN EVENT.  Therefore, the EventID would be 
                ' the parent id.  
                'Context is optional for a data form.  A data form may not need a context value.  
                'To determine whether a data form requires a context value, 
                'look at for a <Context> xml element within the xml for the data form.  
                ' Example: <Context ContextRecordType="Event" RecordIDParameter="EVENTID" />
                Req.FormName = "Event Expense Add Form"
                Req.ContextRecordID = _eventID

                'fvSet (Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValueSet) holds a collection of type  
                ' Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue.  
                'Each Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue represents a form field.  
                'For data forms, form fields are used as to hold end user data entry values.  
                'Once the DataFormSave operation is perfomed on the proxy, form field values
                ' are passed to the web server and processed by data form's business logic layer (sp or clr code).
                'The appropriate form field values can be determined by looking at the XML <FormField> child elements within the 
                ' <FormMetaData> element of the xml within the data form.  
                'Be sure you adhere the Required attributes on the XML <FormField> element of the xml within the data form.  
                ' If the Required attribure = True then you will need to provide a corresponding DataFormFieldValue 
                'in the DataFormFieldValueSet

                'DataFormFieldValue's ID property represents the FieldID attribute within <FormField> element of the xml within the data form.

                fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "SELECTEDEVENTID", .Value = _eventID})
                fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "EVENTEXPENSETYPECODEID", .Value = cboEventExpenseTypes.SelectedValue.ToString})
                fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "VENDORID", .Value = _vendorID})
                fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "BUDGETEDAMOUNT", .Value = txtBudgetedAmt.Text.ToString})
                fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "AMOUNTPAID", .Value = txtAmountPaid.Text.ToString})
                fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "DATEPAID", .Value = dtmAmountPaid.Value.ToString})
                fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "COMMENT", .Value = txtComment.Text.ToString})

                dfi.Values = fvSet
                Req.DataFormItem = dfi

            ElseIf _addEditMode = AddEditMode.Edit Then
                'To save the edit we call an Edit Data Form rather than an Add Data Form
                Req.FormName = "Event Expense Edit Form"

                'Unlike the add, we don't need a context value.  
                ' Instead, we use the ID property to identify the row being updated.
                Req.ID = _eventExpenseID

                fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "EVENTEXPENSETYPECODEID", .Value = cboEventExpenseTypes.SelectedValue.ToString})
                fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "VENDORID", .Value = _vendorID})
                fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "BUDGETEDAMOUNT", .Value = txtBudgetedAmt.Text.ToString})
                fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "AMOUNTPAID", .Value = txtAmountPaid.Text.ToString})
                fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "DATEPAID", .Value = dtmAmountPaid.Value.ToString})
                fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "COMMENT", .Value = txtComment.Text.ToString})

                dfi.Values = fvSet
                Req.DataFormItem = dfi

            End If

            'The API utilizes a Request-Response pattern.  The pattern consists of request-response pairs.
            ' An operation is called on the proxy, such as DataFormSave.  The request is passed to the operation.
            'A reply is received from the proxy.  Each request and reply type is tailored to the operation.
            ' So, the proxy's DataFormSave operation will require a  
            'Blackbaud.AppFx.WebAPI.ServiceProxy.DataFormSaveRequest and will return a 
            'Blackbaud.AppFx.WebAPI.ServiceProxy.DataFormSaveReply.
            'Since we need to 'save' a dataform up with data we will call the proxy's DataFormSave operation
            'Following the Request-Response pattern, we package up a request (DataFormSaveRequest),
            'pass the request variable to the DataListLoad operation and receive a response (DataFormSaveReply)
            Req.ClientAppInfo = _clientAppInfoHeader
            Reply = _appFx.DataFormSave(Req)

        Catch exSoap As System.Web.Services.Protocols.SoapException
            'If an error occurs we attach a SOAP fault error header.
            'You can check the proxy.ResponseErrorHeaderValue to get rich
            'error information including a nicer message copared to the raw exception message.
            Dim wsMsg As String
            If _appFx.ResponseErrorHeaderValue IsNot Nothing Then
                wsMsg = _appFx.ResponseErrorHeaderValue.ErrorText
            Else
                wsMsg = exSoap.Message
            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString)

        Finally
            'Hide hourglass after api call
            Cursor.Current = Cursors.Default
            Cursor.Show()
            Me.Close()
        End Try
    End Sub


    Private Sub picVendorSearch_Click(sender As System.Object, e As System.EventArgs) Handles picVendorSearch.Click
        VendorSearch()
    End Sub

    Private Sub VendorSearch()
        'create a request to invoke the Constituent Search
        Try
            'Display hourglass during appfx web service calls
            Cursor.Current = Cursors.WaitCursor
            Cursor.Show()

            Dim req As New Blackbaud.AppFx.WebAPI.ServiceProxy.SearchListLoadRequest
            req.ClientAppInfo = _clientAppInfoHeader

            'You can refer to the feature using the feature's system record id or name
            'req.SearchListID = New Guid("23C5C603-D7D8-4106-AECC-65392B563887")
            req.SearchListName = "Constituent Search"

            'create a field value set to hold any filters we want to pass in

            'If your searchlist has filters, you may need to provide a value for one or several of the
            ' form fields which serve as the parameters for the search list
            ' Try out the feature's parameters within the application to get a feel for each of the search list's parameters
            ' Tip:  See the <FormMetaData> tag within the SearchListSpec's xml to view the <FormField> tags that declare the
            ' parameters for the search list.  Or see the Fields tab on the Search List's metadata page.   
            ' For 3rd party Altru developers, Blackbaud Professional Services will need to permission the API developer 
            ' with rights to view the feature metadata features.

            'fvSet (Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValueSet) holds a collection of type  
            ' Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue.  Each Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue
            ' represents a form field.  For search lists, form fields are used as parameters.


            Dim fvSet As New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValueSet
            Dim dfi As New Blackbaud.AppFx.XmlTypes.DataForms.DataFormItem

            'Add an item to the collection of the DataFormFieldValueSet.  
            ' Use the form field's/parameter's FieldID value for the DataFormFieldValue.ID and
            ' a value via the DataFormFieldValue.Value
            'DataFormFieldValue's ID property represents the FieldID attribute within <FormField> element of the xml searchlistspec
            'add a field value for each filter parameter.  In this case, we're using the quickfind feature and passing in the FULLNAME.  Note that
            'we also have to include to search only primary addresses, to avoid getting multiple rows in the output if the record has multiple 
            'addresses.

            fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "FULLNAME", .Value = txtConstituentSearch.Text})
            fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "ONLYPRIMARYADDRESS", .Value = True})

            'create a dataform item to contain the filter field value set
            dfi.Values = fvSet

            'pass the filter dataform item to the request 
            req.Filter = dfi

            'invoke the search
            Dim reply As Blackbaud.AppFx.WebAPI.ServiceProxy.SearchListLoadReply = _appFx.SearchListLoad(req)

            Dim selectedRow As Blackbaud.AppFx.WebAPI.ServiceProxy.ListOutputRow = Nothing
            If reply.Output.RowCount = 0 Then
                MsgBox("No constituent records were found.", MsgBoxStyle.Information)
            ElseIf reply.Output.RowCount > 1 Then
                Using f As New SelectSearchResultsForm(_appFx, _clientAppInfoHeader, req.SearchListName, reply)
                    If f.ShowDialog = Windows.Forms.DialogResult.OK Then
                        selectedRow = f.SelectedRow
                    End If
                End Using
            Else
                selectedRow = reply.Output.Rows(0)
            End If

            If selectedRow IsNot Nothing Then
                Dim recordID As Guid = New Guid(selectedRow.Values(0))
                Dim name As String = selectedRow.Values(1)
                txtConstituentSearch.Text = name

                _searchConstituentName = name
                lblConstituentSearchID.Text = recordID.ToString
                _vendorID = recordID.ToString
                _searchTextChanged = False
            End If

        Catch exSoap As System.Web.Services.Protocols.SoapException
            'If an error occurs we attach a SOAP fault error header.
            'You can check the proxy.ResponseErrorHeaderValue to get rich
            'error information including a nicer message copared to the raw exception message.
            Dim wsMsg As String
            If _appFx.ResponseErrorHeaderValue IsNot Nothing Then
                wsMsg = _appFx.ResponseErrorHeaderValue.ErrorText
            Else
                wsMsg = exSoap.Message
            End If

            MsgBox(wsMsg)

        Catch ex As Exception

            MsgBox(ex.Message)
        Finally
            'Hide hourglass after api call
            Cursor.Current = Cursors.Default
            Cursor.Show()
        End Try
    End Sub

    Private Sub txtConstituentSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtConstituentSearch.TextChanged
        If txtConstituentSearch.Text <> _searchConstituentName Then
            _searchTextChanged = True
        Else
            _searchTextChanged = False
        End If
    End Sub

    Private Sub AddEditExpense_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class