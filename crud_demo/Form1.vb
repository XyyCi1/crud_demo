Imports MySql.Data.MySqlClient

Public Class Form1

    Dim conn As MySqlConnection
    Dim COMMAND As MysqlCommand
    Private Sub ButtonConnect_Click(sender As Object, e As EventArgs) Handles ButtonConnect.Click
        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost;userid=root;password=root;database=crud_demo_db;"

        Try
            conn.Open()
            MessageBox.Show("Connection Successful")
        Catch ex As Exception
            MessageBox.Show("Connection Failed: " & ex.Message)
            conn.Close()
        End Try


    End Sub

    Private Sub ButtonInsert_Click(sender As Object, e As EventArgs) Handles ButtonInsert.Click

    End Sub
End Class
