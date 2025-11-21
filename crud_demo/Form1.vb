Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.SqlServer
Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand



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
        Dim query As String = "INSERT INTO students_tbl (name, age, email) VALUES (@name, @age, @email)"
        Try
            Using conn As New MySqlConnection("Server = localhost;userid=root;password=root;database=crud_demo_db;")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", TextBoxName.Text)
                    cmd.Parameters.AddWithValue("@age", CInt(TextBoxAge.Text))
                    cmd.Parameters.AddWithValue("@email", TextBoxEmail.Text)
                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Record Inserted Successfully")

                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub ButtonRead_Click(sender As Object, e As EventArgs) Handles ButtonRead.Click
        Dim query As String = "SELECT * FROM crud_demo_db.students_tbl;"

        Try

            Using conn As New MySqlConnection("server=localhost;userid=root;password=root;database=crud_demo_db;")
                Dim adapter As New MySqlDataAdapter(query, conn) ' get from
                Dim table As New DataTable() ' table object
                adapter.Fill(table) ' from adapter to table object
                DataGridView1.DataSource = table ' display to DataGridView
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim id As Integer = CInt(selectedRow.Cells("id").Value)

            Dim query As String = "DELETE FROM students_tbl WHERE id = @id"
            Try
                Using conn As New MySqlConnection("server=localhost;userid=root;password=root;database=crud_demo_db;")
                    conn.Open()
                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@id", id)
                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                        If rowsAffected > 0 Then
                            MessageBox.Show("Record Deleted Successfully")
                        Else
                            MessageBox.Show("Delete Failed: No matching record found")
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        Else
            MessageBox.Show("Please select a row to delete")
        End If
    End Sub



    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click
        DataGridView1.EndEdit()

        Dim rowIndex As Integer = DataGridView1.CurrentCell.RowIndex
        Dim selectedRow As DataGridViewRow = DataGridView1.Rows(rowIndex)
        Dim idValue = selectedRow.Cells("id").Value
        Dim nameValue = selectedRow.Cells("name").Value
        Dim ageValue = selectedRow.Cells("age").Value
        Dim emailValue = selectedRow.Cells("email").Value
        If idValue IsNot Nothing AndAlso idValue.ToString().Trim() <> "" AndAlso Integer.TryParse(idValue.ToString(), Nothing) Then
            Dim query As String = "UPDATE students_tbl SET name = @name, age = @age, email = @email WHERE id = @id"
            Try
                Using conn As New MySqlConnection("server=localhost;userid=root;password=root;database=crud_demo_db;")
                    conn.Open()
                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@name", nameValue.ToString())
                        cmd.Parameters.AddWithValue("@age", CInt(ageValue))
                        cmd.Parameters.AddWithValue("@email", emailValue.ToString())
                        cmd.Parameters.AddWithValue("@id", CInt(idValue))
                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                        If rowsAffected > 0 Then
                            MessageBox.Show("Record Updated Successfully")
                            ButtonRead_Click(Nothing, Nothing)
                        Else
                            MessageBox.Show("Update Failed: No matching record found")
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        Else
            MessageBox.Show("ID is invalid. Please select a valid row.")
        End If
    End Sub


End Class
