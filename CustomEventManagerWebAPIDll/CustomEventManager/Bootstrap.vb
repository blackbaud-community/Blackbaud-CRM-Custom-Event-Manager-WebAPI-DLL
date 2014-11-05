
Public Class Bootstrap


    'This is the main entry point into this app
    '<STAThread()> indicates that the application 
    'should join the Single-Threaded Apartment (STA). 
    <STAThread()> _
    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New Form1())
    End Sub

End Class






