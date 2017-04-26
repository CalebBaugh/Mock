Imports System.IO
Public Class Form1

    Private Structure ClientInfo
        Dim ClientID As String
        Dim FirstName As String
        Dim SecondName As String
        Dim DOB As String                  'This is the structure that will hold the data that is entered
        Dim EmailAddress As String
    End Structure
    Private Sub Form1_Load() Handles MyBase.Load
        txtClientID.Enabled = False
        If Dir$("Details.txt") = "" Then
            Dim sw As New StreamWriter("Details.txt", True)    'This checks if there is a database to enter/read the data from and if there is not it will create one to use
            sw.WriteLine("")
            sw.Close()
            MsgBox("A new file has been created", vbExclamation, "Warning!")
        End If
    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim ClientData As New ClientInfo
        Dim sw As New System.IO.StreamWriter("Details.txt", True)
        ClientData.ClientID = LSet(txtClientID.Text, 50)
        ClientData.FirstName = LSet(txtFirstName.Text, 50)
        ClientData.SecondName = LSet(txtSecondName.Text, 50)
        ClientData.DOB = LSet(DateOfBirth.Text, 50)                      'Inputting data into the structure
        ClientData.EmailAddress = LSet(txtEmailAddress.Text, 50)

        sw.WriteLine(ClientData.ClientID & ClientData.FirstName & ClientData.SecondName & ClientData.DOB & ClientData.EmailAddress)
        sw.WriteLine("                                                                                             ")
        sw.Close()                                                                  'Has to close when complete
        MsgBox("The file has been saved")
    End Sub

    Private Sub cmdSearch_Click(sender As System.Object, e As System.EventArgs) Handles cmdSearch.Click
        Dim sr As New System.IO.StreamReader("Details.txt", True)
        txtClientID.Enabled = True
        txtFirstName.Enabled = False
        txtSecondName.Enabled = False
        DateOfBirth.Enabled = False                                         'Disabling the text boxes so the data can be shown but not altered
        txtEmailAddress.Enabled = False
        txtFirstName.Text = ""
        txtSecondName.Text = ""                                             'Clearing the text boxes of any previously entered data
        DateOfBirth.Text = ""
        txtEmailAddress.Text = ""
        Dim ClientID As Integer
        ClientID = 0
        Dim ClientData() As String = File.ReadAllLines("Details.txt")
    End Sub
End Class