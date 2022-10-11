Imports System.Net.Security

Public Class Form1

    Public rand As New Random

    Dim counter = New Integer() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'RichTextBox1.AppendText(rand.NextDouble().ToString + Environment.NewLine)

        'Dim sum As Double = 0.0
        'Dim numeroValori = 1000

        'For index As Integer = 1 To numeroValori
        '    Dim value As Double = rand.NextDouble()
        '    RichTextBox1.AppendText(value.ToString + Environment.NewLine)
        '    sum += value
        'Next

        'RichTextBox1.AppendText("The media is :" + (sum / numeroValori).ToString)

        Timer1.Enabled = True
        Timer1.Interval = 1000




    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim value As Double = rand.NextDouble()
        'sum += value
        'num += 1
        'RichTextBox1.AppendText("New value: " + value.ToString + "Media corrente: " + (sum / num).ToString + Environment.NewLine)

        Console.WriteLine("value: " + value.ToString)

        If value = 1.0 Then
            counter(9) += 1
        Else
            counter(Int(value * 10)) += 1
        End If


        For index = 0 To 9
            RichTextBox1.AppendText(counter(index).ToString + " ")
        Next
        RichTextBox1.AppendText(Environment.NewLine)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Stop()
    End Sub
End Class
