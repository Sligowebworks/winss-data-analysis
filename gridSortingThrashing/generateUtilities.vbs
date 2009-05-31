OPTION EXPLICIT

Dim FSO	'File System Object
Dim tsIn	'text stream object
Dim tsOut	' text stream object
Dim strRealFileName	'fully qualified file descriptor
Dim strFilePath	'path

sub InvokeGenerate()
	Set FSO = WSH.CreateObject("Scripting.FileSystemObject")

	strRealFileName = FSO.GetFileName(strFileName)

	If strRealFileName = strFileName Then
		strFilePath = ""
	Else
		strFilePath = mid(strFileName, 1, Len(strFileName) - Len(strRealFileName) )
	End If

	Set tsIn = FSO.OpenTextFile(strFileName, 1)	'for reading

	Dim str
	'read-in input to array
	rowCount = 0
	Do Until tsIn.AtEndOfStream

		str = tsIn.ReadLine
		
		if Not isNewStanza(str) then
			AddRow(str)
		End If
		
		if isNewStanza(str) or tsIn.AtEndOfStream then
			if rowCount > 0 then	
			'execute and flush queue
				Call Generate
				tsOut.Close
				rowCount = 0
				Erase rows
			end if
		end if
		
		if isNewStanza(str) then
			' is new class stanza, create out file
			Set tsOut = FSO.CreateTextFile(strFilePath & CreateOutputFileName(str))
		end if
	Loop

	Set tsIn = Nothing
	Set tsOut = Nothing
	Set FSO = Nothing
end sub

sub debug(strMsg, blnActive)
	if blnDEBUG AND blnActive then
		MsgBox(strMsg)
	end if
end sub

sub AddRow(strLine)
	dim n
	
	'ignore blank lines
	if Trim(strLine) = "" then
		exit sub
	end if
	
	Call getValues(strLine)
	Redim Preserve rows(FIELD_ARRAY_MAX, rowCount)
	for n = 0 to FIELD_ARRAY_MAX
		rows(n,rowCount) = values(n)
		debug "AddRow():: row added" & NEW_STANZA_DELIM & n & "," & rowCount, false
	next

	rowCount=rowCount+1
end sub

function isNewStanza(strLine)
	if InStr(strLine, NEW_STANZA_DELIM) = 1 then
		isNewStanza = true
	else 
		isNewStanza = false
	end if
end function

function CreateOutputFileName(strLine)
	dim nStart, nLength
	dim filename
	
	if isNewStanza(strLine) then
		nStart = 3
	else
		nStart = 1
	end if
	
	if (InStrRev(strLine, NEW_STANZA_DELIM) <> inStr(strLine, NEW_STANZA_DELIM) ) then
		nLength = InStrRev(strLine, NEW_STANZA_DELIM) - nStart
	else
		nLength = Len(strLine)
	end if
	
	'set global
	stanzaName = Mid(strLine, nStart, nLength)
	debug "Class Name::" & stanzaName, true
	
	filename = stanzaName & FILE_EXT
	CreateOutputFileName = filename
end function

sub getValues(strLine)
	dim n
	dim i
	strLine = Trim(strLine)
	Redim values(FIELD_ARRAY_MAX)
	
	n = (FIELD_ARRAY_MAX)
	do while n >= 0
		i = InStrRev(strLine, FIELD_DELIM) 
		values(n) = Trim(Mid(strLine, i + 1, Len(strLine))) ' pos of seperator to the end
		debug "getValues:: array value added:" & values(n), false
		if n > 0 then
		debug "[" & strLine & "]", false
			strLine = RTrim(Left(strLine, i-1))
		end if
	n=n-1
	loop
end sub
