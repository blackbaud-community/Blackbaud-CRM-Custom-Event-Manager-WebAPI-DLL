
'To cut down on the length of the class names you could import the Blackbaud.AppFx.WebAPI namespace
'Within variable declaration, class names shown in full for learning purposes. 
'Imports Blackbaud.AppFx.WebAPI

Public Class Form1

    'The Blackbaud.AppFx.WebAPI.dll is used to access the Infinity application such as 
    ' Blackbaud Enterprise CRM (BBEC), Altru, and Research Point (RP) and handles communication with the 
    ' AppFxWebService.asmx on your behalf.  If you use this dll to access the Infinity application 
    ' you do NOT need a web service reference to AppFxWebService.asmx.  
    'Declare a variable of type Blackbaud.AppFx.WebAPI.ServiceProxy.AppFxWebService to represent
    ' your proxy for the communication mechanism to Infinity applicaiton.
    Private _appFx As Blackbaud.AppFx.WebAPI.ServiceProxy.AppFxWebService
    'You will need to set credentials for the web service.
    Private _myCred As System.Net.ICredentials
    'ClientAppInfoHeader will be used to hold the client application name that 
    ' identifies your custom client software for auditing purposes within the Infinty database.  It also holds
    ' a database identifier to help point to the correct database.
    Private _clientAppInfoHeader As Blackbaud.AppFx.WebAPI.ServiceProxy.ClientAppInfoHeader

    'Variables used to track the event info
    Private _eventsLoaded As Boolean = False
    Private _eventID As String = ""
    Private _eventName As String = ""
    Private _eventExpenseID As String = ""

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        InitializeAppFxWebService()

    End Sub

    Private Sub InitializeAppFxWebService()

        Try
            'Display hourglass during appfx web service calls
            Cursor.Current = Cursors.WaitCursor
            Cursor.Show()

            'Instantiate the proxy to the Infinity application
            _appFx = New Blackbaud.AppFx.WebAPI.ServiceProxy.AppFxWebService

            'Grab the network credentials.  See GetNetworkCredentials() for details.
            _myCred = GetNetworkCredentials()

            'Set the credentials for the service proxy.
            _appFx.Credentials = _myCred

            'Be sure to store the url to the AppFxWebService.asmx in a configuration 
            ' file on your client.  Don't hard code the url.
            'AppFxWebService.asmx is the single SOAP endpoint for accessing all 
            ' application features.
            'Grab the url from the Blackbaud AppFX Server HTTP Endpoint Reference 
            ' (endpointhelp.html).  For Altru, a link to the endpointhelp.html is available via the Resources for Developers
            'link in the top right corner of the branding splash/candystore page
            _appFx.Url = "https://altrurig01bo3.blackbaudhosting.com/5740Altru_9a731bb7-0e50-48e3-b8c3-e03e16a5ac15/appfxwebservice.asmx"
            '_appFx.Url = "http://localhost/bbappfx/appfxwebservice.asmx"

            'Provide the ClientAppName which will be logged in the Infinity database and 
            ' used for auditing purposes.
            'Use a client application name that identifies your custom client software 
            ' from the web shell and any other client user interfaces.
            _clientAppInfoHeader = New Blackbaud.AppFx.WebAPI.ServiceProxy.ClientAppInfoHeader
            _clientAppInfoHeader.ClientAppName = "CustomEventManager"

            '****************************************
            '****** PROVIDING A DATABASE KEY  *******
            '****************************************
            ' Before you can consume features with the API, a developer should provide a key which
            ' identifies the database.  There are two options when obtaining a database key:
            '   1) Manually obtain a database key, store the database key in a config file, 
            '      and retreive database key at runtime
            '   2) Retrieve a listing of databases associated with the Infinity application at run time and present the listing
            '      of databases to the end user.  Let the end user pick the appropriate database.

            '**********************************************************************************************
            '**** Option 1A - Hosted:  Manually Providing a GUID for Hosting's SiteManager (ALTRU/RP) *****
            '**********************************************************************************************
            'ALTRU HOSTED CLIENTS USE A GUID WHICH REFERS TO THE DATABASE KEY within Blackbaud Hosting's SiteManager
            'For Hosted Altru clients the server side IIS virtual directory is configured 
            ' to retrieve a listing of database keys from Blackbaud Hosting's SiteManager.  For Altru, pass a Guid to the 
            ' site manager instead of the typical database key that you would normally do for a non hosted Infinty instance such as BBEC.
            'The REDatabasueToUse typically refers to a "database key" that points to a SQL Server instance and database
            'For hosted Altru clients use a GUID that site manager will use to obtain the database key
            'The GUID can be found by locating the databaseName value at the end of the webshell's url.  See sample below.
            ' In BB hosted environments, on the server side, blackbaud's site manager will use this Guid to look up 
            ' the appropriate database key
            ' https://altrurig01bo3.blackbaudhosting.com/5740Altru_9a731bb7-0e50-48e3-b8c3-e03e16a5ac15/webui/webshellpage.aspx?databaseName=10399621-5E99-4916-8625-2703496B1C41
            '_clientAppInfoHeader.REDatabaseToUse = "10399621-5E99-4916-8625-2703496B1C41"

            '**********************************************************************
            '**** Option 1B - Non Hosted:  Manually Providing a Database Key  *****
            '**********************************************************************
            'Non hosted clients (BBEC) can obtain the database key by reviewing the connectionStrings element within
            'the web.config file on IIS. 
            '_clientAppInfoHeader.REDatabaseToUse = "BBInfinity"


            '**************************************************************
            '*** Option 2 - Retrieving a list of available databases  *****
            '**************************************************************
            'Alternatively, if you want the end user to select the database name, you can code a request to 
            ' retrieve a list of databases
            Dim req As New Blackbaud.AppFx.WebAPI.ServiceProxy.GetAvailableREDatabasesRequest
            req.ClientAppInfo = _clientAppInfoHeader

            'IMPORATANT:  For hosted, You must pass a valid HostedSiteInfoID to GetAvailableREDatabases 
            ' to retrieve the database list from SiteManager.  If you dont, you will get the following error message:
            '       Server was unable to process request. ---> 
            '       You must pass a valid siteInfoID to GetAvailableDatabases if the virtual directory is configured 
            ' HostedSiteInfoID is ignored in hon hosted scenarios. 
            req.HostedSiteInfoID = New System.Guid("9a731bb7-0e50-48e3-b8c3-e03e16a5ac15")

            'issue the request to the proxy.  Retrieve the reply from the proxy.  Populate a combo box using the reply.
            'to retrieve the database list from SiteManager.
            Dim reply As Blackbaud.AppFx.WebAPI.ServiceProxy.GetAvailableREDatabasesReply = _appFx.GetAvailableREDatabases(req)
            DisplayDatabases(reply)

        Catch ex As Exception
            MsgBox(ex.Message.ToString)

        Finally
            'Hide hourglass after api call
            Cursor.Current = Cursors.Default
            Cursor.Show()
        End Try
    End Sub

    Private Function GetNetworkCredentials() As System.Net.ICredentials

        '**** AUTHENTICATION ****
        'Each Altru user will have a Windows AD account within the hosted domain.
        'You will need to authenticate with the appropriate credentials.  Authentication is handled by Windows AD.
        'You will need a username and password to authenticate against the Windows Active Directory.  
        'For Altru, the appropriate domain name is configured on the Altru IIS server by Blackbaud Hosting.
        'Typically the developer does not need to provide the domain name.  This holds true for BBEC instances, as well. 

        '**** AUTHORIZATION ****
        'Each windows user account is linked to an application user within the Altru database.
        'Altru will authorize which features, such as a data list, the application user is allowed to automate/consume.
        'Therefore, the credentials you provide must link to an application user which is authorized to 
        ' utilize all the features that you wish to manipulate with the API
        'See Administration > Application > Security to associate application users to system roles and system roles to features.
        'Blackbaud Professional Services will need to provide credentials to 3rd party Altru API developers

        'Dim securelyStoredUserName, securelyStoredPassword, securelyStoredDomain As String
        Dim securelyStoredUserName, securelyStoredPassword As String

        'The user name does not need to be prefixed with the domain name... 
        ' For example, for the user name dont use DOMAINNAME\USERNAME just USERNAME

        '**** IMPORTANT *****
        'Don't hard code the URL, database identifier, user name and password to the Infinity application
        'The URL to the application, Database identifiers, User Names, and passwords need to be 
        'configurable and secure.  
        securelyStoredUserName = "name"
        securelyStoredPassword = "password"
        'securelyStoredDomain = "BLACKBAUDHOST"

        '**** Providing Credentials
        'System.Net.NetworkCredential implements System.NET.ICredentials
        'For Altru, the appropriate domain name is configured on the Altru IIS server by Blackbaud Hosting.
        'Typically the developer does not need to provide the domain name.  This holds true for BBEC instances, as well. 
        'Dim NetworkCredential As New System.Net.NetworkCredential(securelyStoredUserName, securelyStoredPassword, securelyStoredDomain)
        Dim NetworkCredential As New System.Net.NetworkCredential(securelyStoredUserName, securelyStoredPassword)

        Return NetworkCredential

    End Function

    Private Sub btnGetEvents_Click(sender As System.Object, e As System.EventArgs) Handles btnGetEvents.Click
        LoadEvents()
        Me.btnAddExpense.Enabled = False
    End Sub


    Private Sub LoadEvents()

        'Display hourglass during appfx web service calls
        Cursor.Current = Cursors.WaitCursor
        Cursor.Show()

        Me.lvEventExpenses.Clear()
        Me.lblEventExpenses.Text = "Expenses for "

        'You need 5 key pieces of information when consuming a feature.
        'User Name
        'Password
        'AppFxWebService.asmx URL
        'Database Key
        'System Record ID (Guid) or the Name of the feature that uniquely identifies the feature 
        '(i.e. datalist, dataform, searchlist, etc.)

        Dim Req As New Blackbaud.AppFx.WebAPI.ServiceProxy.DataListLoadRequest
        Dim fvSet As New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValueSet
        Dim dfi As New Blackbaud.AppFx.XmlTypes.DataForms.DataFormItem
        Dim Reply As New Blackbaud.AppFx.WebAPI.ServiceProxy.DataListLoadReply

        Req.ClientAppInfo = _clientAppInfoHeader

        'Provide the name of the feature as part of the request.  Use the metadata features like the Data List Search task 
        'to find the appropriate feature name.  
        'For 3rd party Altru developers, Blackbaud Professional Services will need to permission the API developer 
        ' with rights to view the feature metadata features.  This will enable the developer to obtain the feature name or system record id (GUID)
        Req.DataListName = "Event Calendar Event List"

        'As an alternative to the DataListName in the request, you could also use the DataListID/System Record ID.
        'Provide the system record id of the feature.
        'Req.DataListID = New System.Guid("cf479f2c-5657-42ca-8ed6-edde97c6a9ac")

        'If your data list has filters, you may need to provide a value for one or several of the
        ' form fields which serve as the parameters for the datalist's filter.
        ' Try out the feature's filter within the application to get a feel for each of the data list's parameters
        ' Tip:  See the <Parameters> tag within the datalistspec's xml to view the FormField tags that declare the
        ' parameters for the data list.  Or see the Filters tab on the data list metadata page.   
        ' For 3rd party Altru developers, Blackbaud Professional Services will need to permission the API developer 
        ' with rights to view the feature metadata features.

        'fvSet (Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValueSet) holds a collection of type  
        ' Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue.  Each Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue
        ' represents a form field.  For data lists, form fields are used as parameters.
        'The appropriate filter values can be determined by looking at the form fields within the 
        ' parameters portion of the data list spec.

        'Add an item to the collection of the DataFormFieldValueSet.  
        ' Use the form field's/parameter's FieldID value for the DataFormFieldValue.ID and
        ' a value via the DataFormFieldValue.Value
        'DataFormFieldValue's ID property represents the FieldID attribute within <FormField> element of the xml datalistspec.
        fvSet.Add(New Blackbaud.AppFx.XmlTypes.DataForms.DataFormFieldValue With {.ID = "DATEFILTER", .Value = "0"})

        'The DataFormItem is used to hold the set of form fields.
        dfi.Values = fvSet

        'DataFormItem is passed to the request
        Req.Parameters = dfi

        'Max rows acts as a governor to limit the amount of rows retrieved by the datalist
        Req.MaxRows = 500

        'If you plan on inspecting the metadata values that originated from DataListSpec xml doc, such as
        ' the FieldID or the IsHidden attributes of the OutputField element, then set IncludeMetaData to True
        'Later within the DisplayDataListReplyRowsInListView procedure we will inspect the meta data in the reply
        ' and use that metadata to format the UI grid, hide certain columns, etc. 
        Req.IncludeMetaData = True

        Try
            'The API utilizes a Request-Response pattern.  The pattern consists of request-response pairs.
            ' An operation is called on the proxy, such as DataListLoad.  The request is passed to the operation.
            'A reply is received from the proxy.  Each request and reply type is tailored to the operation.
            ' So, the proxy's DataListLoad operation will require a Blackbaud.AppFx.WebAPI.ServiceProxy.DataListLoadRequest and 
            ' will return a Blackbaud.AppFx.WebAPI.ServiceProxy.DataListLoadReply.
            'Since we need to 'load' a datalist up with data we will call the proxy's DataListLoad operation
            'Following the Request-Response pattern, we package up a request (DataListLoadRequest),
            'pass the request variable to the DataListLoad operation and receive a response (DataListLoadReply)
            Reply = _appFx.DataListLoad(Req)

            DisplayDataListReplyRowsInListView(Reply, lvEvents)

            _eventsLoaded = True

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
            _eventsLoaded = False
            MsgBox(ex.ToString)
        Finally
            'Hide hourglass after api call
            Cursor.Current = Cursors.Default
            Cursor.Show()
        End Try
    End Sub

    Private Sub DisplayDataListReplyRowsInListView(Reply As Blackbaud.AppFx.WebAPI.ServiceProxy.DataListLoadReply, ListView As System.Windows.Forms.ListView)
        Try
            'Display hourglass during appfx web service calls
            Cursor.Current = Cursors.WaitCursor
            Cursor.Show()

            With ListView
                .View = View.Details
                .FullRowSelect = True
                .Clear()
            End With

            'Since we set  Req.IncludeMetaData = True in the request, we will receive
            ' metadata values that originated from DataListSpec xml doc, such as
            ' the FieldID or the IsHidden attributes of the OutputField element.
            ' The reply will contain a MetaData property of type 
            ' Blackbaud.AppFx.WebAPI.ServiceProxy.DataListLoadMetaData()
            If (Reply.Rows IsNot Nothing) Then
                For Each f As Blackbaud.AppFx.XmlTypes.DataListOutputFieldType In Reply.MetaData.OutputDefinition.OutputFields
                    If f.IsHidden = True Then
                        ListView.Columns.Add(f.FieldID, f.Caption, 0)
                    Else
                        ListView.Columns.Add(f.FieldID, f.Caption)
                    End If
                Next

                For Each row As Blackbaud.AppFx.WebAPI.ServiceProxy.DataListResultRow In Reply.Rows
                    ListView.Items.Add(New ListViewItem(row.Values))
                Next

                ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
                For Each f As Blackbaud.AppFx.XmlTypes.DataListOutputFieldType In Reply.MetaData.OutputDefinition.OutputFields
                    If f.IsHidden = True Then
                        ListView.Columns(f.FieldID).Width = 0
                    End If
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString)

        Finally
            'Hide hourglass after api call
            Cursor.Current = Cursors.Default
            Cursor.Show()
        End Try
    End Sub

    Private Sub lvEvents_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvEvents.SelectedIndexChanged
        LoadExpenses()
        Me.btnAddExpense.Enabled = True
    End Sub

    Private Sub LoadExpenses()
        If _eventsLoaded = True Then
            Dim item As ListViewItem

            _eventName = ""
            _eventID = ""
            Dim SelectedEvent As ListView.SelectedListViewItemCollection = Me.lvEvents.SelectedItems
            For Each item In SelectedEvent
                _eventID = item.SubItems(0).Text.ToString
                _eventName = item.SubItems(1).Text.ToString
            Next
            If _eventID = "" Then
                Exit Sub
            End If

            Me.lblEventExpenses.Text = "Expenses for " & _eventName
            Dim Req As New Blackbaud.AppFx.WebAPI.ServiceProxy.DataListLoadRequest
            Dim dfi As New Blackbaud.AppFx.XmlTypes.DataForms.DataFormItem
            Dim Reply As New Blackbaud.AppFx.WebAPI.ServiceProxy.DataListLoadReply
            Dim price As Double = 0.0

            Req.ClientAppInfo = _clientAppInfoHeader

            'Provide the name of the feature as part of the request.  Use the metadata features 
            'like the Data List Search task to find the appropriate feature name.  
            'Blackbaud Professional Services will need to permission the API developer 
            ' with rights to the feature metadata features
            Req.DataListName = "Event Expense List"


            ' A data list may require a context value. Think of a context value as a "parent id".
            ' In the example below we are pulling the expenses FOR AN EVENT.  Therefore, the EventID would be 
            ' the parent id.  

            'Context is optional for a data list.  A data list may not need a context value.  These types of data lists are
            'termed "top level data lists".  The "Event Calendar Event List" is an example of a top level data list.
            'To determine whether a data list requires a context value, 
            'look at for a <Context> xml element within the xml for the datalistspec.
            'Alternatively, you could look to see if a value exists for the
            ' 'Context record type' in the summary section of the meta data page for the datalist.
            Req.ContextRecordID = _eventID

            'As an alternative to the DataListName in the request, you could also use the DataListID.
            'Provide the system record id of the feature.
            'Req.DataListID = New System.Guid("a62332d9-3f64-4b44-8b66-cfedfc747587")

            'Max rows acts as a governor to limit the amount of rows retrieved by the datalist
            Req.MaxRows = 500

            'If you plan on inspecting the metadata values that originated from DataListSpec xml doc, such as
            ' the FieldID or the IsHidden attributes of the OutputField element, then set IncludeMetaData to True
            Req.IncludeMetaData = True

            Try
                'The API utilizes a Request-Response pattern.  The pattern consists of request-response pairs.
                ' An operation is called on the proxy, such as DataListLoad.  The request is passed to the operation.
                'A reply is received from the proxy.  Each request and reply type is tailored to the operation.
                ' So, the proxy's DataListLoad operation will require a Blackbaud.AppFx.WebAPI.ServiceProxy.DataListLoadRequest and 
                ' will return a Blackbaud.AppFx.WebAPI.ServiceProxy.DataListLoadReply.
                'Since we need to 'load' a datalist up with data we will call the proxy's DataListLoad operation
                'Following the Request-Response pattern, we package up a request (DataListLoadRequest),
                'pass the request variable to the DataListLoad operation and receive a response (DataListLoadReply)
                Application.UseWaitCursor = True

                Reply = _appFx.DataListLoad(Req)

                DisplayDataListReplyRowsInListView(Reply, lvEventExpenses)

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
                MsgBox(ex.ToString)
            Finally
                Application.UseWaitCursor = False
            End Try

        End If
    End Sub


    

    Private Sub cbo_Databases_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbo_Databases.SelectedIndexChanged
        ' Hosted Example
        '_clientAppInfoHeader.REDatabaseToUse = "10399621-5E99-4916-8625-2703496B1C41"

        'Non Hosted Example
        '_clientAppInfoHeader.REDatabaseToUse = "BBInfinity"
        If Not cbo_Databases.SelectedValue Is Nothing Then
            _clientAppInfoHeader.REDatabaseToUse = cbo_Databases.SelectedValue
        End If
        Me.btnGetEvents.Enabled = True
    End Sub

    Private Sub DisplayDatabases(Reply As Blackbaud.AppFx.WebAPI.ServiceProxy.GetAvailableREDatabasesReply)
        'The DisplayDatabases procedure accepts an object of type Blackbaud.AppFx.WebAPI.ServiceProxy.GetAvailableREDatabasesReply
        ' which contains a list of databases

        Me.cbo_Databases.DisplayMember = "Display"
        Me.cbo_Databases.ValueMember = "Value"

        Dim DatabaseItems As System.Collections.ArrayList = New System.Collections.ArrayList
        Dim Value, Display As String


        'Reply.Databases will return a string array of databases configured for a particular Infinity application
        'The value for each item in the string array will be different for hosted versus non hosted applications.
        'For a hosted application, each item in the array will contain two values delimited with a semi-colon. 
        '  Example of one database value in the Hosted string array:  "10399621-5E99-4916-8625-2703496B1C41;5740Altru"
        '   The value before the semi-colon (0399621-5E99-4916-8625-2703496B1C41) is the GUID that site manager will 
        '   use to obtain the database key.
        '   The value after the semi-colon (5740Altru) is the friendly name to display to the end user. 
        For Each Row As String In Reply.Databases
            'If hitting a non hosted database then the Reply.Databases values will NOT be separated with a semicolon.
            If Row.IndexOf(";") = -1 Then
                Value = Row.ToString
                Display = Row.ToString
            Else
                'If hitting an Altru instance the Reply.Databases values will be separated with a semicolon so,
                ' parse out the GUID and the friendly display name.
                'Get the GUID for the Altru database
                Value = Row.Substring(0, Row.IndexOf(";"))
                'Get the friendly display name for the Altru database
                Display = Row.Substring((Row.IndexOf(";") + 1), (Row.Length - Row.IndexOf(";")) - 1)
            End If

            DatabaseItems.Add(New DatabaseKeyItem(Value, Display))
        Next

        Me.cbo_Databases.Items.Clear()
        Me.cbo_Databases.DataSource = DatabaseItems

        If cbo_Databases.Items.Count > 0 Then cbo_Databases.SelectedIndex = 0

    End Sub

    Private Class DatabaseKeyItem
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


    Private Sub lvEventExpenses_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvEventExpenses.SelectedIndexChanged
        Dim SelectedEventExpense As ListView.SelectedListViewItemCollection = Me.lvEventExpenses.SelectedItems
        For Each item In SelectedEventExpense
            _eventExpenseID = item.SubItems(0).Text.ToString
        Next

        If _eventExpenseID = "" Then
            Me.btnEditExpense.Enabled = False
            Me.btnDeleteExpense.Enabled = False

            Exit Sub
        Else

            Me.btnEditExpense.Enabled = True
            Me.btnDeleteExpense.Enabled = True
        End If
    End Sub

    Private Sub btnAddExpense_Click(sender As System.Object, e As System.EventArgs) Handles btnAddExpense.Click
        Try
            'Display hourglass during appfx web service calls
            Cursor.Current = Cursors.WaitCursor
            Cursor.Show()

            Dim ExpenseForm As New AddEditExpense(_eventID, _eventName, _appFx, _myCred, _clientAppInfoHeader, AddEditExpense.AddEditMode.Add)
            ExpenseForm.ShowDialog(Me)
            LoadExpenses()

        Catch ex As Exception
            MsgBox(ex.Message.ToString)

        Finally
            'Hide hourglass after api call
            Cursor.Current = Cursors.Default
            Cursor.Show()
        End Try


    End Sub


    Private Sub btnEditExpense_Click(sender As System.Object, e As System.EventArgs) Handles btnEditExpense.Click

        Try
            'Display hourglass during appfx web service calls
            Cursor.Current = Cursors.WaitCursor
            Cursor.Show()

            Dim ExpenseForm As New AddEditExpense(_eventID, _eventName, _appFx, _myCred, _clientAppInfoHeader, AddEditExpense.AddEditMode.Edit, _eventExpenseID)
            ExpenseForm.ShowDialog(Me)
            LoadExpenses()

        Catch ex As Exception
            MsgBox(ex.Message.ToString)

        Finally
            'Hide hourglass after api call
            Cursor.Current = Cursors.Default
            Cursor.Show()
        End Try

    End Sub

    Private Sub btnDeleteExpense_Click(sender As System.Object, e As System.EventArgs) Handles btnDeleteExpense.Click
        Dim req As New Blackbaud.AppFx.WebAPI.ServiceProxy.RecordOperationPerformRequest
        Dim reply As Blackbaud.AppFx.WebAPI.ServiceProxy.RecordOperationPerformReply

        req.ClientAppInfo = _clientAppInfoHeader
        req.ID = _eventExpenseID
        req.RecordOperationName = "Event Expense: Delete"
        'req.RecordOperationID = New System.Guid("eeba73b6-8878-41c5-9730-a3c7f9e64a9f")

        Try
            'Display hourglass during appfx web service calls
            Cursor.Current = Cursors.WaitCursor
            Cursor.Show()

            'The API utilizes a Request-Response pattern.  The pattern consists of request-response pairs.
            ' An operation is called on the proxy, such as RecordOperationPerform.  
            ' The request is passed to the operation.
            'A reply is received from the proxy.  Each request and reply type is tailored to the operation.
            ' So, the proxy's RecordOperationPerform operation will require a  
            'Blackbaud.AppFx.WebAPI.ServiceProxy.RecordOperationPerformRequest and will return a 
            'Blackbaud.AppFx.WebAPI.ServiceProxy.RecordOperationPerformReply.
            'Since we need to 'delete' a row of data we use the appropriate record operation/feature named 
            ' "Event Expense: Delete" 
            reply = _appFx.RecordOperationPerform(req)
            LoadExpenses()

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


    End Sub
End Class