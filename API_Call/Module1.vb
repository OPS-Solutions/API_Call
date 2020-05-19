Imports System.Net
Module Module1
	Sub Main()

		Dim strArgs As String = Command()
		Console.WriteLine("Arguments: " & strArgs)
		Console.WriteLine("")

		If strArgs.Contains("?") Then
			Log("Argument help", "http://127.0.0.1:21212/api/Waypoints|{""Id"": 1,""Name"": ""WaypointName1""}")
			End
		End If

		Dim strArray As String() = strArgs.Split("|")
		If UBound(strArray) = 1 Then
			POST(strArray(0), strArray(1))
		Else
			Log("Bad arguments, use single pipe (|) as delimiter")
		End If
	End Sub

	Private Sub POST(ByVal URL_and_Endpoint As String, Optional ByVal body As String = "")
		Try
			Using wc As WebClient = New WebClient()
				wc.Headers.Set(HttpRequestHeader.ContentType, "application/json")
				wc.Headers.Set(HttpRequestHeader.Accept, "application/json")

				Dim result As String = wc.UploadString(URL_and_Endpoint, "POST", body)

				If Not String.IsNullOrWhiteSpace(result) Then
					Log("Result:", result)
				End If
			End Using
		Catch ex As Exception
			Log("Exception sending POST request", ex.ToString)
		End Try
	End Sub
	Private Sub Log(ParamArray ByVal strMessage As String())
		For Each strLine As String In strMessage
			Console.WriteLine(strLine)
		Next

		Console.WriteLine("")
		Console.WriteLine("Press any key to exit")
		Console.ReadKey()
	End Sub
End Module
