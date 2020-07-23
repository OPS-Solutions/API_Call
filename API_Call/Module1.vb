Imports System.Net
Module Module1
	' In LGS Open File / URL:
	' Path:      ->  C:\Source Code\API_Call\API_Call\bin\Debug\API_Call.exe
	' Arguments: ->  "http://121.0.0.1:21212/api/Waypoints|{\"Id\": 1,\"Name\": \"Sad\"}"
	'   you have to escape the " symbol using \"
	'   
	Sub Main()
		Try
			Dim strArgs As String = Command()
			Console.WriteLine("Arguments: " & strArgs)
			Console.WriteLine("")

			If strArgs.Contains("?") OrElse (strArgs.Length < 2) Then
				Log("Argument help (SpotGuide)", "http://127.0.0.1:21212/api/Waypoints|{""Id"": 1,""Name"": ""WaypointName1""}")
				LogWithPause("Argument help (LightGuide)", "http://127.0.0.1:8080/api/variables/set|[{""Name"": ""Quantity"", ""Value"": ""18""},{""Name"": ""PartNumber"", ""Value"": ""ABCD1234""}]")
				End
			End If

			Dim strArray As String() = strArgs.Split("|")
			If UBound(strArray) = 1 Then
				POST(strArray(0), strArray(1))
			Else
				LogWithPause("Bad arguments, use single pipe (|) as delimiter")
			End If
		Catch ex As Exception
			LogWithPause("Error loading application: " & ex.ToString)
		End Try

	End Sub

	Private Sub POST(ByVal URL_and_Endpoint As String, Optional ByVal body As String = "")
		Try
			Using wc As WebClient = New WebClient()
				wc.Headers.Set(HttpRequestHeader.ContentType, "application/json")
				wc.Headers.Set(HttpRequestHeader.Accept, "application/json")

				Dim result As String = wc.UploadString(URL_and_Endpoint, "POST", body)

				If result?.ToUpper.Contains("EXCEPTION") Then
					LogWithPause("Result:", result)
				Else
					Log("Result:", result)
					Threading.Thread.Sleep(2000)
				End If
			End Using
		Catch ex As Exception
			LogWithPause("Exception sending POST request", ex.ToString)
		End Try
	End Sub
	Private Sub Log(ParamArray ByVal strMessage As String())
		For Each strLine As String In strMessage
			Console.WriteLine(strLine)
		Next

		Console.WriteLine("")
	End Sub
	Private Sub LogWithPause(ParamArray ByVal strMessage As String())
		For Each strLine As String In strMessage
			Console.WriteLine(strLine)
		Next

		Console.WriteLine("This executable is called 'API_Call' version " & My.Application.Info.Version.ToString)
		Console.WriteLine("")
		Console.WriteLine("Press any key to exit")
		Console.ReadKey()
	End Sub
End Module
