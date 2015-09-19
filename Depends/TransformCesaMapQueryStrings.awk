#!/usr/bin/gawk

# sample:
# for f in CESA*.ascx; do gawk -f TransformCesaMapQueryStrings.awk $f; done

# Used to make the QueryStrings in the CESA Maps programatic instead of hard-coded.

BEGIN {
	# Set RS(Record Separator) to end of area tags
	RS = "/>"
	ORS = ""
	cachef = "first.awk.out"
	system( "touch " cachef)
}

{if (FNR == 1) { print "processing file:" FILENAME "\n"} }
#if an area tag
 /<area/  {
	# substitute
	sub(/SchoolScript.aspx\?SEARCHTYPE=/, 
		"SchoolScript.aspx<%Response.Write( GetQueryString(new String[] {\"SEARCHTYPE=")
	
	sub(/XXXX&/,
		"XXXX\"})); %>")
	
	gsub( /\&/,
		"\",\"")
		
 }

 # make sure we can write to the file
 {  if (system("test -r " cachef) != 0) system("sleep 1s")  }
 

 #cache
 # since print always uses ORS, we have set it to empty
 # RT (Record Text) is what was actually matched, 
 # so records with no RS won't have ORS appended erroneously
 {	print $0 RT >cachef }
 
 END {
	close(FILENAME)
	#write back to source file
	system( "cat " cachef " >" FILENAME )
	# seems like it shouldn't be necessary, but svn detects changes without EOL conversion:
	system( "unix2dos " FILENAME)
	close(cachef)
	system("rm " cachef)
}
 