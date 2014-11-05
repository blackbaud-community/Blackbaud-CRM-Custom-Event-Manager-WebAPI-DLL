'Imports Blackbaud.AppFx.WebAPI

Public Class SelectSearchResultsForm
    'The AppFxWebService proxy class is generated by Visual Studio (VS) when you add a web 
    ' reference to the  appfxwebservice.asmx.
    'The namespace (BBAppFxWebSvc) is named when you set up the web reference 
    ' to the AppFxWebService.asmx web service.  
    'After you set up the web reference, VS will generate a proxy class to help 
    ' you communicate with the web service
    'The proxy class is named AppFxWebService and it is organized 
    ' within the BBAppFxWebSvc namespace.
    Private _appFx As Blackbaud.AppFx.WebAPI.ServiceProxy.AppFxWebService
    'You will need to set credentials for the web service.  To prepare for this, 
    ' declare MyCred as type ICredentials
    Private _myCred As System.Net.ICredentials
    Private _clientAppInfoHeader As Blackbaud.AppFx.WebAPI.ServiceProxy.ClientAppInfoHeader
    Private _eventID As String
    Private _eventName As String


    'Private _searchListID As Guid
    Private _searchListName As String
    Private _searchListLoadReply As Blackbaud.AppFx.WebAPI.ServiceProxy.SearchListLoadReply
    Private _selectedRow As Blackbaud.AppFx.WebAPI.ServiceProxy.ListOutputRow



    Private Class MyListViewItem
        Inherits ListViewItem

        Private _row As Blackbaud.AppFx.WebAPI.ServiceProxy.ListOutputRow

        Friend ReadOnly Property Row() As Blackbaud.AppFx.WebAPI.ServiceProxy.ListOutputRow
            Get
                Return _row
            End Get
        End Property

        Friend Sub New(ByVal row As Blackbaud.AppFx.WebAPI.ServiceProxy.ListOutputRow)
            MyBase.New(row.Values)

            _row = row
        End Sub

    End Class




    'Public Sub New(ByVal service As Blackbaud.AppFx.WebAPI.ServiceProxy.AppFxWebService, ByVal header As Blackbaud.AppFx.WebAPI.ServiceProxy.ClientAppInfoHeader, ByVal searchListID As Guid, ByVal searchListLoadReply As Blackbaud.AppFx.WebAPI.ServiceProxy.SearchListLoadReply)
    '    Me.InitializeComponent()

    '    _appFx = service
    '    _clientAppInfoHeader = header
    '    _searchListID = searchListID
    '    _searchListLoadReply = searchListLoadReply

    'End Sub


    Public Sub New(ByVal service As Blackbaud.AppFx.WebAPI.ServiceProxy.AppFxWebService, ByVal header As Blackbaud.AppFx.WebAPI.ServiceProxy.ClientAppInfoHeader, ByVal searchListName As String, ByVal searchListLoadReply As Blackbaud.AppFx.WebAPI.ServiceProxy.SearchListLoadReply)
        Me.InitializeComponent()

        _appFx = service
        _clientAppInfoHeader = header
        _searchListName = searchListName
        _searchListLoadReply = searchListLoadReply

    End Sub

    Public Property SelectedRow() As Blackbaud.AppFx.WebAPI.ServiceProxy.ListOutputRow
        Get
            Return _selectedRow
        End Get
        Set(ByVal value As Blackbaud.AppFx.WebAPI.ServiceProxy.ListOutputRow)
            _selectedRow = value
        End Set
    End Property

    Private Sub SelectSearchResults_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadOutput()
        btn_Select.Enabled = (lv_Output.Items.Count > 0)
    End Sub

    Private Sub LoadOutput()

        'issue another request to get the search list metadata, so we can built the output columns
        Try
            Dim req As New Blackbaud.AppFx.WebAPI.ServiceProxy.SearchListGetMetaDataRequest
            req.ClientAppInfo = _clientAppInfoHeader
            req.SearchListName = _searchListName
            ' req.SearchListID = _searchListID

            Dim reply As Blackbaud.AppFx.WebAPI.ServiceProxy.SearchListGetMetaDataReply = _appFx.SearchListGetMetaData(req)

            'setup the listview based on the search list metadata
            With lv_Output
                .Clear()
                .View = View.Details
                .FullRowSelect = True
                For Each f As Blackbaud.AppFx.XmlTypes.SearchListOutputFieldType In reply.OutputDefinition.OutputFields
                    lv_Output.Columns.Add(f.Caption)
                Next
            End With

            'load the list with the search results
            For Each row As Blackbaud.AppFx.WebAPI.ServiceProxy.ListOutputRow In _searchListLoadReply.Output.Rows
                lv_Output.Items.Add(New MyListViewItem(row))
            Next

            lv_Output.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)

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

        End Try

    End Sub

    Private Sub lv_Output_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lv_Output.DoubleClick
        SelectRow()
    End Sub

    Private Sub btn_Select_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Select.Click
        SelectRow()
    End Sub

    Private Sub SelectRow()

        If lv_Output.SelectedItems.Count = 0 Then Exit Sub
        Dim lv As MyListViewItem = DirectCast(lv_Output.SelectedItems(0), MyListViewItem)

        _selectedRow = lv.Row
        Me.DialogResult = Windows.Forms.DialogResult.OK

    End Sub

End Class