Imports System.IO
Imports System.Text.RegularExpressions

Public Class Form1

    Private NameValid As Boolean
    Private SecondNameValid As Boolean
    Private EmailValid As Boolean

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

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
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

    Private Sub txtFirstName_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFirstName.Leave
        'If Not A Matching Format Entered
        If Not Regex.Match(txtFirstName.Text, "^[a-z]*$", RegexOptions.IgnoreCase).Success Then 'Only Letters

            MessageBox.Show("Please Enter Alphabetic Characters Only") 'Inform User

            txtFirstName.Focus() 'Return Focus
            txtFirstName.Clear() 'Clear TextBox

            NameValid = False 'Boolean = False
        Else

            NameValid = True 'Everything Fine

        End If
    End Sub

    Private Sub txtSecondName_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSecondName.Leave
        'Create A Pattern For Surname
        Dim strSurname As String = "^[a-zA-Z\s]+$"

        Dim reSurname As New Regex(strSurname) 'Attach Pattern To Surname Textbox

        'Not A Match
        If Not reSurname.IsMatch(txtSecondName.Text) Then

            MessageBox.Show("Please Enter Alphabetic Characters Only")

            txtSecondName.Focus()

            txtSecondName.Clear()

            SecondNameValid = False

        Else

            SecondNameValid = True

        End If
    End Sub

    Private Sub ValidateEmail()

        'Set Up Reg Exp Pattern To Allow Most Characters, And No Special Characters
        Dim reEmail As Regex = New Regex("([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\." + _
        ")|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})", _
        RegexOptions.IgnoreCase _
        Or RegexOptions.CultureInvariant _
        Or RegexOptions.IgnorePatternWhitespace _
        Or RegexOptions.Compiled _
        )

        Dim blnPossibleMatch As Boolean = reEmail.IsMatch(txtEmailAddress.Text)

        If blnPossibleMatch Then

            'Check If Entered Email Is In Correct Format
            If Not txtEmailAddress.Text.Equals(reEmail.Match(txtEmailAddress.Text).ToString) Then

                MessageBox.Show("Please enter a valid email")

            Else

                EmailValid = True 'Email is valid

            End If

        Else 'Not A Match To Pattern

            EmailValid = False 'Set Boolean Variable To False

            MessageBox.Show("Please enter a valid email") 'Inform User

            txtEmailAddress.Clear() 'Clear Textbox

            txtEmailAddress.Focus() 'Set Focus To TextBox

        End If

    End Sub

    Private Sub txtEmail_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmailAddress.LostFocus

        ValidateEmail() 'Check Email Validity

    End Sub
End Class